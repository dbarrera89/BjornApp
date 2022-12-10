using BjornApp.Modelo;
using BjornApp.VistasModelo;
using Plugin.Media;
using Plugin.Media.Abstractions;
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

		MediaFile file;
		string idUsuario;
		string rutaFoto;

        private void btnCrearCuenta_Clicked(object sender, EventArgs e)
        {
			if (!string.IsNullOrEmpty(txtNombres.Text))
			{
				if (!string.IsNullOrEmpty(txtCorreo.Text))
				{
                    if (!string.IsNullOrEmpty(txtContrasena.Text))
					{
                        CrearCuenta();
                        IniciarSesion();
						ObtenerIdUsuario();
						//TODO: Pendiente habilitar método, esperar a solucionar problema con Firebase Storage
						//SubirFotoStorage();
						InsertarNegocios();
                    }

                }

            }
			
        }

		private void CrearCuenta()
		{
			var funcion = new VMcrearcuenta();
			funcion.CrearCuenta(txtCorreo.Text, txtContrasena.Text);
		}

		private void IniciarSesion()
		{
			var funcion = new VMcrearcuenta();
			funcion.ValidarCuenta(txtCorreo.Text, txtContrasena.Text);

		}

		private async void ObtenerIdUsuario()
		{
			
			var funcion = new VMcrearcuenta();
            idUsuario = await funcion.ObtenerIdUsuario();

        }

		private async void SubirFotoStorage()
		{
			var funcion = new VMnegocios();
			rutaFoto = await funcion.SubirImagenStorage(file.GetStream(), idUsuario);
		}

		private async void InsertarNegocios()
		{
			var funcion = new VMnegocios();
			var parametros = new Mnegocios();

			parametros.idusuario = idUsuario;
			parametros.idcategoria = "-";
			parametros.celular = "-";
			parametros.descripcion= "-";
			parametros.direccion= "-";
			parametros.foto= rutaFoto;
			parametros.nombre = txtNombres.Text;
			parametros.idlocalidad = "-";
			parametros.prioridad= "0";
			
			await funcion.InsertarNegocios(parametros);
        }

        private async void btnSubirFoto_Clicked(object sender, EventArgs e)
        {
			await CrossMedia.Current.Initialize();
			try
			{
				file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
				{
					PhotoSize = PhotoSize.Medium
				});
				if (file == null)
					return;
				fotoPerfil.Source = ImageSource.FromStream(file.GetStream);
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", ex.Message, "Ok");
			}
        }
    }
}