using ChessApp.Models;

namespace ChessApp.Service;

public interface Game
{
    Player CreatePlayer(string? firstName, string? lastName, string? email);

    // Le nom du classement par défaut (ex: "ELO")
    string DefaultRankName { get; }
    
    // La valeur de départ par défaut (ex: 400)
    int DefaultRankStartValue { get; }
}