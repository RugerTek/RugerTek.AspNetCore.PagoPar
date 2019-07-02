using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RugerTek.AspNetCore.PagoPar.HttpClients.Models.Transactions
{
    public class TransactionModel
    {
        public bool Pagado { get; set; }
        [JsonProperty("forma_pago")]
        public string FormaPago { get; set; }
        [JsonProperty("fecha_pago")]
        public string FechaPago { get; set; }
        public string Monto { get; set; }
        [JsonProperty("fecha_maxima_pago")]
        public string FechaMaximaPago { get; set; }
        [JsonProperty("hash_pedido")]
        public string HashPedido { get; set; }
        [JsonProperty("numero_pedido")]
        public string NumeroPedido { get; set; }
        public bool Cancelado { get; set; }
        [JsonProperty("forma_pago_identificador")]
        public string FormaPagoIdentificador { get; set; }
        public string Token { get; set; }
    }
}
