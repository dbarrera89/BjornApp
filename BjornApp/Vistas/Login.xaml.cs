using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BjornApp.Vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();
		}

        private async void btnCrearCuenta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CrearCorreo());
        }

        private void btnIniciar_Clicked(object sender, EventArgs e)
        {

        }
    }
}