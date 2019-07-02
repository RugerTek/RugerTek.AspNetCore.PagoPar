using System;
using System.Collections.Generic;
using System.Text;

namespace RugerTek.AspNetCore.PagoPar.Models.Shared
{
    public class PagoParItemModel
    {
        public string Ciudad { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public string Categoria { get; set; }
        public string UrlImagen { get; set; }
        public string Descripcion { get; set; }
        public int IdProducto { get; set; }
        public long PrecioTotal { get; set; }
        public string VendedorTelefono { get; set; }
        public string VendedorDireccion { get; set; }
        public string VendedorDireccionReferencia { get; set; }
        public string VendedorDireccionCoordenadas { get; set; }
    }
}
