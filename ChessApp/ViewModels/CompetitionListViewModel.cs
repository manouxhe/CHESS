using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ChessApp.Models;
using ChessApp.Service;

namespace ChessApp.ViewModels;

// viewmodel qui gere la liste des competitions
public partial class CompetitionListViewModel : ViewModelBase
{
    private readonly ICompetitionService _competitionService;

    // liste observable des competitions
    public ObservableCollection<CompetitionViewModel> Competitions { get; } = new();

    [ObservableProperty]
    private CompetitionViewModel? _selectedCompetition;

    // champs pour creer une nouvelle competition
    [ObservableProperty]
    private string? _newCompetitionName;

    [ObservableProperty]
    private DateTimeOffset? _newCompetitionDate;

    [ObservableProperty]
    private string? _newCompetitionLocation;

    public CompetitionListViewModel(ICompetitionService competitionService)
    {
        _competitionService = competitionService;

        // chargement initial des competitions
        foreach (var comp in _competitionService.GetCompetitions())
        {
            Competitions.Add(
                new CompetitionViewModel(comp, _competitionService)
            );
        }
    }

    // commande pour creer une competition
    [RelayCommand]
    private void AddCompetition()
    {
        if (string.IsNullOrWhiteSpace(NewCompetitionName)
            || NewCompetitionDate == null
            || string.IsNullOrWhiteSpace(NewCompetitionLocation))
            return;

        var comp = _competitionService.CreateCompetition(
            NewCompetitionName,
            NewCompetitionDate.Value.DateTime,
            NewCompetitionLocation
        );

        var vm = new CompetitionViewModel(comp, _competitionService);
        Competitions.Add(vm);

        SelectedCompetition = vm;

        // reset des champs
        NewCompetitionName = null;
        NewCompetitionDate = null;
        NewCompetitionLocation = null;
    }
}
