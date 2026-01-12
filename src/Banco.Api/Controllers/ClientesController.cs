using Banco.Application.DTOs.Clientes;
using Banco.Application.Interfaces.Repositories;
using Banco.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Banco.Api.Controllers
{
    /// <summary>
    /// Controlador responsable de exponer los endpoints relacionados con los clientes
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ClientesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Obtiene todos los clientes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ObtenerTodo()
        {
            var cliente = await unitOfWork.Clientes.GetAllAsync();
            //Mapeo a DTO
            var response = cliente.Select(x => new ClienteResponse
            {
                ClienteId = x.ClienteId,
                Nombre = x.Nombre,
                Estado = x.Estado
            });

            return Ok(response);
        }
        /// <summary>
        /// Obtiene un cliente por su ClienteId
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns></returns>
        [HttpGet("{clienteId}")]
        public async Task<IActionResult> ObtenerClientePorId(string clienteId)
        {
            var cliente = await unitOfWork.Clientes.GetByClienteIdAsync(clienteId);

            if (cliente == null)
            {
                return NotFound("Clienmte no ewncontrado.");
            }

            var response = new ClienteResponse
            {
                ClienteId = cliente.ClienteId,
                Nombre = cliente.Nombre,
                Estado = cliente.Estado
            };

            return Ok(response);
        }
        /// <summary>
        /// crea un nuevo cliente
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteRequest request)
        {
            //Validacion si existe ClienteId
            if (string.IsNullOrWhiteSpace(request.ClienteId))
            {
                return BadRequest("ClienteId es obligatorio.");
            }

            var cliente = new Cliente
            (
                request.ClienteId,
                request.Nombre,
                request.Genero,
                request.Edad,
                request.Identificacion,
                request.Direccion,
                request.Telefono,
                request.Contrasena,
                request.Estado
            );

            await unitOfWork.Clientes.AddAsync(cliente);
            await unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerClientePorId), new { clienteId = request.ClienteId}, null);
        }
        /// <summary>
        /// Actualiza el estado de un cliente
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpPatch("{clienteId}/estado")]
        public async Task<IActionResult> ActualizarEstado(string clienteId, [FromBody] bool estado)
        {
            var cliente = await unitOfWork.Clientes.GetByClienteIdAsync(clienteId);

            if (cliente == null)
            {
                return BadRequest("Cliente no encontrado.");
            }
            //Actualizacion
            cliente.GetType().GetProperty("Estado")?.SetValue(cliente, estado);

            unitOfWork.Clientes.Update(cliente);
            await unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}