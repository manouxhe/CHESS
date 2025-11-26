using System;
using System.Collections.Generic;
using System.Linq;
using ChessApp.Models;

namespace ChessApp.Service;

// ici le service qui gere vraiment les compet
public class CompetitionService : ICompetitionService
{
    // liste interne pour stock les compet
    private readonly List<Competition> _competitions = new();

    public Competition CreateCompetition(string name, DateTime date, string location)
    {
        // creer la nouvelle compet
        var newComp = new Competition(name, date, location);
        
        // on l'ajoute a la liste interne
        _competitions.Add(newComp);
        
        // renv pour pouvoir l'utiliser direct
        return newComp;
    }

    public void DeleteCompetition(Guid id)
    {
        // trouver la compet a suppr
        var toDelete = _competitions.FirstOrDefault(c => c.Id == id);

        // si existe on retire
        if (toDelete != null)
            _competitions.Remove(toDelete);
    }

    public List<Competition> GetCompetitions()
    {
        // renv la liste actuelle
        return _competitions;
    }

    public void RegisterPlayer(Competition competition, Player player)
    {
        // on verif si le joueur pas deja dedans
        if (!competition.Players.Contains(player))
        {
            competition.Players.Add(player);
        }
    }

    public void AddMatch(Competition competition, Match match)
    {
        // ajout du match dans la compet
        competition.Matches.Add(match);
    }
}
