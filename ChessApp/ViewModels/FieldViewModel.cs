using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;



namespace ChessApp.ViewModels;

// ViewModel représentant un champ de formulaire d’inscription
public partial class FieldViewModel : ViewModelBase
{

    public string Key { get; }
    public string DisplayName { get; }
    public bool IsNumeric { get; }

    private readonly string? _defaultValue;


    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Ce champ est requis.")]
    [CustomValidation(typeof(FieldViewModel), nameof(ValidateNumeric))]
    private string? _value;

    public FieldViewModel(
        string key,
        string displayName,
        string? defaultValue = "",
        bool isNumeric = false)
    {
        Key = key;
        DisplayName = displayName;
        _defaultValue = defaultValue;
        _value = defaultValue;
        IsNumeric = isNumeric;

    }

    // Réinitialise la valeur du champ à sa valeur par défaut.
    public void Reset() => Value = _defaultValue;

    public static ValidationResult? ValidateNumeric(string? value, ValidationContext context)
    {

        if (context.ObjectInstance is FieldViewModel field && field.IsNumeric)
        {
            // Si c'est numérique mais que le texte n'est pas un nombre entier valide
            if (!string.IsNullOrEmpty(value) && !int.TryParse(value, out _))
            {
                return new ValidationResult("Ce champ doit être un nombre entier (ex: 1200).");
            }
        }

        return ValidationResult.Success;
    }
}
