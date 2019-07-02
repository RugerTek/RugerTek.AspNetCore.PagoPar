using RugerTek.AspNetCore.PagoPar.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace RugerTek.AspNetCore.PagoPar.Models.InitTransaction
{
    public class PagoParInitTransactionModel
    {
        public PagoParClientModel Comprador { get; set; } = new PagoParClientModel();
        public long MontoTotal { get; set; }
        // public string TipoPedido { get; set; }
        public DateTime FechaMaximaPago { get; set; }
        public string IdPedidoComercio { get; set; }
        public string DescripcionResumen { get; set; }
        public List<PagoParItemModel> ComprasItems { get; set; } = new List<PagoParItemModel>();
    }
}
