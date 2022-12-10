using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using BjornApp.VistasModelo;
using BjornApp.Vistas;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace BjornApp.VistasModelo
{
    public class VMcrearcuenta
    {
        public async void CrearCuenta(string correo, string contrasena)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapiFirebase));
            await authProvider.CreateUserWithEmailAndPasswordAsync(correo, contrasena);
        }

        public async void ValidarCuenta(string correo, string contrasena)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapiFirebase));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(correo, contrasena);
            var contenido = await auth.GetFreshAuthAsync();
            var serializacion = JsonConvert.SerializeObject(contenido);
            Preferences.Set("MyFirebaseRefreshToken", serializacion);
        }
    }
}
