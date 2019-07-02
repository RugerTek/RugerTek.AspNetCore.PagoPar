using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RugerTek.AspNetCore.PagoPar.HttpClients.Models.Shared
{
    public class ClientModel
    {
        public string Ruc { get; set; }
        public string Email { get; set; }
        public string Ciudad { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Documento { get; set; }
        public string Coordenadas { get; set; }
        [JsonProperty("razon_social")]
        public string RazonSocial { get; set; }
        [JsonProperty("tipo_documento")]
        public string TipoDocumento { get; set; }
        [JsonProperty("direccion_referencia")]
        public string DireccionReferencia { get; set; }
    }
}
