using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ChessApp.Models;
using ChessApp.Service;

namespace ChessApp.ViewModels;

public partial class PlayerListViewModel : ViewModelBase
{
    private readonly Game _game;      // Fabrique de joueurs et comparateurs (dépend du jeu : échecs, tennis, etc.)


    // Collection complète
    private readonly ObservableCollection<PlayerViewModel> _allPlayers = new();
    public ObservableCollection<PlayerViewModel> Players => _allPlayers;

    public PlayerListViewModel()
    {

        _game = new Gamechess();


        // Données pour le designer Avalonia, comme dans la ToDoList
        if (Design.IsDesignMode)
        {
            var p1 = new Chessplayer()
            {
                FirstName = "Magnus (Design)",
                LastName = "Carlsen",
                Email = "magnus@chess.com",

            };


            var p2 = new Chessplayer()
            {
                FirstName = "Hikaru (Design)",
                LastName = "Nakamura",
                Email = "hika@chess.com",

            };


            _allPlayers.Add(new PlayerViewModel(p1));
            _allPlayers.Add(new PlayerViewModel(p2));

        }
    }

    public PlayerListViewModel(Game game)
    {
        _game = game;


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

    [RelayCommand(CanExecute = nameof(CanAddPlayer))]
    private void AddPlayer()
    {
        var newPlayerModel = _game.CreatePlayer(
           NewPlayerFirstName, NewPlayerLastName, NewPlayerEmail
       );
        var newPlayerVM = new PlayerViewModel(newPlayerModel);
        _allPlayers.Add(newPlayerVM);

        NewPlayerFirstName = NewPlayerLastName = NewPlayerEmail = null;
    }

    private bool CanAddPlayer() =>
            !string.IsNullOrWhiteSpace(NewPlayerFirstName) &&
            !string.IsNullOrWhiteSpace(NewPlayerLastName);

    [RelayCommand]
    private void RemovePlayer(PlayerViewModel player)
    {
        if (player == null) return;


        _allPlayers.Remove(player);
    }

}