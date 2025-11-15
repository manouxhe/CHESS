using ChessApp.Models;

namespace ChessApp.Service;

public class Gamechess : Game
{
    private string DefaultEloName = "ELO";
    private int DefaultEloValue = 400;

    public string DefaultRankName => DefaultEloName;
    public int DefaultRankStartValue => DefaultEloValue;

    public Player CreatePlayer(string? firstName, string? lastName, string? email)
    {
        var player = new Chessplayer()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };
        player.Rankings.Add(DefaultEloName, new Eloranking(DefaultEloName, DefaultEloValue));

        //player.Rankings.Add("ELO_FIDE", new Eloranking("ELO", 400));

        return player;
    }

}