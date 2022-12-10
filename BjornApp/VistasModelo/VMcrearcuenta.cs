using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using BjornApp.VistasModelo;
using BjornApp.Vistas;
using Newtonsoft.Json;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace BjornApp.VistasModelo
{
    public class VMcrearcuenta
    {
        public async Task CrearCuenta(string correo, string contrasena)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapiFirebase));
            await authProvider.CreateUserWithEmailAndPasswordAsync(correo, contrasena);
        }

        public async Task ValidarCuenta(string correo, string contrasena)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapiFirebase));
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(correo, contrasena);
                await App.Current.MainPage.DisplayAlert("Correcto", "Listo", "Ok");
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Datos incorrectos", "Ok");
            }
        }
    }
}
