using System;
using System.Collections.Generic;
using System.Text;

namespace RugerTek.AspNetCore.PagoPar.Models.PaymentMethod
{
    public class PagoParPaymentMethod
    {
        public string FormaPago { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string MontoMinimo { get; set; }
    }
}
