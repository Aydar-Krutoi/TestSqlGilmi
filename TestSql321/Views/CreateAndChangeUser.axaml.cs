using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Linq;
using TestSqlGilmi.Data;
using TestSqlGilmi.Models;

namespace TestSqlGilmi;

public partial class CreateAndChangeUser : Window
{
    public CreateAndChangeUser()
    {
        InitializeComponent();
        LoadRoles();
        LoadUserData();
    }

    private void LoadRoles()
    {
        var roles = App.DbContext.Roles.ToList();
        RoleComboBox.ItemsSource = roles;
    }

    private void LoadUserData()
    {
        if (UserVariableData.seletedUserInMainWindow == null) return;

        var user = UserVariableData.seletedUserInMainWindow;
        FullNameText.Text = user.FullName;
        PhoneNumberText.Text = user.PhoneNumber;
        DescriptionText.Text = user.Descipition;

        var login = App.DbContext.Logins.FirstOrDefault(l => l.UserId == user.IdUser);
        if (login != null)
        {
            LoginText.Text = login.Login1;
            PasswordText.Text = login.Password;
        }

        
        if (user.RolesId != null)
        {
            var selectedRole = RoleComboBox.ItemsSource
                .OfType<Role>()
                .FirstOrDefault(r => r.IdRoles == user.RolesId);
            RoleComboBox.SelectedItem = selectedRole;
        }
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FullNameText.Text))
        {
            ShowError("Поле 'ФИО' обязательно для заполнения");
            return;
        }

        if (string.IsNullOrWhiteSpace(PhoneNumberText.Text))
        {
            ShowError("Поле 'Номер телефона' обязательно для заполнения");
            return;
        }

        if (string.IsNullOrWhiteSpace(LoginText.Text))
        {
            ShowError("Поле 'Логин' обязательно для заполнения");
            return;
        }

        if (string.IsNullOrWhiteSpace(PasswordText.Text))
        {
            ShowError("Поле 'Пароль' обязательно для заполнения");
            return;
        }

        if (RoleComboBox.SelectedItem == null)
        {
            ShowError("Необходимо выбрать роль");
            return;
        }

        var selectedRole = (Role)RoleComboBox.SelectedItem;

        try
        {
            if (UserVariableData.seletedUserInMainWindow != null)
            {
                var user = App.DbContext.Users
                    .FirstOrDefault(u => u.IdUser == UserVariableData.seletedUserInMainWindow.IdUser);

                if (user != null)
                {
                    user.FullName = FullNameText.Text;
                    user.PhoneNumber = PhoneNumberText.Text;
                    user.Descipition = DescriptionText.Text;
                    user.RolesId = selectedRole.IdRoles;

                    var login = App.DbContext.Logins.FirstOrDefault(l => l.UserId == user.IdUser);
                    if (login != null)
                    {
                        login.Login1 = LoginText.Text;
                        login.Password = PasswordText.Text;
                    }
                    else
                    {
                        var newLogin = new Login()
                        {
                            Login1 = LoginText.Text,
                            Password = PasswordText.Text,
                            UserId = user.IdUser
                        };
                        App.DbContext.Logins.Add(newLogin);
                    }
                }
            }
            else
            {
                var newUser = new User()
                {
                    FullName = FullNameText.Text,
                    PhoneNumber = PhoneNumberText.Text,
                    Descipition = DescriptionText.Text,
                    RolesId = selectedRole.IdRoles
                };

                App.DbContext.Users.Add(newUser);
                App.DbContext.SaveChanges();

                var newLogin = new Login()
                {
                    Login1 = LoginText.Text,
                    Password = PasswordText.Text,
                    UserId = newUser.IdUser
                };

                App.DbContext.Logins.Add(newLogin);
            }

            App.DbContext.SaveChanges();
            this.Close();
        }
        catch (Exception ex)
        {
            ShowError($"Ошибка: {ex.Message}");
        }
    }

    private void ShowError(string message)
    {
        var messageBox = new Window
        {
            Title = "Ошибка",
            Content = new TextBlock { Text = message, Margin = new Avalonia.Thickness(20) },
            SizeToContent = SizeToContent.WidthAndHeight,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        messageBox.ShowDialog(this);
    }
}