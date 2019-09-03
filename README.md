# Nuget de C# para el API de Pago Par

En el `ConfigureServices` agregamos los servicios

```cs
services.AddPagoParServices(config =>
{
    config.PublicKey = Configuration["PagoPar:PublicToken"];
    config.PrivateKey = Configuration["PagoPar:PrivateToken"];
});
```

Despues ya podes inyectar el servicio de Pago Par donde quieras

```cs
public class PaymentService : IPaymentService
{
  private readonly IPagoParService _pagoParService;
  
  public PaymentService(IPagoParService pagoParService)
  {
    _pagoParService = pagoParService;
  }
}
```

Por favor avisar si hay algun bug.
Aceptamos pull requests.

Desarrollado en RugerTek S.A.
