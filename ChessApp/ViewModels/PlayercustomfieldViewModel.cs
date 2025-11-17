using CommunityToolkit.Mvvm.ComponentModel;
using ChessApp.Models;

namespace ChessApp.ViewModels;


// ViewModel pour gérer un seul champs (ex: "FIDE ID", "Club")
public partial class PlayercustomfieldViewModel : ViewModelBase
{
    private readonly Playercustomfield _fieldcustomModel;

    
    // Nom lisible pour la vue (ex: "FIDE ID", "Club").
 
    public string DisplayName => _fieldcustomModel.DisplayName;

   
    // Clé interne pour lier ce champ au modèle (ex: "fide_id").

    public string Key => _fieldcustomModel.Key;



    [ObservableProperty]
   
    private string? _value;
   

    public PlayercustomfieldViewModel(Playercustomfield fieldcustomModel)
    {
        _fieldcustomModel = fieldcustomModel;
        _value = fieldcustomModel.Value; 

    }
    
}
