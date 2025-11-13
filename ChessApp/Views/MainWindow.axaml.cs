using Avalonia.Controls;
using ChessApp.ViewModels;
using ChessApp.Service;

namespace ChessApp.Views;

public partial class MainWindow : Window
{
    private PlayerListViewModel? _viewModel;
    public MainWindow()
    {
        InitializeComponent();
        Game factory = new Gamechess();

        // Création du ViewModel principal
        _viewModel = new PlayerListViewModel(factory);

        // Liaison entre la Vue et le ViewModel
        // Le DataContext est essentiel en MVVM : il relie la vue aux données.
        this.DataContext = _viewModel;

    }
}