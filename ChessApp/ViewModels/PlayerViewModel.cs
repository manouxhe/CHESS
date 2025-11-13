using CommunityToolkit.Mvvm.ComponentModel;
using ChessApp.Models;
using ChessApp.Service;

namespace ChessApp.ViewModels;

public partial class PlayerViewModel : ViewModelBase
{
    private readonly Player _playerModel;
    [ObservableProperty]
    private string? _firstName;
    [ObservableProperty]
    private string? _lastName;
    [ObservableProperty]
    private string? _email;

    public PlayerViewModel(Player playerModel)
    {
        _playerModel = playerModel;

        _firstName = _playerModel.FirstName;
        _lastName = _playerModel.LastName;
        _email = _playerModel.Email;
    }

}

