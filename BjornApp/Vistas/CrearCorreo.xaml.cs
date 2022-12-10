using BjornApp.Modelo;
using BjornApp.VistasModelo;
using Firebase.Auth;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
			CerrarSesion();
		}

		MediaFile file;
		string idUsuario;
		string rutaFoto;

        private async void btnCrearCuenta_Clicked(object sender, EventArgs e)
        {

			if (!string.IsNullOrEmpty(txtNombres.Text))
			{
				if (!string.IsNullOrEmpty(txtCorreo.Text))
				{
                    if (!string.IsNullOrEmpty(txtContrasena.Text))
					{
                        await CrearCuenta();
                        await IniciarSesion();
						await ObtenerIdUsuario();
						//TODO: Pendiente habilitar método, esperar a solucionar problema con Firebase Storage
						//await SubirFotoStorage();
						await InsertarNegocios();
					}
					else
					{
						await DisplayAlert("Alerta", "Agregue una contraseña", "OK");
					}

				}
				else
				{
                    await DisplayAlert("Alerta", "Agregue un correo", "OK");
                }

			}
			else
			{
                await DisplayAlert("Alerta", "Agregue un nombre", "OK");
            }
			
        }

		private void CerrarSesion()
		{
			Preferences.Remove("MyFirebaseRefreshToken");

        }

		private async Task CrearCuenta()
		{
			var funcion = new VMcrearcuenta();
			await funcion.CrearCuenta(txtCorreo.Text, txtContrasena.Text);
		}

		private async Task IniciarSesion()
		{
			var funcion = new VMcrearcuenta();
			await funcion.ValidarCuenta(txtCorreo.Text, txtContrasena.Text);

		}

		private async Task ObtenerIdUsuario()
		{
			try
			{
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapiFirebase));
                var guararId = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var refrescarContenido = await authProvider.RefreshAuthAsync(guararId);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refrescarContenido));
                idUsuario = guararId.User.LocalId;				
            }
			catch  (Exception)
			{
				await DisplayAlert("Alerta", "Sesion expirada", "OK");
			}

        }

		private async Task SubirFotoStorage()
		{
			var funcion = new VMnegocios();
			rutaFoto = await funcion.SubirImagenStorage(file.GetStream(), idUsuario);
		}

		private async Task InsertarNegocios()
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
			await DisplayAlert("Listo", "Vuelva a abrir la aplicación", "Ok");
			Process.GetCurrentProcess().CloseMainWindow();
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