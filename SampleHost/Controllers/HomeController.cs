using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleHost.Models;
using RugerTek.AspNetCore.PagoPar.Services;
using RugerTek.AspNetCore.PagoPar.Models.InitTransaction;
using RugerTek.AspNetCore.PagoPar.Models.Shared;

namespace SampleHost.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPagoParService _pagoParService;

        public HomeController(IPagoParService pagoParService)
        {
            _pagoParService = pagoParService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> InitPayment()
        {
            var model = new PagoParInitTransactionModel
            {
                MontoTotal = 20000,
                Comprador =
                {
                    Ciudad = "1",
                    Email = "aizr97@gmail.com",
                    Nombre = "Andoni Zubizarreta",
                    RazonSocial = "Andoni Zubizarreta",
                    Ruc = "4178924-5",
                    Documento = "4178924",
                    Telefono = "0981158883",
                    TipoDocumento = "CI"
                },
                DescripcionResumen = "Compra virtual",
                FechaMaximaPago = DateTime.Now.AddDays(2),
                IdPedidoComercio = "252635",
                ComprasItems = new List<PagoParItemModel>
                {
                    new PagoParItemModel
                    {
                        Cantidad = 1,
                        Categoria = "909",
                        Ciudad = "1",
                        Descripcion = "IPhone 8",
                        IdProducto = 1,
                        Nombre = "IPhone 8",
                        PrecioTotal = 20000,
                        UrlImagen = "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/image/AppleInc/aos/published/images/i/ph/iphone8/plus/iphone8-plus-gold-select-2018?wid=470&hei=556&fmt=png-alpha&.v=1550795417455"
                    }
                }
            };
            var result = await _pagoParService.InitTransactionAsync(model);
            if (!result.Respuesta)
            {
                return Ok(result.Error);
            }
            return Redirect(result.Resultado.RedirectUrl);
        }

        public async Task<IActionResult> PaymentMethods()
        {
            var result = await _pagoParService.ListPaymentMethodsAsync();
            return Ok(result);
        }

        public async Task<IActionResult> GetTransaction([FromQuery] string hash)
        {
            var result = await _pagoParService.GetTransactionInfo(hash);
            return Ok(result);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
