using Calculator_.src;
using Stripe;
using System.Windows;

namespace Calculator_
{
    public partial class App : System.Windows.Application
    {
        private void StartingWindow(object sender, StartupEventArgs e)
        {
            StripeConfiguration.ApiKey = "sk_test_51SIPHSEBgNLQba937dRlLiAy2LSIZNJuOQtmx0yKa8VOXCGDbqwMbpbbwajbch2FP3uqZCMhFIOddeuYtVemuvFW00EXxQrBsA";
            Database.BeginIt();
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            if (!Database.EFHook.Users.Any())
            {
                CreateUser need = new();
                need.ShowDialog();
            }
            IEnumerable<User> findActive = from user in Database.EFHook.Users where user.IsActiveUser == true select user;
            findActive.First().ChargeUserCard(50, "Entry Fee");
            this.MainWindow = new MainWindow(findActive.First());
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
            this.MainWindow.Show();
        }
    }

}
