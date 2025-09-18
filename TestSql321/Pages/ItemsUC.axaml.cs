using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Linq;
using TestSqlGilmi.Data;
using TestSqlGilmi.Models;
using TestSqlGilmi.ViewModels;

namespace TestSqlGilmi;

public partial class ItemsUC : UserControl
{
    public ItemsUC()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private async void DataGrid_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        var selectedItem = MainDataGridItems.SelectedItem as Item;
        if (selectedItem == null) return;

        var parent = this.VisualRoot as Window;
        if (parent == null) return;

        var createAndChangeItemWindow = new CreateAndChangeItem(selectedItem);
        await createAndChangeItemWindow.ShowDialog(parent);

        var viewModel = DataContext as MainWindowViewModel;
        viewModel?.RefreshItemsData();
    }

    private async void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var parent = this.VisualRoot as Window;
        if (parent != null)
        {
            var createAndChangeItemWindow = new CreateAndChangeItem(null);
            await createAndChangeItemWindow.ShowDialog(parent);
            var viewModel = DataContext as MainWindowViewModel;
            viewModel?.RefreshItemsData();
        }
    }
}