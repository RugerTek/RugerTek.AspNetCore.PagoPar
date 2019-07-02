using Newtonsoft.Json;
using RugerTek.AspNetCore.HttpClients.Models.Shared;
using RugerTek.AspNetCore.PagoPar.HttpClients.Models.Shared;
using System.Collections.Generic;

namespace RugerTek.AspNetCore.PagoPar.HttpClients.Models.InitTransaction
{
    public class InitTransactionModel
    {
        public string Token { get; set; }
        public ClientModel Comprador { get; set; } = new ClientModel();
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
        [JsonProperty("monto_total")]
        public long MontoTotal { get; set; }
        [JsonProperty("tipo_pedido")]
        public string TipoPedido { get; set; }
        [JsonProperty("fecha_maxima_pago")]
        public string FechaMaximaPago { get; set; }
        [JsonProperty("id_pedido_comercio")]
        public string IdPedidoComercio { get; set; }
        [JsonProperty("descripcion_resumen")]
        public string DescripcionResumen { get; set; }
        [JsonProperty("compras_items")]
        public List<ItemModel> ComprasItems { get; set; } = new List<ItemModel>();
    }
}
