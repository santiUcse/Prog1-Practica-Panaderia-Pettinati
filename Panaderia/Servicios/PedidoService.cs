using Panaderia.Datos;
using Panaderia.Entidades;

namespace Panaderia.Servicios;

public class PedidoService
{
    private readonly PedidoRepositorio _repositorio;

    public PedidoService(string rutaArchivo = "pedidos.json")
    {
        _repositorio = new PedidoRepositorio(rutaArchivo);
    }

    public List<Pedido> ObtenerTodos()
    {
        return _repositorio.ObtenerTodos();
    }
}
