using System.Collections.Generic;
using System.Collections.ObjectModel;
using ChessApp.Models;

namespace ChessApp.ViewModels;

// viewmodel pour gerer une liste de parties
public class MatchListViewModel : ViewModelBase
{
    public ObservableCollection<MatchViewModel> Matches { get; } = new();

    public MatchListViewModel(IEnumerable<Match> matchList)
    {
        foreach (var match in matchList)
        {
            Matches.Add(new MatchViewModel(match));
        }
    }

    // ajout d une nouvelle partie
    public void AddMatch(Match match)
    {
        Matches.Add(new MatchViewModel(match));
    }
}
