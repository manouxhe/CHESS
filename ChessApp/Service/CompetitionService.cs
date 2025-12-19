using System;
using System.Collections.Generic;
using System.Linq;
using ChessApp.Models;

namespace ChessApp.Service;

public class CompetitionService : ICompetitionService
{
    private readonly List<Competition> _competitions = new();
    private int _nextId = 1;

    public Competition CreateCompetition(string name, DateTime? start, DateTime? end, string location)
    {
        var newComp = new Competition(_nextId++, name, start, end, location);
        _competitions.Add(newComp);
        return newComp;
    }

    public void DeleteCompetition(int id)
    {
        var toDelete = _competitions.FirstOrDefault(c => c.Id == id);
        if (toDelete != null) _competitions.Remove(toDelete);
    }

    public List<Competition> GetCompetitions() => _competitions;

    public void RegisterPlayer(Competition competition, Player player)
    {
        if (!competition.Players.Any(p => p.Email == player.Email))
            competition.Players.Add(player);
    }

    public void AddMatch(Competition competition, Match match) => competition.Matches.Add(match);
}