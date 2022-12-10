using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using BjornApp.VistasModelo;
using BjornApp.Vistas;
using Newtonsoft.Json;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace BjornApp.VistasModelo
{
    public class VMcrearcuenta
    {
        string idUsuario;
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

        public async Task<string> ObtenerIdUsuario()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapiFirebase));
            var guararId = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
            var refrescarContenido = await authProvider.RefreshAuthAsync(guararId);
            Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refrescarContenido));
            idUsuario = guararId.User.LocalId;
            return idUsuario;
        }
    }
}
