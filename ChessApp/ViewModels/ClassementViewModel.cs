using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ChessApp.Service;

namespace ChessApp.ViewModels;

public partial class ClassementViewModel : ViewModelBase
{
    private readonly Classement _classement;

    // Cache des joueurs bruts venant de PlayerListViewModel
    private IEnumerable<PlayerViewModel> _players = new List<PlayerViewModel>();

    // Cache du classement complet calculé (pour pouvoir filtrer dessus)
    private List<ClassementItemViewModel> _allItems = new();

    public ObservableCollection<KeyValuePair<string, string>> Categories { get; } = new();

    // La liste affichée à l'écran (filtrée)
    public ObservableCollection<ClassementItemViewModel> Items { get; } = new();

    [ObservableProperty]
    private string? _CategoryKey;

    [ObservableProperty]
    private string? _CategoryName;

    [ObservableProperty]
    private string? _searchText;

    public ClassementViewModel(Classement classement)
    {
        _classement = classement;

        var categories = _classement.GetCategoriesRank();
        foreach (var c in categories) Categories.Add(c);

        if (Categories.Any())
        {
            CategoryKey = Categories.First().Value;
            CategoryName = Categories.First().Key;
        }
    }

    [RelayCommand]
    private void Search()
    {
        RefreshFilteredList(SearchText);
    }

    [RelayCommand]
    private void ChangeCategory(string key)
    {
        CategoryKey = key;
        var cat = Categories.FirstOrDefault(c => c.Value == key);
        if (!string.IsNullOrEmpty(cat.Key)) CategoryName = cat.Key;


        UpdateList();
    }


    public void Refresh(IEnumerable<PlayerViewModel> players)
    {
        _players = players;
        UpdateList();
    }

    // Recalcule le classement complet(exemple: après un changement de catégorie ou de données)
    private void UpdateList()
    {
        if (string.IsNullOrEmpty(CategoryKey)) return;

        //Calcul via le service
        var rankingList = _classement.CalculateClassement(_players, CategoryKey);


        _allItems.Clear();
        foreach (var item in rankingList)
        {

            _allItems.Add(new ClassementItemViewModel(item.Position, item.OriginalPlayer, item.Score));
        }

        // Application du filtre de recherche actuel
        RefreshFilteredList(SearchText);
    }


    public void RefreshFilteredList(string? filter = null)
    {
        Items.Clear();

        IEnumerable<ClassementItemViewModel> itemsToShow;

        if (string.IsNullOrWhiteSpace(filter))
        {
            itemsToShow = _allItems;
        }
        else
        {
            itemsToShow = _allItems.Where(item =>
                item.PlayerName.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                item.Position.ToString().Contains(filter)
            );
        }

        foreach (var item in itemsToShow)
        {
            Items.Add(item);
        }
    }
}