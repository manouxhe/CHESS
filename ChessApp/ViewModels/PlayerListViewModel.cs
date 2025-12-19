using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ChessApp.Models;
using ChessApp.Service;
using System.ComponentModel.DataAnnotations;

namespace ChessApp.ViewModels;

public partial class PlayerListViewModel : ViewModelBase
{
    private readonly Game _game;      // Fabrique de joueurs

    // Collection complète
    private readonly ObservableCollection<PlayerViewModel> _allPlayers = new();
    public ObservableCollection<FieldViewModel> CustomFields { get; } = new();
    public ObservableCollection<PlayerViewModel> FilteredPlayers { get; } = new();
    public ClassementViewModel ClassementVM { get; } = new ClassementViewModel(new ChessClassement());
    public CompetitionListViewModel CompetitionList { get; }

    // La méthode passée en Action aux enfants
    private void OnPlayerInfoChanged()
    {
        UpdateClassement();
    }

    public PlayerListViewModel()
    {
        _game = new Gamechess();
        ClassementVM = new ClassementViewModel(new ChessClassement());
        CompetitionList = new CompetitionListViewModel(this, new CompetitionService(), new EloCalculator());
        // Données pour le designer Avalonia, comme dans la ToDoList
        if (Design.IsDesignMode)
        {
            foreach (var field in _game.GetFields())
                CustomFields.Add(field);

            var p1 = new Chessplayer()
            {
                FirstName = "Magnus (Design)",
                LastName = "Carlsen",
                Email = "magnus@chess.com",
            };
            p1.Rankings.Add("ELO_FIDE", new Eloranking("ELO FIDE", 2830));

            var p2 = new Chessplayer()
            {
                FirstName = "Hikaru (Design)",
                LastName = "Nakamura",
                Email = "hika@chess.com",
            };
            p2.Rankings.Add("ELO_FIDE", new Eloranking("ELO FIDE", 2780));

            _allPlayers.Add(new PlayerViewModel(p1));
            _allPlayers.Add(new PlayerViewModel(p2));
            RefreshFilteredList();
            UpdateClassement();
        }
    }

    public PlayerListViewModel(Game game, Classement classement, Calculator calculator)
    {
        _game = game;
        ClassementVM = new ClassementViewModel(classement);
        CompetitionList = new CompetitionListViewModel(this, new CompetitionService(), calculator);
        foreach (var field in _game.GetFields())
        {
            field.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(FieldViewModel.Value))
                    AddPlayerCommand.NotifyCanExecuteChanged();
            };


            field.ErrorsChanged += (s, e) => AddPlayerCommand.NotifyCanExecuteChanged();

            CustomFields.Add(field);
        }
        RefreshFilteredList();
        UpdateClassement();
    }

    [ObservableProperty]
    private string? _searchText; // Ce que je vais taper dans la barre de recherche

    //ici c'est la Commande pour déclencher par le bouton "Rechercher"
    [RelayCommand]
    private void Search()
    {
        RefreshFilteredList(SearchText);
    }

    public void RefreshFilteredList(string? filter = null)
    {
        FilteredPlayers.Clear();

        IEnumerable<PlayerViewModel> players = string.IsNullOrWhiteSpace(filter)
            ? _allPlayers
            : _allPlayers.Where(p =>
                (p.FirstName?.Contains(filter, StringComparison.OrdinalIgnoreCase) == true) ||
                (p.LastName?.Contains(filter, StringComparison.OrdinalIgnoreCase) == true)
              );

        foreach (var player in players)
            FilteredPlayers.Add(player);
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Le prénom est requis.")]
    [NotifyCanExecuteChangedFor(nameof(AddPlayerCommand))]
    private string? _newPlayerFirstName;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Le nom est requis.")]
    [NotifyCanExecuteChangedFor(nameof(AddPlayerCommand))]
    private string? _newPlayerLastName;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "L'email est requis.")]
    [EmailAddress(ErrorMessage = "Format d'email invalide.")] // Vérifie @ 
    [NotifyCanExecuteChangedFor(nameof(AddPlayerCommand))]
    private string? _newPlayerEmail;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "La date de naissance est requise.")]
    [NotifyCanExecuteChangedFor(nameof(AddPlayerCommand))]
    private DateTimeOffset? _newPlayerBirthDate;

    [ObservableProperty]
    private string? _newPlayerRankName;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddPlayerCommand))]
    private string? _newPlayerRankValue;

    [RelayCommand(CanExecute = nameof(CanAddPlayer))]
    private void AddPlayer()
    {
        DateTime birthDate = NewPlayerBirthDate?.DateTime ?? DateTime.Now;
        var newPlayerModel = _game.CreatePlayer(

           NewPlayerFirstName, NewPlayerLastName, NewPlayerEmail, birthDate, CustomFields.ToList()
        );
        var newPlayerVM = new PlayerViewModel(newPlayerModel, OnPlayerInfoChanged);
        _allPlayers.Add(newPlayerVM);
        RefreshFilteredList(SearchText);
        UpdateClassement();
        NewPlayerFirstName = NewPlayerLastName = NewPlayerEmail = null;
        NewPlayerBirthDate = null;
        foreach (var field in CustomFields) field.Reset();
    }

    private bool CanAddPlayer()
    {
        bool mainFieldsOK = !string.IsNullOrWhiteSpace(NewPlayerFirstName) &&
                        !string.IsNullOrWhiteSpace(NewPlayerLastName) &&
                        !string.IsNullOrWhiteSpace(NewPlayerEmail) &&
                        NewPlayerBirthDate != null &&
                        !HasErrors;


        bool customFieldsOK = CustomFields.All(f => !string.IsNullOrWhiteSpace(f.Value) && !f.HasErrors);

        return mainFieldsOK && customFieldsOK;
    }

    [RelayCommand]
    private void RemovePlayer(PlayerViewModel player)
    {
        if (player == null) return;

        _allPlayers.Remove(player);
        FilteredPlayers.Remove(player);
        UpdateClassement();
    }
    private void UpdateClassement()
    {
        ClassementVM.Refresh(_allPlayers);
    }
}