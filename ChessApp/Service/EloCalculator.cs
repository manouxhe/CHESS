using System;
using ChessApp.Models;

namespace ChessApp.Service;

// calc du elo apres un match
public static class EloCalculator
{
    // choisit le k selon regles 
    private static int GetK(Player p, Ranking r)
    {
        // si peu de matchs moins de 30 : k = 40
        if (p.MatchCount < 30)
            return 40;

        // si tres fort joueur : k = 10
        if (r.Value >= 2400)
            return 10;

        // sinon valeur standard
        return 20;
    }

    // met a jour le elo d un joueur
    public static void UpdateElo(Player p, Ranking rankingPlayer, Ranking rankingOpp, double score)
    {
        int k = GetK(p, rankingPlayer);

        double expected = 1.0 / (1 + Math.Pow(10, (rankingOpp.Value - rankingPlayer.Value) / 400.0));

        double newElo = rankingPlayer.Value + k * (score - expected);

        rankingPlayer.Value = (int)Math.Round(newElo);

        // on incremente nb matchs du joueur
        p.MatchCount++;
    }
}
