using Banco.Application.DTOs.Cuentas;
using Banco.Application.Interfaces.Repositories;
using Banco.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Banco.Api.Controllers
{
    /// <summary>
    /// Controlador responsable de exponer los endpoints relacionados con las cuentas
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CuentasController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Obtiene todas las cuentas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ObtenerTodo()
        {
            var cuentas = await unitOfWork.Cuentas.GetAllAsync();

            var response = cuentas.Select(x => new CuentaResponse
            {
                NumeroCuenta = x.NumeroCuenta,
                TipoCuenta = x.TipoCuenta,
                Saldo = x.Saldo,
                Estado = x.Estado
            });

            return Ok(response);
        }
        /// <summary>
        /// Obtiene una cuenta por su numero de cuenta
        /// </summary>
        /// <param name="numeroCuenta"></param>
        /// <returns></returns>
        [HttpGet("{numeroCuenta}")]
        public async Task<IActionResult> ObtenerPorNumeroCuenta(string numeroCuenta)
        {
            var cuenta = await unitOfWork.Cuentas.GetByNumeroCuentaAsync(numeroCuenta);

            if (cuenta == null)
                return NotFound("Cuenta no encontrada.");

            var response = new CuentaResponse
            {
                NumeroCuenta = cuenta.NumeroCuenta,
                TipoCuenta = cuenta.TipoCuenta,
                Saldo = cuenta.Saldo,
                Estado = cuenta.Estado
            };

            return Ok(response);
        }
        /// <summary>
        /// Crea una nueva cuenta
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CuentaRequest request)
        {
            if (request.SaldoInicial < 0)
                return BadRequest("El saldo inicial no puede ser negativo.");

            var cuenta = new Cuenta(
                request.NumeroCuenta,
                request.TipoCuenta,
                request.SaldoInicial,
                request.Estado,
                request.ClienteId);

            await unitOfWork.Cuentas.AddAsync(cuenta);
            await unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerPorNumeroCuenta),
                new { numeroCuenta = request.NumeroCuenta },
                null);
        }
    }
}