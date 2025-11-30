namespace ChessApp.Models;

public interface Ranking
{
    // Nom du classement (ex: "ELO", "ELO Blitz", "Classement National")
    string Name { get; }

    // Valeur numérique du classement (ex: 1200, 2500, etc.)
    int Value { get; set; }
}