using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ChessApp.Models;
using ChessApp.Service;

namespace ChessApp.ViewModels;

public partial class PlayerListViewModel : ViewModelBase
{
    private readonly Game _game;      // Fabrique de joueurs

    // Collection complète
    private readonly ObservableCollection<PlayerViewModel> _allPlayers = new();
    public ObservableCollection<FieldViewModel> CustomFields { get; } = new();
    public ObservableCollection<PlayerViewModel> FilteredPlayers { get; } = new();

    public PlayerListViewModel()
    {
        _game = new Gamechess();

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
        }
    }

    public PlayerListViewModel(Game game)
    {
        _game = game;
        foreach (var field in _game.GetFields())
        {
            CustomFields.Add(field);
        }
        RefreshFilteredList();
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
    [NotifyCanExecuteChangedFor(nameof(AddPlayerCommand))]
    private string? _newPlayerFirstName;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddPlayerCommand))]
    private string? _newPlayerLastName;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddPlayerCommand))]
    private string? _newPlayerEmail;

    [ObservableProperty]
    private string? _newPlayerRankName;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddPlayerCommand))]
    private string? _newPlayerRankValue;

    [RelayCommand(CanExecute = nameof(CanAddPlayer))]
    private void AddPlayer()
    {
        var newPlayerModel = _game.CreatePlayer(
           NewPlayerFirstName, NewPlayerLastName, NewPlayerEmail, CustomFields.ToList()
        );
        var newPlayerVM = new PlayerViewModel(newPlayerModel);
        _allPlayers.Add(newPlayerVM);
        RefreshFilteredList(SearchText);

        NewPlayerFirstName = NewPlayerLastName = NewPlayerEmail = null;
        foreach (var field in CustomFields) field.Reset();
    }

    private bool CanAddPlayer() =>
            !string.IsNullOrWhiteSpace(NewPlayerFirstName) &&
            !string.IsNullOrWhiteSpace(NewPlayerLastName);

    [RelayCommand]
    private void RemovePlayer(PlayerViewModel player)
    {
        if (player == null) return;

        _allPlayers.Remove(player);
        FilteredPlayers.Remove(player);
    }
}