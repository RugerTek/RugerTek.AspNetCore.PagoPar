using Newtonsoft.Json;

namespace RugerTek.AspNetCore.HttpClients.Models.Shared
{
    public class ItemModel
    {
        public string Ciudad { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public string Categoria { get; set; }
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
        [JsonProperty("url_imagen")]
        public string UrlImagen { get; set; }
        public string Descripcion { get; set; }
        [JsonProperty("id_producto")]
        public int IdProducto { get; set; }
        [JsonProperty("precio_total")]
        public long PrecioTotal { get; set; }
        [JsonProperty("vendedor_telefono")]
        public string VendedorTelefono { get; set; }
        [JsonProperty("vendedor_direccion")]
        public string VendedorDireccion { get; set; }
        [JsonProperty("vendedor_direccion_referencia")]
        public string VendedorDireccionReferencia { get; set; }
        [JsonProperty("vendedor_direccion_coordenadas")]
        public string VendedorDireccionCoordenadas { get; set; }
    }
}
