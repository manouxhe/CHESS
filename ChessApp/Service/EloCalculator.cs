using System;
using ChessApp.Models;

namespace ChessApp.Service;

public class EloCalculator : Calculator
{
    public string GetRankingKey() => "ELO_FIDE";

    public void UpdateRankings(Player p1, Player p2, double score1, double score2)
    {
        if (p1.Rankings.TryGetValue(GetRankingKey(), out var r1) &&
            p2.Rankings.TryGetValue(GetRankingKey(), out var r2))
        {
            // Logique spécifique ELO
            int k1 = GetK(p1, r1);
            int k2 = GetK(p2, r2);

            double expected1 = 1.0 / (1 + Math.Pow(10, (r2.Value - r1.Value) / 400.0));
            double expected2 = 1.0 / (1 + Math.Pow(10, (r1.Value - r2.Value) / 400.0));

            r1.Value += (int)Math.Round(k1 * (score1 - expected1));
            r2.Value += (int)Math.Round(k2 * (score2 - expected2));

            p1.MatchCount++;
            p2.MatchCount++;
        }
    }

    private int GetK(Player p, Ranking r)
    {
        if (p.MatchCount < 30) return 40;
        if (r.Value >= 2400) return 10;
        return 20;
    }
}