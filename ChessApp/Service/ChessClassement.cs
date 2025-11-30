using System.Collections.Generic;
using System.Linq;
using ChessApp.ViewModels;

namespace ChessApp.Service;

// CERVEAU : L'implémentation spécifique aux Échecs (ELO).
public class ChessClassement : Classement
{
    public Dictionary<string, string> GetCategoriesRank()
    {
        return new Dictionary<string, string>
        {
            { "ELO", "ELO FIDE" },
            { "ELO Blitz", "ELO_BLITZ" }   // Exemple de futur classement
        };
    }

    public List<ClassementItemViewModel> CalculateClassement(IEnumerable<PlayerViewModel> players, string rankingKey)
    {
        //On projette et on trie
        var sortedPlayers = players
            .Select(p => new
            {
                Player = p,
                Score = GetScoreValue(p, rankingKey)
            })
            .OrderByDescending(x => x.Score)     // Score décroissant
            .ThenBy(x => x.Player.LastName)      // Nom A-Z
            .ThenBy(x => x.Player.FirstName)     // Prénom A-Z
            .ToList();

        //On construit la liste finale avec la position (1, 2, 3...)
        var classement = new List<ClassementItemViewModel>();
        int rank = 1;

        foreach (var item in sortedPlayers)
        {

            classement.Add(new ClassementItemViewModel(rank, item.Player, item.Score.ToString()));
            rank++;
        }

        return classement;
    }

    // Récupère la valeur numérique du classement demandé
    private int GetScoreValue(PlayerViewModel player, string key)
    {

        var rankVM = player.Rankings.FirstOrDefault(r => r.Name == key);

        if (rankVM != null && int.TryParse(rankVM.StringValue, out int value))
        {
            return value;
        }
        return 0;
    }
}