using System;
using System.Collections.Generic;
using System.Text;

namespace RugerTek.AspNetCore.PagoPar.Models.Shared
{
    public class PagoParClientModel
    {
        public string Ruc { get; set; }
        public string Email { get; set; }
        public string Ciudad { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Documento { get; set; }
        public string Coordenadas { get; set; }
        public string RazonSocial { get; set; }
        public string TipoDocumento { get; set; }
        public string DireccionReferencia { get; set; }
    }
}
