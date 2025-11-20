using System;
using System.Collections.Generic;

namespace ChessApp.Models;

public class Match
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Player? WhitePlayer { get; set; }  //peut être null au début car peut pas predire joueur qui y sera
    public Player? BlackPlayer { get; set; }

    public string Result { get; set; } = "Not played"; 
    // par défaut

    public List<string> Moves { get; set; } = new List<string>();   //stocker chaque moov sous forme de texte position pion (match.Moves.Add("e4");)
}
//pas de constructeur , a voir ce quon veut rendre oblligatoire pour les match (joueurs date..;)