namespace ChessApp.Models;


// Représente un champs individuel associé à un joueur.
public interface Playercustomfield
{
    // Identifiant technique unique de du champs.
    string Key { get; }


    // Nom affichable de l’attribut pour l’utilisateur.

    string DisplayName { get; }


    // Valeur actuelle du champs.

    string? Value { get; set; }
    //Est-ce que ce champ doit être un nombre ?
    bool IsNumeric { get; }


}

