using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ChessApp.Models;
using ChessApp.Service;
using System.Collections.Generic;
using System.Linq;

namespace ChessApp.ViewModels;

// viewmodel pour gerer une competition
public partial class CompetitionViewModel : ViewModelBase
{
    private readonly Competition _competition;
    private readonly ObservableCollection<PlayerViewModel> _globalPlayers;
    private readonly ICompetitionService _competitionService;
    private readonly Calculator _calculator;

    public CompetitionViewModel(Competition competition, ICompetitionService competitionService, ObservableCollection<PlayerViewModel> globalPlayers, Calculator calculator)
    {
        _competition = competition;
        _globalPlayers = globalPlayers;
        _competitionService = competitionService;
        _calculator = calculator;

        inscritPlayers = new ObservableCollection<PlayerViewModel>(
            _competition.Players.Select(p => new PlayerViewModel(p))
        );

        SearchPlayers = new ObservableCollection<PlayerViewModel>();

        // Initialisation de la liste des matchs
        Matches = new MatchListViewModel(_competition.Matches);

        UpdateAvailablePlayers();
    }

    // infos competition
    public int Id => _competition.Id;
    public string? Name { get => _competition.Name; set => SetProperty(_competition.Name, value, _competition, (m, v) => m.Name = v); }
    public DateTime? StartDate { get => _competition.StartDate; set => SetProperty(_competition.StartDate, value, _competition, (m, v) => m.StartDate = v); }
    public DateTime? EndDate { get => _competition.EndDate; set => SetProperty(_competition.EndDate, value, _competition, (m, v) => m.EndDate = v); }
    public string? Location { get => _competition.Location; set => SetProperty(_competition.Location, value, _competition, (m, v) => m.Location = v); }
    // joueurs inscrits a la competition
    public ObservableCollection<PlayerViewModel> inscritPlayers { get; } = new();
    public ObservableCollection<PlayerViewModel> SearchPlayers { get; } = new();
    [ObservableProperty]
    private string? _searchPlayerText;

    // Dès qu'on tape dans la recherche, on filtre la liste des disponibles
    partial void OnSearchPlayerTextChanged(string? value)
    {
        UpdateAvailablePlayers();
    }

    private void UpdateAvailablePlayers()
    {
        if (_globalPlayers == null) return;

        SearchPlayers.Clear();
        var query = _globalPlayers.Where(gp => !inscritPlayers.Any(ep => ep.Email == gp.Email));

        if (!string.IsNullOrWhiteSpace(SearchPlayerText))
        {
            var filter = SearchPlayerText.Trim();
            // Recherche sur le Prénom OU le Nom
            query = query.Where(gp =>
                (gp.FirstName != null && gp.FirstName.Contains(filter, StringComparison.OrdinalIgnoreCase)) ||
                (gp.LastName != null && gp.LastName.Contains(filter, StringComparison.OrdinalIgnoreCase)));
        }

        foreach (var p in query) SearchPlayers.Add(p);
    }

    // Commande pour INSCRIRE un joueur
    [RelayCommand]
    private void AddPlayerToComp(PlayerViewModel pvm)
    {
        if (pvm == null) return;
        if (inscritPlayers.Any(p => p.Email == pvm.Email))
        {
            return;
        }

        _competitionService.RegisterPlayer(_competition, pvm.PlayerModel);
        inscritPlayers.Add(pvm); // On ajoute le ViewModel directement
        UpdateAvailablePlayers();
        CreateMatchCommand.NotifyCanExecuteChanged(); // On prévient que le bouton Match peut changer

    }

    // Commande pour RETIRER un joueur
    [RelayCommand]
    private void RemovePlayerFromComp(PlayerViewModel pvm) 
    {
        if (pvm == null) return;

        _competition.Players.Remove(pvm.PlayerModel);
        inscritPlayers.Remove(pvm);
        UpdateAvailablePlayers();
        CreateMatchCommand.NotifyCanExecuteChanged();
    }

    // liste des matchs 
    public MatchListViewModel Matches { get; }

  
    public List<double> ScoreOptions { get; } = new() { 1, 0, 0.5 };


    // creer un nouveau match
    [ObservableProperty][NotifyCanExecuteChangedFor(nameof(CreateMatchCommand))] private PlayerViewModel? _selectedFirstPlayer;
    [ObservableProperty][NotifyCanExecuteChangedFor(nameof(CreateMatchCommand))] private PlayerViewModel? _selectedSecondPlayer;
    [ObservableProperty][NotifyCanExecuteChangedFor(nameof(CreateMatchCommand))] private double? _selectedFirstScore;
    [ObservableProperty][NotifyCanExecuteChangedFor(nameof(CreateMatchCommand))] private double? _selectedSecondScore;

    [RelayCommand(CanExecute = nameof(CanCreateMatch))]
    private void CreateMatch()
    {
        if (SelectedFirstPlayer == null || SelectedSecondPlayer == null ||
            SelectedFirstScore == null || SelectedSecondScore == null) return;

        // Le VM demande au "Calculator" de faire son travail sans savoir comment
        _calculator.UpdateRankings(SelectedFirstPlayer.PlayerModel, SelectedSecondPlayer.PlayerModel, SelectedFirstScore.Value, SelectedSecondScore.Value);

        SelectedFirstPlayer.RefreshStats();
        SelectedSecondPlayer.RefreshStats();

        var match = new Match
        {
            WhitePlayer = SelectedFirstPlayer.PlayerModel,
            BlackPlayer = SelectedSecondPlayer.PlayerModel,
            WhiteScore = SelectedFirstScore,
            BlackScore = SelectedSecondScore,
            Result = $"{SelectedFirstPlayer.LastName} ({SelectedFirstScore}) - {SelectedSecondPlayer.LastName} ({SelectedSecondScore})"
        };

        _competitionService.AddMatch(_competition, match);
        Matches.AddMatch(match);

        SelectedFirstPlayer = SelectedSecondPlayer = null;
        SelectedFirstScore = SelectedSecondScore = null;
    }

    private bool CanCreateMatch() =>
        inscritPlayers.Count >= 2 &&
        SelectedFirstPlayer != null && SelectedSecondPlayer != null &&
        SelectedFirstPlayer != SelectedSecondPlayer &&
        SelectedFirstScore != null && SelectedSecondScore != null;
}
