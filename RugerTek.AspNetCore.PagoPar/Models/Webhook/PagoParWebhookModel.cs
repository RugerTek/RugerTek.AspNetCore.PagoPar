using RugerTek.AspNetCore.PagoPar.Models.Results;
using RugerTek.AspNetCore.PagoPar.Models.Transactions;
using System.Collections.Generic;

namespace RugerTek.AspNetCore.PagoPar.Models.Webhook
{
    public class PagoParWebhookModel : PagoParResult<List<PagoParTransactionModel>>
    {
        
    }
}
