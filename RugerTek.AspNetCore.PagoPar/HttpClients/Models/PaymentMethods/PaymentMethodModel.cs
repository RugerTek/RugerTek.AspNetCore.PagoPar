using Newtonsoft.Json;

namespace RugerTek.AspNetCore.PagoPar.HttpClients.Models.PaymentMethods
{
    public class PaymentMethodModel
    {
        [JsonProperty("forma_pago")]
        public string FormaPago { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        [JsonProperty("monto_minimo")]
        public string MontoMinimo { get; set; }
    }
}
