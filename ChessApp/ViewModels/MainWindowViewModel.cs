using ChessApp.Service;

namespace ChessApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public PlayerListViewModel PlayerList { get; }
    public ClassementViewModel Classement { get; }
    public CompetitionListViewModel CompetitionList { get; }

    public MainWindowViewModel()
    {
        Game game = new Gamechess();
        ICompetitionService competitionService = new CompetitionService();

        PlayerList = new PlayerListViewModel(game);

        Classement = PlayerList.ClassementVM; 

        CompetitionList = new CompetitionListViewModel(competitionService);
    }
}
