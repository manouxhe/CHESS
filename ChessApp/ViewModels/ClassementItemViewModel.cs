namespace ChessApp.ViewModels;

public class ClassementItemViewModel : ViewModelBase
{
    public int Position { get; }
    public string PlayerName { get; }
    public string Score { get; }
    public PlayerViewModel OriginalPlayer { get; }

    public ClassementItemViewModel(int position, PlayerViewModel playerVM, string score)
    {
        Position = position;
        OriginalPlayer = playerVM;
        Score = score;
        PlayerName = $"{playerVM.FirstName} {playerVM.LastName}";
    }
}