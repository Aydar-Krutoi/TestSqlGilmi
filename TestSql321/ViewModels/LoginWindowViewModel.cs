using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSqlGilmi.Data;

namespace TestSqlGilmi.ViewModels
{
    public partial class LoginMainWindowViewModel : ViewModelBase
    {
        public List<Login> Logins { get; set; }

        public LoginMainWindowViewModel()
        {
            RefreshData();
        }

        public void RefreshData()
        {
            var loginsFromDb = App.DbContext.Logins.ToList();
            Logins = loginsFromDb;
            OnPropertyChanged(nameof(Logins));
        }
    }
}
