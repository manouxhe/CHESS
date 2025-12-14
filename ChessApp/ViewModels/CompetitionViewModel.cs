using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ChessApp.Models;
using ChessApp.Service;

namespace ChessApp.ViewModels;

// viewmodel pour gerer une competition
public partial class CompetitionViewModel : ViewModelBase
{
    private readonly Competition _competition;
    private readonly ICompetitionService _competitionService;

    public CompetitionViewModel(Competition competition, ICompetitionService competitionService)
    {
        _competition = competition;
        _competitionService = competitionService;

        // joueurs inscrits
        Players = new ObservableCollection<Player>(_competition.Players);

        // matchs
        Matches = new MatchListViewModel(_competition.Matches);
    }

    // infos competition
    public string Name => _competition.Name;
    public DateTime Date => _competition.Date;
    public string Location => _competition.Location;

    // joueurs inscrits a la competition
    public ObservableCollection<Player> Players { get; }

    // liste des matchs 
    public MatchListViewModel Matches { get; }

    // selection pour creer un match
    [ObservableProperty]
    private Player? _selectedWhitePlayer;

    [ObservableProperty]
    private Player? _selectedBlackPlayer;

    // creer un nouveau match
    [RelayCommand(CanExecute = nameof(CanCreateMatch))]
    private void CreateMatch()
    {
        var match = new Match
            {
                WhitePlayer = SelectedWhitePlayer,
                BlackPlayer = SelectedBlackPlayer
            };

        _competitionService.AddMatch(_competition, match);
        Matches.AddMatch(match);

        SelectedWhitePlayer = null;
        SelectedBlackPlayer = null;
    }

    private bool CanCreateMatch()
    {
        return SelectedWhitePlayer != null
            && SelectedBlackPlayer != null
            && SelectedWhitePlayer != SelectedBlackPlayer;
    }

    // encoder le resultat et mettre a jour les elo
    public void SetMatchResult(Match match, double scoreWhite, double scoreBlack)
    {
        if (match.WhitePlayer == null || match.BlackPlayer == null)
            return;

        var white = match.WhitePlayer;
        var black = match.BlackPlayer;

        var whiteRank = white.Rankings["ELO_FIDE"];
        var blackRank = black.Rankings["ELO_FIDE"];

        EloCalculator.UpdateElo(white, whiteRank, blackRank, scoreWhite);
        EloCalculator.UpdateElo(black, blackRank, whiteRank, scoreBlack);

        match.Result = scoreWhite > scoreBlack ? "1-0"
                     : scoreWhite < scoreBlack ? "0-1"
                     : "0.5-0.5";
    }
}
