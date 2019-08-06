using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RugerTek.AspNetCore.HttpClients.Models.Shared;
using RugerTek.AspNetCore.PagoPar.Configuration;
using RugerTek.AspNetCore.PagoPar.Helpers;
using RugerTek.AspNetCore.PagoPar.HttpClients;
using RugerTek.AspNetCore.PagoPar.HttpClients.Models.InitTransaction;
using RugerTek.AspNetCore.PagoPar.HttpClients.Models.PaymentMethods;
using RugerTek.AspNetCore.PagoPar.HttpClients.Models.Transactions;
using RugerTek.AspNetCore.PagoPar.Models.InitTransaction;
using RugerTek.AspNetCore.PagoPar.Models.PaymentMethod;
using RugerTek.AspNetCore.PagoPar.Models.Results;
using RugerTek.AspNetCore.PagoPar.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RugerTek.AspNetCore.PagoPar.Services.Implementations
{
    public class PagoParService : IPagoParService
    {
        private readonly ILogger<PagoParService> _logger;
        private readonly PagoParHttpClient _httpClient;
        private readonly PagoParConfiguration _pagoParConfig;

        public PagoParService(ILogger<PagoParService> logger, PagoParHttpClient httpClient, IOptions<PagoParConfiguration> pagoParConfig)
        {
            _logger = logger;
            _httpClient = httpClient;
            _pagoParConfig = pagoParConfig.Value;
        }

        public async Task<PagoParResult<(string Hash, string RedirectUrl)>> InitTransactionAsync(PagoParInitTransactionModel model)
        {
            try
            {
                _logger.LogInformation("Starting transaction", JsonConvert.SerializeObject(model));
                var content = new InitTransactionModel
                {
                    DescripcionResumen = model.DescripcionResumen,
                    FechaMaximaPago = model.FechaMaximaPago.ToString("yyyy-MM-dd HH:mm:ss"),
                    TipoPedido = "VENTA-COMERCIO", // TODO: Enum this shit
                    MontoTotal = model.MontoTotal,
                    IdPedidoComercio = model.IdPedidoComercio,
                    PublicKey = _pagoParConfig.PublicKey,
                    Token = $"{_pagoParConfig.PrivateKey}{model.IdPedidoComercio}{model.MontoTotal}".Sha1(),
                    Comprador =
                    {
                        Ciudad = model.Comprador.Ciudad,
                        Coordenadas = model.Comprador.Coordenadas,
                        Direccion = model.Comprador.Direccion,
                        DireccionReferencia = model.Comprador.DireccionReferencia,
                        Documento = model.Comprador.Documento,
                        Email = model.Comprador.Email,
                        Nombre = model.Comprador.Nombre,
                        RazonSocial = model.Comprador.RazonSocial,
                        Ruc = model.Comprador.Ruc,
                        Telefono = model.Comprador.Telefono,
                        TipoDocumento = model.Comprador.TipoDocumento
                    }
                };
                foreach (var compraItem in model.ComprasItems)
                {
                    var item = new ItemModel
                    {
                        Cantidad = compraItem.Cantidad,
                        Categoria = compraItem.Categoria,
                        Ciudad = compraItem.Ciudad,
                        Descripcion = compraItem.Descripcion,
                        IdProducto = compraItem.IdProducto,
                        Nombre = compraItem.Nombre,
                        PrecioTotal = compraItem.PrecioTotal,
                        UrlImagen = compraItem.UrlImagen,
                        VendedorDireccion = compraItem.VendedorDireccion,
                        VendedorDireccionCoordenadas = compraItem.VendedorDireccionCoordenadas,
                        VendedorDireccionReferencia = compraItem.VendedorDireccionReferencia,
                        VendedorTelefono = compraItem.VendedorTelefono,
                        PublicKey = _pagoParConfig.PublicKey
                    };

                    content.ComprasItems.Add(item);
                }

                var apiResult = await _httpClient.InitTransactionAsync(content);
                var result = new PagoParResult<(string Hash, string RedirectUrl)>
                {
                    Respuesta = apiResult.Respuesta,
                    Error = apiResult.Error
                };
                var resultado = apiResult.Resultado.FirstOrDefault();
                if (apiResult.Respuesta && resultado != null)
                {
                    var hash = resultado.Data;
                    result.Resultado = (
                        Hash: hash,
                        RedirectUrl: GenerateRedirectUrl(hash)
                    );
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in InitTransactionAsync");
                throw;
            }
        }

        public async Task<PagoParResult<PagoParTransactionModel>> GetTransactionInfo(string hash)
        {
            try
            {
                _logger.LogInformation("Getting transaction", hash);
                var body = new GetTransactionInfoModel
                {
                    HashPedido = hash,
                    TokenPublico = _pagoParConfig.PublicKey,
                    Token = $"{_pagoParConfig.PrivateKey}CONSULTA".Sha1()
                };
                var apiResult = await _httpClient.GetTransactionAsync(body);
                var transaction = apiResult.Resultado.FirstOrDefault();
                if (transaction is null) return null;
                var result = new PagoParResult<PagoParTransactionModel>
                {
                    Respuesta = apiResult.Respuesta,
                    Resultado =
                    {
                        Cancelado = transaction.Cancelado,
                        FechaMaximaPago = transaction.FechaMaximaPago,
                        FechaPago = transaction.FechaPago,
                        FormaPago = transaction.FormaPago,
                        FormaPagoIdentificador = transaction.FormaPagoIdentificador,
                        HashPedido = transaction.HashPedido,
                        Monto = transaction.Monto,
                        NumeroPedido = transaction.NumeroPedido,
                        Pagado = transaction.Pagado,
                        Token = transaction.Token
                    }
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTransactionInfo");
                throw;
            }
        }

        public async Task<PagoParResult<List<PagoParPaymentMethod>>> ListPaymentMethodsAsync()
        {
            try
            {
                _logger.LogInformation("Executing ListPaymentMethodsAsync");
                var body = new PaymentMethodsRequestModel
                {
                    TokenPublico = _pagoParConfig.PublicKey,
                    Token = $"{_pagoParConfig.PrivateKey}FORMA-PAGO".Sha1()
                };
                var apiResult = await _httpClient.ListPaymentMethodsAsync(body);
                var result = new PagoParResult<List<PagoParPaymentMethod>>
                {
                    Respuesta = apiResult.Respuesta,
                    Resultado = apiResult.Resultado.Select(x => new PagoParPaymentMethod
                    {
                        Titulo = x.Titulo,
                        Descripcion = x.Descripcion,
                        FormaPago = x.FormaPago,
                        MontoMinimo = x.MontoMinimo
                    }).ToList(),
                    Error = apiResult.Error
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ListPaymentMethodsAsync");
                throw;
            }
        }

        public string GenerateRedirectUrl(string hash, string paymentMethod = null)
        {
            if (string.IsNullOrWhiteSpace(paymentMethod))
                return $"https://www.pagopar.com/pagos/{hash}";
            return $"https://www.pagopar.com/pagos/{hash}?forma_pago={paymentMethod}";
        }

        public bool ValidateWebhookToken(string hashPedido, string token)
        {
            return $"{_pagoParConfig.PrivateKey}{hashPedido}".Sha1() == token;
        }
    }
}
