namespace ChessApp.Models;


// Représente un attribut spécifique à un joueur d’échecs : son identifiant FIDE.
public class Fideidfield : Playercustomfield
{
    public string Key => "fide_id";

    public string DisplayName => "FIDE ID";

    public string? Value { get; set; }
    public bool IsNumeric => true;

    public Fideidfield(string? value = null)
    {
        Value = value;
    }

}

