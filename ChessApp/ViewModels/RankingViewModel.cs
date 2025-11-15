using CommunityToolkit.Mvvm.ComponentModel;
using ChessApp.Models;

namespace ChessApp.ViewModels;

public partial class RankingViewModel : ViewModelBase
{
    private readonly Ranking _rankingModel;

    
    //Nom du classement (ex : "ELO", "Classement national").
    
    public string Name => _rankingModel.Name;
    [ObservableProperty]
    private string? _stringValue;
    public RankingViewModel(Ranking rankingModel)
    {
        _rankingModel = rankingModel;
        StringValue = rankingModel.Value.ToString(); // ex: 400 → "400"
    }
    public void RevertToModelValue()
    {
        StringValue = _rankingModel.Value.ToString();
    }
}