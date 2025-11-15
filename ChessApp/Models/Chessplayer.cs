using System.Collections.Generic;

namespace ChessApp.Models;

public class Eloranking : Ranking
{
    // Nom du classement (ex: "ELO", "ELO Blitz", etc.)
    public string Name { get; }

    // Valeur numérique du classement
    public int Value { get; set; }

    
        public Eloranking(string name, int value = 400)
    {
        Name = name;
        Value = value;
    }
}
public class Chessplayer : Player
{
    // Informations d'identité de base
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }

    // Ensemble des classements associés à ce joueur (ELO, Blitz, etc.)
    // La clé e= classement, la valeur est un objet Ranking.
    public Dictionary<string, Ranking> Rankings { get; }
        = new Dictionary<string, Ranking>();
}