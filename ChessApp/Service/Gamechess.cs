using ChessApp.Models;

namespace ChessApp.Service;

public class Gamechess : Game
{
    public Player CreatePlayer(string? firstName, string? lastName, string? email)
    {
        return new Chessplayer
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };
    }

}