

using Avalonia.Controls;
using System;
using TestSqlGilmi.Data;
using TestSqlGilmi.Models;
using TestSqlGilmi.ViewModels;

namespace TestSqlGilmi.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainControl.Content = new UserUC();
        }

        

        private void Button_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MainControl.Content = new UserUC();
        }

        private void Button_Click_2(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MainControl.Content = new ItemsUC();
        }

        private void Button_Click_3(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MainControl.Content = new BasketUC();
        }
    }
}