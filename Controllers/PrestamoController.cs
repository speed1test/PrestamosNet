using System.Reflection.Metadata.Ecma335;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Prestamos.Models;
using Prestamos.Repository;

namespace Prestamo;

public class PrestamoController : Controller{
    private readonly ILogger<PrestamoController> _logger;

    public PrestamoController(ILogger<PrestamoController> logger)
    {
        _logger = logger;
    }

    [Route("/Prestamo/Home")]
     public IActionResult Ver()
    {
        //Console.WriteLine(PrestamosRepository.obtenerPrestamos());
        ViewBag.prestamos = PrestamosRepository.obtenerPrestamos();
        return View("/Views/Prestamo/Index.cshtml");
    }
    [Route("/Prestamo/Crear")]
    public IActionResult Crear()
    {
        return View("/Views/Prestamo/Crear.cshtml");
    }
}