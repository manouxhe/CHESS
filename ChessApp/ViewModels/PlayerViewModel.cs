using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ChessApp.Models;
using ChessApp.Service;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;

namespace ChessApp.ViewModels;

public partial class PlayerViewModel : ViewModelBase
{

    private readonly Player _playerModel;
    public Player PlayerModel => _playerModel;
    private readonly Action? _onInfoChanged; //pour notifier un changement

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
    public int MatchCount
    {
        get => _playerModel.MatchCount;
        set
        {
            if (_playerModel.MatchCount != value)
            {
                _playerModel.MatchCount = value;
                OnPropertyChanged(nameof(MatchCount)); // Important ici aussi
            }
        }
    }
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Date requise")]
    private DateTimeOffset? _birthDate;
    public ObservableCollection<RankingViewModel> Rankings { get; } = new();
    public ObservableCollection<PlayercustomfieldViewModel> Fields { get; } = new();


    public PlayerViewModel(Player playerModel, Action? onInfoChanged = null)
    {
        _playerModel = playerModel;
        _onInfoChanged = onInfoChanged;

        _firstName = _playerModel.FirstName;
        _lastName = _playerModel.LastName;
        _email = _playerModel.Email;
        _birthDate = new DateTimeOffset(_playerModel.BirthDate);

        Rankings.Clear();
        foreach (var rankModel in _playerModel.Rankings.Values)
        {
            var rankViewModel = new RankingViewModel(rankModel);
            // Si un ELO change, on appelle l'action du parent
            rankViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(RankingViewModel.StringValue))
                    _onInfoChanged?.Invoke();
            };

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

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(BirthDate) && BirthDate.HasValue)
            _playerModel.BirthDate = BirthDate.Value.DateTime;

        // Si le nom change (ordre alphabétique du classement), on prévient le parent
        if (e.PropertyName == nameof(FirstName) || e.PropertyName == nameof(LastName))
        {
            _onInfoChanged?.Invoke();
        }
    }
    public void RefreshStats()
    {
        OnPropertyChanged(nameof(MatchCount));

        foreach (var rankingVM in Rankings)
        {
            rankingVM.Refresh();
        }

        OnPropertyChanged(nameof(Rankings));
    }
}

