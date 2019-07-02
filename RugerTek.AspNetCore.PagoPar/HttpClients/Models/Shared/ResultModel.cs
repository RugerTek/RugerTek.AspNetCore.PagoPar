namespace RugerTek.AspNetCore.HttpClients.Models.Shared
{
    public class ResultModel
    {
        public bool Respuesta { get; set; }
        public object Resultado { get; set; }
    }

    public class ResultModel<T> where T : new()
    {
        public bool Respuesta { get; set; }
        public T Resultado { get; set; } = new T();
        public string Error { get; set; }
    }
}
