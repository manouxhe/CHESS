using CommunityToolkit.Mvvm.ComponentModel;
using ChessApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ChessApp.ViewModels;

public partial class RankingViewModel : ViewModelBase
{
    private readonly Ranking _rankingModel;


    //Nom du classement (ex : "ELO", "Classement national").

    public string Name => _rankingModel.Name;
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "La valeur est requise.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Doit être un nombre entier.")]
    private string? _stringValue;
    public RankingViewModel(Ranking rankingModel)
    {
        _rankingModel = rankingModel;
        StringValue = rankingModel.Value.ToString(); // ex: 400 → "400"
    }

    partial void OnStringValueChanged(string? value)
    {

        ValidateProperty(value, nameof(StringValue));

        if (!HasErrors && int.TryParse(value, out int result))
        {
            _rankingModel.Value = result;
        }
    }
    public void RevertToModelValue()
    {
        StringValue = _rankingModel.Value.ToString();
    }
}