using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestSqlGilmi.Data;
using TestSqlGilmi.Models;

namespace TestSqlGilmi.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public List<User> Users { get; set; }

        public MainWindowViewModel()
        {
            RefreshData();
        }

        public void RefreshData()
        {
            var usersFromDb = App.DbContext.Users
                .Include(u => u.Roles)
                .ToList();
            Users = usersFromDb;
            OnPropertyChanged(nameof(Users));
        }


    }
}
