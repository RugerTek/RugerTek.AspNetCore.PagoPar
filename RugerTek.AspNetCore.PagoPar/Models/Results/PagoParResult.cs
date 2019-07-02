using System;
using System.Collections.Generic;
using System.Text;

namespace RugerTek.AspNetCore.PagoPar.Models.Results
{
    public class PagoParResult<T> where T : new()
    {
        public bool Respuesta { get; set; }
        public T Resultado { get; set; }
        public string Error { get; set; }
    }
}
