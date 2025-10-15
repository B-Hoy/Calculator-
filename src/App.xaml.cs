using Stripe;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Calculator_
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			StripeConfiguration.ApiKey = "sk_test_51SIPHSEBgNLQba937dRlLiAy2LSIZNJuOQtmx0yKa8VOXCGDbqwMbpbbwajbch2FP3uqZCMhFIOddeuYtVemuvFW00EXxQrBsA";

		}
    }

}
