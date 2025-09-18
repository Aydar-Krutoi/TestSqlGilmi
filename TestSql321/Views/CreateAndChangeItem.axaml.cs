using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Linq;
using TestSqlGilmi.Data;
using TestSqlGilmi.Models;

namespace TestSqlGilmi;

public partial class CreateAndChangeItem : Window
{
    private readonly Item? _editingItem;

    public CreateAndChangeItem(Item? itemToEdit)
    {
        _editingItem = itemToEdit;
        InitializeComponent();
        LoadItemData();
    }

    private void LoadItemData()
    {
        if (_editingItem == null) return;

        NameText.Text = _editingItem.Name;
        PriceText.Text = _editingItem.Price;
        DescriptionText.Text = _editingItem.Opisanie;
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameText.Text))
        {
            ShowError("Поле 'Name' обязательно для заполнения");
            return;
        }

        if (string.IsNullOrWhiteSpace(PriceText.Text))
        {
            ShowError("Поле 'Price' обязательно для заполнения");
            return;
        }

        try
        {
            if (_editingItem != null)
            {
                var item = App.DbContext.Items
                    .FirstOrDefault(i => i.IdItems == _editingItem.IdItems);

                if (item != null)
                {
                    item.Name = NameText.Text;
                    item.Price = PriceText.Text;
                    item.Opisanie = DescriptionText.Text;
                }
            }
            else
            {
                var newItem = new Item()
                {
                    Name = NameText.Text,
                    Price = PriceText.Text,
                    Opisanie = DescriptionText.Text
                };
                App.DbContext.Items.Add(newItem);
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
            Content = new TextBlock { Text = message, Margin = new Thickness(20) },
            SizeToContent = SizeToContent.WidthAndHeight,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        messageBox.ShowDialog(this);
    }
}