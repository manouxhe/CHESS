namespace ChessApp.Models;

public interface Player
{
    // Nom et prénom du joueur et Adresse mail du joueur
    string? FirstName { get; set; }
    string? LastName { get; set; }

    string? Email { get; set; }
}