using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Linq;
using TestSqlGilmi.Data;
using TestSqlGilmi.Models;
using TestSqlGilmi.ViewModels;

namespace TestSqlGilmi;

public partial class UserUC : UserControl
{
    public UserUC()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        LoadData();
    }

    private void LoadData()
    {

       var users = App.DbContext.Users.ToList();
       MainDataGridUsers.ItemsSource = users;
    }

    private async void DataGrid_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        var selectedUser = MainDataGridUsers.SelectedItem as User;

        if (selectedUser == null) return;

        UserVariableData.seletedUserInMainWindow = selectedUser;

        var parent = this.VisualRoot as Window;
        if (parent == null) return;
        var createAndChangeUserWindow = new CreateAndChangeUser();
        await createAndChangeUserWindow.ShowDialog(parent);

        var viewModel = DataContext as MainWindowViewModel;
        viewModel.RefreshData();

    }

    private async void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        UserVariableData.seletedUserInMainWindow = null;

        var createAndChangeUserWindow = new CreateAndChangeUser();
        var parent = this.VisualRoot as Window;

        await createAndChangeUserWindow.ShowDialog(parent);

        var viewModel = DataContext as MainWindowViewModel;
        viewModel.RefreshData();
    }
}