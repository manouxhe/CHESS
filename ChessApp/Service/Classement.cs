using System.Collections.Generic;
using ChessApp.ViewModels;

namespace ChessApp.Service;

public interface Classement
{
    // Renvoie la liste des catégories disponibles
    Dictionary<string, string> GetCategoriesRank();

    // Calcule le classement pour une catégorie donnée (rankingKey)
    List<ClassementItemViewModel> CalculateClassement(IEnumerable<PlayerViewModel> players, string rankingKey);
}