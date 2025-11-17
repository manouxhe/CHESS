using System.Collections.Generic;
using ChessApp.Models;
using ChessApp.ViewModels;

namespace ChessApp.Service;

public interface Game
{

    // Fournit la liste des champs personnalisés à afficher dans le formulaire d’inscription d’un joueur.:
    // Pour les échecs : FIDE ID, ELO initial
    // Retourne :
    // Une liste de `FieldViewModel` décrivant chaque champ :
    // clé interne (key)
    // nom à afficher (displayName)
    // valeur par défaut

    List<FieldViewModel> GetFields();
    Player CreatePlayer(string? firstName, string? lastName, string? email, List<FieldViewModel> customFields); // les champs personnalisés définis par GetFields().


}