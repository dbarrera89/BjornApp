using BjornApp.VistasModelo;
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
	public partial class CrearCorreo : ContentPage
	{
		public CrearCorreo ()
		{
			InitializeComponent ();
		}

        private void btnCrearCuenta_Clicked(object sender, EventArgs e)
        {
			CrearCuenta();
        }

		private void CrearCuenta()
		{
			var funcion = new VMcrearcuenta();
			funcion.CrearCuenta(txtCorreo.Text, txtContrasena.Text);
		}
    }
}