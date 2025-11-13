using ChessApp.Models;

namespace ChessApp.Service;

public interface Game
{
    Player CreatePlayer(string? firstName, string? lastName, string? email);
}