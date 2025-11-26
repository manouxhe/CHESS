using System;
using System.Collections.Generic;

namespace ChessApp.Models;

public interface Player
{
    // Nom et prénom du joueur et Adresse mail du joueur
    string? FirstName { get; set; }
    string? LastName { get; set; }

    
    string? Email { get; set; }
    int MatchCount { get; set; }
    DateTime BirthDate { get; set; } 

    // Dictionnaire des classements (ELO, ATP, etc.)
    //   Rankings["ELO_FIDE"] = new EloRanking("ELO FIDE", 2500);
    Dictionary<string, Ranking> Rankings { get; }
    List<Playercustomfield> Fields { get; }
}