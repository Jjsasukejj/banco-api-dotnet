using Banco.Application.DTOs.Movimientos;
using Banco.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Banco.Api.Controllers
{
    /// <summary>
    /// Controlador responsable de exponer los endpoints relacionados con los movimientos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoService movimientoService;
        /// <summary>
        /// El controlador depende del servicio de movimientos, no de repositorios ni de EF Core
        /// </summary>
        /// <param name="movimientoService"></param>
        public MovimientosController(IMovimientoService movimientoService)
        {
            this.movimientoService = movimientoService;
        }
        [HttpPost]
        public async Task<IActionResult> RegistrarMovimiento([FromBody] RegistrarMovimientoRequest request)
        {
            //Validacion basica de entrada, las reglas de negocio viven en el servicio
            if (request.Valor <= 0)
            {
                return BadRequest("El valor del movimiento debe ser mayor a cero.");
            }

            try
            {
                //se delega toda la logica del negocio al servicio
                var movimiento = await movimientoService.RegistrarMovimientoAsync(
                    request.NumeroCuenta,
                    request.TipoMovimiento,
                    request.Valor);
                
                //Se contruye el DTO de respuesta
                var response = new RegistrarMovimientoResponse
                {
                    Fecha = movimiento.Fecha,
                    NumeroCuenta = request.NumeroCuenta,
                    TipoMovimiento = movimiento.TipoMovimiento.ToString(),
                    Valor = movimiento.Valor,
                    Saldo = movimiento.Saldo
                };

                //HTTP 200 ok con el resultado
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                // errores de negocio controlados (saldo insuficiente, cupo diario, etc)
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                //error no controlado
                return StatusCode(500, "Ocurrio un error interno al procesar el movimiento.");
            }
        }
    }
}