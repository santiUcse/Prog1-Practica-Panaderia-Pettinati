using Newtonsoft.Json;
using Panaderia.Entidades;

namespace Panaderia.Datos;

public class PedidoRepositorio
{
    private readonly string _rutaArchivo;

    public PedidoRepositorio(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;
    }

    public List<Pedido> ObtenerTodos()
    {
        if (!File.Exists(_rutaArchivo))
        {
            return new List<Pedido>();
        }

        string json = File.ReadAllText(_rutaArchivo);

        return JsonConvert.DeserializeObject<List<Pedido>>(json)
               ?? new List<Pedido>();
    }

    public void GuardarTodos(List<Pedido> pedidos)
    {
        string json = JsonConvert.SerializeObject(
            pedidos,
            Newtonsoft.Json.Formatting.Indented);

        File.WriteAllText(_rutaArchivo, json);
    }
}
