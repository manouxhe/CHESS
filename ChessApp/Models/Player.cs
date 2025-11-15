using System.Collections.Generic;

namespace ChessApp.Models;

public interface Player
{
    // Nom et prénom du joueur
    string? FirstName { get; set; }
    string? LastName { get; set; }

    // Adresse mail du joueur
    string? Email { get; set; }

    // Dictionnaire des classements (ELO, ATP, etc.)
    // La clé = (ex: "ELO"), la valeur est un objet Ranking.
    // Exemple :
    //   Rankings["ELO"] = new EloRanking("ELO", 2500);
    Dictionary<string, Ranking> Rankings { get; }
}