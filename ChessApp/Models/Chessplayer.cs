using System;
using System.Collections.Generic;

namespace ChessApp.Models;

public class Eloranking : Ranking
{
    // Nom du classement
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
    public int MatchCount { get; set; } = 0;
    public DateTime BirthDate { get; set; }

    // Ensemble des classements associés à ce joueur (ELO, Blitz, etc.)
    // La clé est le nom du classement, la valeur est un objet Ranking.
    public Dictionary<string, Ranking> Rankings { get; }
        = new Dictionary<string, Ranking>();
    public List<Playercustomfield> Fields { get; }

    public Chessplayer(IEnumerable<Playercustomfield>? fields = null)
    {
        // Si aucune liste n’est fournie, on crée une liste vide.
        Fields = new List<Playercustomfield>(fields ?? new List<Playercustomfield>());
    }
}