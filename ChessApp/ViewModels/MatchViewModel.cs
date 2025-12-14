using CommunityToolkit.Mvvm.ComponentModel;
using ChessApp.Models;

namespace ChessApp.ViewModels;

// viewmodel pour une seule partie
public partial class MatchViewModel : ViewModelBase
{
    private readonly Match _matchModel;

    public MatchViewModel(Match matchModel)
    {
        _matchModel = matchModel;
        _result = matchModel.Result;
    }

    // joueurs (lecture seule pour la vue)
    public Player? WhitePlayer => _matchModel.WhitePlayer;
    public Player? BlackPlayer => _matchModel.BlackPlayer;

    // resultat du match 
    [ObservableProperty]
    private string _result;

    // synchro vm -> model
    partial void OnResultChanged(string value)
    {
        _matchModel.Result = value;
    }

    // acces au model si besoin plus tard
    public Match Model => _matchModel;
}
