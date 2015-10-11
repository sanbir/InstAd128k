using Instad128000.Core.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAd128000.ViewModels.InstagramViewModels
{
    public class LoginViewModel : CommonViewModel
    {
        public LoginViewModel()
        {
            Login = Properties.Settings.Default.Username;
            Password = Properties.Settings.Default.Password;
        }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
