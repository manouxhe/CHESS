using ChessApp.Service;

namespace ChessApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public PlayerListViewModel PlayerList { get; }
    public ClassementViewModel Classement { get; }

    public MainWindowViewModel()
    {
        Game game = new Gamechess();
        Classement classementService = new ChessClassement();

        Calculator chessCalc = new EloCalculator();

        PlayerList = new PlayerListViewModel(game, classementService, chessCalc);

        Classement = PlayerList.ClassementVM;
    }
}