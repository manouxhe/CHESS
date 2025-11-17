using CommunityToolkit.Mvvm.ComponentModel; 



namespace ChessApp.ViewModels;

// ViewModel représentant un champ de formulaire d’inscription
public partial class FieldViewModel : ViewModelBase
{
   
    public string Key { get; }
    public string DisplayName { get; }

    private readonly string? _defaultValue;


    [ObservableProperty]
    private string? _value;

    public FieldViewModel(
        string key,
        string displayName,
        string? defaultValue = "")
    {
        Key = key;
        DisplayName = displayName;
        _defaultValue = defaultValue;
        _value = defaultValue;

    }

    // Réinitialise la valeur du champ à sa valeur par défaut.
    public void Reset() => Value = _defaultValue;
}
