using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using Firebase.Database;

namespace BjornApp.VistasModelo
{
    public class Constantes
    {
        public static FirebaseClient firebase = new FirebaseClient("https://publicityapp-5d8df-default-rtdb.firebaseio.com/");

        public const string WebapiFirebase = "AIzaSyAManFnZTWU6bsrJzbtGcCNXswEJBWIMhg";
    }
}
