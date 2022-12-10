using BjornApp.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database.Query;
using System.Threading.Tasks;
using Firebase.Storage;
using System.IO;

namespace BjornApp.VistasModelo
{
    public class VMnegocios
    {
        string rutaFoto;
        public async Task InsertarNegocios(Mnegocios parametros)
        {
            await Constantes.firebase
                .Child("Negocios")
                .PostAsync(new Mnegocios()
                {
                    celular = parametros.celular,
                    descripcion = parametros.descripcion,
                    direccion = parametros.direccion,
                    foto = parametros.foto,
                    nombre = parametros.nombre,
                    idusuario= parametros.idusuario,
                    idlocalidad = parametros.idlocalidad,
                    prioridad = parametros.prioridad
                });
        }

        public async Task<string> SubirImagenStorage(Stream imageStream, string idUsuario)
        {
            var Imagen = await new FirebaseStorage(Constantes.storage)
                .Child("Negocios")
                .Child(idUsuario + ".jpg")
                .PutAsync(imageStream);
            rutaFoto = Imagen;
            return rutaFoto;
        }
    }
}
