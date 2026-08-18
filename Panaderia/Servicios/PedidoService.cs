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

    //OBTENER TODOS --------------------------- 

    public List<Pedido> ObtenerTodos()
    {
        return _repositorio.ObtenerTodos();
    }

    //CREAR ---------------------------
    public Pedido Crear(string nombreCliente, string detalle, TipoEntrega tipoEntrega)
    {
        if (string.IsNullOrWhiteSpace(nombreCliente))
        {
            throw new ArgumentException("El nombre del cliente es obligatorio.");
        }

        if (nombreCliente.Length > 100)
        {
            throw new ArgumentException(
                "El nombre del cliente no puede superar los 100 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(detalle))
        {
            throw new ArgumentException("El detalle es obligatorio.");
        }

        List<Pedido> pedidos = _repositorio.ObtenerTodos();

        int nuevoId = pedidos.Count == 0
            ? 1
            : pedidos.Max(p => p.Id) + 1;

        Pedido pedido = new Pedido
        {
            Id = nuevoId,
            NombreCliente = nombreCliente,
            Detalle = detalle,
            TipoEntrega = tipoEntrega
        };

        pedidos.Add(pedido);

        _repositorio.GuardarTodos(pedidos);

        return pedido;
    }

    //OBTENER POR ID ---------------------------
    public Pedido? ObtenerPorId(int id)
    {
        List<Pedido> pedidos = _repositorio.ObtenerTodos();

        return pedidos.FirstOrDefault(p => p.Id == id);
    }

    //TOMAR PEDIDO ---------------------------
    public Pedido? TomarPedido(int id)
    {
        List<Pedido> pedidos = _repositorio.ObtenerTodos();

        Pedido? pedido = pedidos.FirstOrDefault(p => p.Id == id);

        if (pedido == null)
        {
            return null;
        }

        if (pedido.Estado != EstadoPedido.Recibido)
        {
            throw new InvalidOperationException(
                "El pedido no está en estado Recibido.");
        }

        pedido.Estado = EstadoPedido.EnPreparacion;

        _repositorio.GuardarTodos(pedidos);

        return pedido;
    }
    //MARCAR LISTO ---------------------------
    public Pedido? MarcarListo(int id)
    {
        List<Pedido> pedidos = _repositorio.ObtenerTodos();

        Pedido? pedido = pedidos.FirstOrDefault(p => p.Id == id);

        if (pedido == null)
        {
            return null;
        }

        if (pedido.Estado != EstadoPedido.EnPreparacion)
        {
            throw new InvalidOperationException(
                "El pedido no está en estado En Preparación.");
        }

        pedido.Estado = EstadoPedido.Listo;

        _repositorio.GuardarTodos(pedidos);

        return pedido;
    }
    //ENTREGAR ---------------------------
    public Pedido? Entregar(int id)
    {
        List<Pedido> pedidos = _repositorio.ObtenerTodos();

        Pedido? pedido = pedidos.FirstOrDefault(p => p.Id == id);

        if (pedido == null)
        {
            return null;
        }

        if (pedido.Estado != EstadoPedido.Listo)
        {
            throw new InvalidOperationException(
                "El pedido no está en estado Listo.");
        }

        pedido.Estado = EstadoPedido.Entregado;

        _repositorio.GuardarTodos(pedidos);

        return pedido;
    }
    //LINQ
    //OBTENER POR ESTADO ---------------------------
    public List<Pedido> ObtenerPorEstado(EstadoPedido estado)
    {
        List<Pedido> pedidos = _repositorio.ObtenerTodos();

        return pedidos
            .Where(p => p.Estado == estado)
            .ToList();
    }

//BUSCAR POR CLIENTE ---------------------------
    public List<Pedido> BuscarPorCliente(string texto)
    {
        List<Pedido> pedidos = _repositorio.ObtenerTodos();

        return pedidos
            .Where(p => p.NombreCliente.Contains(
                texto,
                StringComparison.OrdinalIgnoreCase))
            .ToList();
    }



}

