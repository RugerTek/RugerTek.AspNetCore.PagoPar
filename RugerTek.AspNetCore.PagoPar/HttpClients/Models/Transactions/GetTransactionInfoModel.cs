using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RugerTek.AspNetCore.PagoPar.HttpClients.Models.Transactions
{
    public class GetTransactionInfoModel
    {
        [JsonProperty("hash_pedido")]
        public string HashPedido { get; set; }
        public string Token { get; set; }
        [JsonProperty("token_publico")]
        public string TokenPublico { get; set; }
    }
}
