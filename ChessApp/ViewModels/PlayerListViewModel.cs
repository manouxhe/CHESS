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
        NewPlayerRankName = _game.DefaultRankName;
        NewPlayerRankValue = _game.DefaultRankStartValue.ToString();


        // Données pour le designer Avalonia, comme dans la ToDoList
        if (Design.IsDesignMode)
        {
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

        }
    }

    public PlayerListViewModel(Game game)
    {
        _game = game;
        NewPlayerRankName = _game.DefaultRankName;
        NewPlayerRankValue = _game.DefaultRankStartValue.ToString();


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
           NewPlayerFirstName, NewPlayerLastName, NewPlayerEmail
       );
        var newPlayerVM = new PlayerViewModel(newPlayerModel);
        _allPlayers.Add(newPlayerVM);

        NewPlayerFirstName = NewPlayerLastName = NewPlayerEmail = null;
        NewPlayerRankValue = _game.DefaultRankStartValue.ToString();
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