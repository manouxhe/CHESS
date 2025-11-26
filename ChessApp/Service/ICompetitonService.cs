using System;
using System.Collections.Generic;
using ChessApp.Models;

namespace ChessApp.Service;

// juste la liste des fcts que le service doit avoir
public interface ICompetitionService
{
    Competition CreateCompetition(string name, DateTime date, string location); // creer une compet

    void DeleteCompetition(Guid id); // supprimer via id

    List<Competition> GetCompetitions(); // renv toutes les compet

    void RegisterPlayer(Competition competition, Player player); // inscrire joueur

    void AddMatch(Competition competition, Match match); // ajouter un match
}
