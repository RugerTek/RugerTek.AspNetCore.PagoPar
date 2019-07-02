using RugerTek.AspNetCore.PagoPar.Models.InitTransaction;
using RugerTek.AspNetCore.PagoPar.Models.PaymentMethod;
using RugerTek.AspNetCore.PagoPar.Models.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RugerTek.AspNetCore.PagoPar.Services
{
    public interface IPagoParService
    {
        Task<PagoParResult<(string Hash, string RedirectUrl)>> InitTransactionAsync(PagoParInitTransactionModel model);
        Task<PagoParResult<List<PagoParPaymentMethod>>> ListPaymentMethodsAsync();
    }
}
