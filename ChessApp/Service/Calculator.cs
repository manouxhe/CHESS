using ChessApp.Models;

namespace ChessApp.Service;

public interface Calculator
{
    void UpdateRankings(Player p1, Player p2, double score1, double score2);

    string GetRankingKey();
}