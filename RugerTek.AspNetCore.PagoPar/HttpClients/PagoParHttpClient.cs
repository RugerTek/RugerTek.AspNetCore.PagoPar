using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RugerTek.AspNetCore.HttpClients.Models.Shared;
using RugerTek.AspNetCore.PagoPar.HttpClients.Models.InitTransaction;
using RugerTek.AspNetCore.PagoPar.HttpClients.Models.PaymentMethods;
using RugerTek.AspNetCore.PagoPar.HttpClients.Models.Shared;
using RugerTek.AspNetCore.PagoPar.HttpClients.Models.Transactions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RugerTek.AspNetCore.PagoPar.HttpClients
{
    public class PagoParHttpClient
    {
        private readonly HttpClient _httpClient;

        public PagoParHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResultModel<List<DataModel>>> InitTransactionAsync(InitTransactionModel model)
        {
            var body = new StringContent(JsonConvert.SerializeObject(model, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver()}));
            var result = await _httpClient.PostAsync("comercios/1.1/iniciar-transaccion", body);
            var responseString = await result.Content.ReadAsStringAsync();
            return MapResult<List<DataModel>>(responseString);
        }

        public async Task<ResultModel<List<PaymentMethodModel>>> ListPaymentMethodsAsync(PaymentMethodsRequestModel model)
        {
            var body = new StringContent(JsonConvert.SerializeObject(model, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            var result = await _httpClient.PostAsync("forma-pago/1.1/traer", body);
            var responseString = await result.Content.ReadAsStringAsync();
            return MapResult<List<PaymentMethodModel>>(responseString);
        }

        public async Task<ResultModel<List<TransactionModel>>> GetTransactionAsync(GetTransactionInfoModel model)
        {
            var body = new StringContent(JsonConvert.SerializeObject(model, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            var result = await _httpClient.PostAsync("pedidos/1.1/traer", body);
            var responseString = await result.Content.ReadAsStringAsync();
            return MapResult<List<TransactionModel>>(responseString);
        }

        private ResultModel<T> MapResult<T>(string responseString) where T : new()
        {
            var response = JsonConvert.DeserializeObject<ResultModel>(responseString);
            if (response.Respuesta)
            {
                return JsonConvert.DeserializeObject<ResultModel<T>>(responseString);
            }
            return new ResultModel<T>
            {
                Respuesta = false,
                Error = response.Resultado.ToString()
            };
        }
    }
}
