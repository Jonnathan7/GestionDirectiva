using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDirectiva.Presentacion.Core.ViewModel.Base
{
    public class CargarArchivoViewModel
    {
        public string NombreArchivo { get; set; }
        public byte[] Archivo { get; set; }
        public string ArchivoBase64 { get; set; }
        public string Extension { get; set; }
        public int Tamaño { get; set; }
    }
}
