namespace Panaderia.Entidades;

public class Pedido
{
    public int Id { get; set; } //FALTA TODAVIA ASIGNAR EL ID 
    public string NombreCliente { get; set; }
    public string Detalle { get; set; }
    public TipoEntrega TipoEntrega { get; set; }
    public EstadoPedido Estado { get; set; }
    public DateTime FechaCreacion { get; set; }

    public Pedido()
    {
        Estado = EstadoPedido.Recibido;
        FechaCreacion = DateTime.Now;
    }
}
