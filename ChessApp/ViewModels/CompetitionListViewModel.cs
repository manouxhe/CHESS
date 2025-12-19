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
    private readonly PlayerListViewModel _playerListVM;
    private readonly Calculator _calculator;

    // liste observable des competitions
    public ObservableCollection<CompetitionViewModel> Competitions { get; } = new();

    [ObservableProperty]
    private CompetitionViewModel? _selectedCompetition;

    // champs pour creer une nouvelle competition
    [ObservableProperty] private string? _newName;
    [ObservableProperty] private DateTime? _newStart;
    [ObservableProperty] private DateTime? _newEnd;
    [ObservableProperty] private string? _newLocation;

    public CompetitionListViewModel(PlayerListViewModel playerListVM, ICompetitionService competitionService, Calculator calculator)
    {
        _playerListVM = playerListVM;
        _competitionService = competitionService;
        _calculator = calculator;
    }

    // commande pour creer une competition
    [RelayCommand]
    private void AddCompetition()
    {
        if (string.IsNullOrWhiteSpace(NewName) || _competitionService == null || _playerListVM == null)
            return;

        var comp = _competitionService.CreateCompetition(
            NewName,
            NewStart,
            NewEnd, 
            NewLocation ?? ""
        );

        var vm = new CompetitionViewModel(comp, _competitionService,  _playerListVM.FilteredPlayers, _calculator);
        Competitions.Add(vm);

        NewName = null;
        NewLocation = null;
        NewStart = null; 
        NewEnd = null;
    }

    [RelayCommand]
    private void DeleteCompetition(CompetitionViewModel vm)
    {
        _competitionService.DeleteCompetition(vm.Id);
        Competitions.Remove(vm);
    }

}
