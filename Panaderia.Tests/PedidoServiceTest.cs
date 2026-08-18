using NUnit.Framework;
using Panaderia.Entidades;
using Panaderia.Servicios;

namespace Panaderia.Tests;

public class PedidoServiceTests
{
    private const string RutaArchivo = "test_pedidos.json";

    private PedidoService _service = null!;

    [SetUp]
    public void SetUp()
    {
        if (File.Exists(RutaArchivo))
        {
            File.Delete(RutaArchivo);
        }

        _service = new PedidoService(RutaArchivo);
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(RutaArchivo))
        {
            File.Delete(RutaArchivo);
        }
    }

    [Test]
public void Crear_PedidoValido_QuedaEnEstadoRecibido()
{
    // Arrange
    string nombreCliente = "Juan Pérez";
    string detalle = "2 tortas de chocolate";
    TipoEntrega tipoEntrega = TipoEntrega.RetiroLocal;

    // Act
    Pedido pedido = _service.Crear(
        nombreCliente,
        detalle,
        tipoEntrega);

    // Assert
    Assert.That(pedido, Is.Not.Null);
    Assert.That(pedido.Id, Is.EqualTo(1));
    Assert.That(pedido.NombreCliente, Is.EqualTo(nombreCliente));
    Assert.That(pedido.Detalle, Is.EqualTo(detalle));
    Assert.That(pedido.TipoEntrega, Is.EqualTo(tipoEntrega));
    Assert.That(pedido.Estado, Is.EqualTo(EstadoPedido.Recibido));
}

}
