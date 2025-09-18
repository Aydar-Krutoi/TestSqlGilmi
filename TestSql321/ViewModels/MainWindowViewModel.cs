using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestSqlGilmi.Data;

namespace TestSqlGilmi.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public List<User> Users { get; set; }
        public List<Item> Items { get; set; }
        public List<Basket> Baskets { get; set; }

        public MainWindowViewModel()
        {
            RefreshData();
            RefreshItemsData();
            RefreshBasketsData();
        }

        public void RefreshData()
        {
            var usersFromDb = App.DbContext.Users
               .Include(u => u.Roles)
               .ToList();
            Users = usersFromDb;
            OnPropertyChanged(nameof(Users));
        }

        public void RefreshItemsData()
        {
            var itemsFromDb = App.DbContext.Items.ToList();
            Items = itemsFromDb;
            OnPropertyChanged(nameof(Items));
        }

        public void RefreshBasketsData()
        {
            var basketsFromDb = App.DbContext.Baskets
                .Include(b => b.User)
                .Include(b => b.Items)
                .ToList();
            Baskets = basketsFromDb;
            OnPropertyChanged(nameof(Baskets));
        }
    }
}