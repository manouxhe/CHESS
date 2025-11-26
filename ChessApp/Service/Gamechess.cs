using System;
using System.Collections.Generic;
using System.Linq;
using ChessApp.Models;
using ChessApp.ViewModels;


namespace ChessApp.Service;

public class Gamechess : Game
{
    public List<FieldViewModel> GetFields()
    {
        return new List<FieldViewModel>
        {
            // Champ 1 Identifiant FIDE
            new FieldViewModel(
                key: "fide_id",
                displayName: "FIDE ID",
                defaultValue: "",
                isNumeric: true
            ),

            // Champ 2 Classement ELO initial
            new FieldViewModel(
                key: "initial_elo",
                displayName: "ELO Initial",
                defaultValue: "400",
                isNumeric: true
            )
        };
    }
    public Player CreatePlayer(string? firstName, string? lastName, string? email, DateTime birthDate, List<FieldViewModel> customFields)
    {


        string fideid = customFields.FirstOrDefault(f => f.Key == "fide_id")?.Value ?? "Inconnu";
        int elo = int.TryParse(customFields.FirstOrDefault(f => f.Key == "initial_elo")?.Value, out var e) ? e : 1200;

        var fields = new List<Playercustomfield>
        {
            new Fideidfield(fideid) 
            // Ex: on pourrait plus tard ajouter new ClubAttribute("Nom du club")
        };


        var player = new Chessplayer(fields)
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            BirthDate = birthDate,
            MatchCount = 0
        };

        //Ajout du classement initial (ELO FIDE)
        player.Rankings.Add("ELO_FIDE", new Eloranking("ELO FIDE", elo));

        return player;
    }

}