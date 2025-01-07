using Budget_Management.Models;
using Budget_Management.Servicios;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace Budget_Management.Controllers
{
    public class TiposCuentasController: Controller
    {

        /* */
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;

        public TiposCuentasController(IRepositorioTiposCuentas repositorioTiposCuentas)
        {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
        }



        public async Task<IActionResult> Index()
        {
            var usuarioId = 2;
            var tiposCuentas = await repositorioTiposCuentas.Obtener(usuarioId);


            foreach (var cuenta in tiposCuentas) { Debug.WriteLine($"Id: {cuenta.ID}, Name: {cuenta.Nombre}, Order: {cuenta.Orden}"); };


            return View(tiposCuentas);
        }

        public IActionResult Crear()
        {
              

            return View();
        }



        [HttpPost] /* como este task retorna algo, el IActionResult va entre <> */
        public async Task <IActionResult> Crear(TipoCuenta tipoCuenta)
        {

            // si el dato enviado no coincide con el modelo, entra al if. 
            if(!ModelState.IsValid)
            {
                // envío tipo cuenta con la info que el usuario ya escribió
                return View(tipoCuenta);
            }

            tipoCuenta.UsuarioId = 2;

            // VALIDACION DESDE EL CONTROLLER. 
            var yaExisteTipoCuenta = await repositorioTiposCuentas.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);

            if (yaExisteTipoCuenta)
            {
                ModelState.AddModelError(nameof(tipoCuenta.Nombre), $"El nombre {tipoCuenta.Nombre} ya existe.");

                return View(tipoCuenta);

            }

            
            await repositorioTiposCuentas.Crear(tipoCuenta);


            return RedirectToAction("Index");
        }



        /* como este método retorna algo, el IActionResult va entre <> */
        [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre)
        {

            var usuarioId = 2;

            var yaExisteTipoCuentas = await repositorioTiposCuentas.Existe(nombre, usuarioId);

            if(yaExisteTipoCuentas)
            {
                return Json($"El nombre {nombre} ya existe ");
            }

            return Json(true);
        }
    }
}
