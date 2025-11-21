using CommunityToolkit.Mvvm.ComponentModel;
using ChessApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ChessApp.ViewModels;


// ViewModel pour gérer un seul champs (ex: "FIDE ID", "Club")
public partial class PlayercustomfieldViewModel : ViewModelBase
{
    private readonly Playercustomfield _fieldcustomModel;


    // Nom lisible pour la vue (ex: "FIDE ID", "Club").

    public string DisplayName => _fieldcustomModel.DisplayName;


    // Clé interne pour lier ce champ au modèle (ex: "fide_id").

    public string Key => _fieldcustomModel.Key;

    public bool IsModelNumeric => _fieldcustomModel.IsNumeric;



    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Ne peut pas être vide")]
    [CustomValidation(typeof(PlayercustomfieldViewModel), nameof(ValidateNumeric))]

    private string? _value;


    public PlayercustomfieldViewModel(Playercustomfield fieldcustomModel)
    {
        _fieldcustomModel = fieldcustomModel;
        _value = fieldcustomModel.Value;

    }
    partial void OnValueChanged(string? value)
    {
        ValidateProperty(value, nameof(Value));

        if (!HasErrors)
        {
            _fieldcustomModel.Value = value;
        }
    }

    public static ValidationResult? ValidateNumeric(string? value, ValidationContext context)
    {
        if (context.ObjectInstance is PlayercustomfieldViewModel vm && vm.IsModelNumeric)
        {
            if (!string.IsNullOrEmpty(value) && !int.TryParse(value, out _))
            {
                return new ValidationResult("Doit être un nombre entier.");
            }
        }
        return ValidationResult.Success;
    }

}
