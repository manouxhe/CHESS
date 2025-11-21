using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ChessApp.Models;
using ChessApp.Service;
using System.ComponentModel.DataAnnotations;

namespace ChessApp.ViewModels;

public partial class PlayerViewModel : ViewModelBase
{

    private readonly Player _playerModel;
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Le prénom est requis.")]
    private string? _firstName;
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Le nom est requis.")]
    private string? _lastName;
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [EmailAddress(ErrorMessage = "Email invalide.")]
    private string? _email;
    public ObservableCollection<RankingViewModel> Rankings { get; } = new();
    public ObservableCollection<PlayercustomfieldViewModel> Fields { get; } = new();


    public PlayerViewModel(Player playerModel)
    {
        _playerModel = playerModel;

        _firstName = _playerModel.FirstName;
        _lastName = _playerModel.LastName;
        _email = _playerModel.Email;

        Rankings.Clear();
        foreach (var rankModel in _playerModel.Rankings.Values)
        {
            var rankViewModel = new RankingViewModel(rankModel);

            Rankings.Add(rankViewModel);
        }
        Fields.Clear();
        foreach (var fieldModel in _playerModel.Fields)
        {
            var fieldViewModel = new PlayercustomfieldViewModel(fieldModel);
            Fields.Add(fieldViewModel);
        }
        //On valide au démarrage pour vérifier si les données chargées sont correctes
        ValidateAllProperties();
    }


}

