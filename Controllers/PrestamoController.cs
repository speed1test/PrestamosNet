using System.Linq.Expressions;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Prestamos.Models;
using Prestamos.Repository;
using Log.Repository;

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
        List<PrestamoModel> objeto=PrestamosRepository.obtenerPrestamos();
        /*List<ResumenModel> resumen = new List<ResumenModel>();
        ResumenModel objeto2 = new ResumenModel();
        for(int i = 0; i < objeto.Count; i++)
        {
            int edad = ObtenerEdad(Convert.ToDateTime(objeto[i].fechaPrestamo));
            objeto2.estadoResumen = "";
            objeto2.prestamoResumen = objeto[i];
            objeto2.cuotaResumen = CalcularCuota(edad,objeto[i].montoPrestamo,objeto[i].mesesPrestamo.valorMes);
            //Console.WriteLine(Convert.ToDouble(CalcularCuota(edad,objeto[i].montoPrestamo,objeto[i].mesesPrestamo.valorMes)));
            Console.WriteLine(objeto2.cuotaResumen);
            resumen.Add(objeto2);
        }*/
        ViewBag.prestamos = objeto;
        return View("/Views/Prestamo/Index.cshtml");
    }
    [Route("/Prestamo/Consultar/Cuota/{idPrestamo}")]
    public IActionResult ConsultarCuota(int idPrestamo)
    {
        PrestamoModel prestamo = PrestamosRepository.obtenerPrestamo(Convert.ToInt32(idPrestamo));
        double cuota = CalcularCuota(prestamo.fechaPrestamo,prestamo.montoPrestamo,prestamo.mesesPrestamo.valorMes);
        int edad = ObtenerEdad(Convert.ToDateTime(prestamo.fechaPrestamo));
        string ipConsulta = obtenerIp(HttpContext);
        ViewBag.cuota = cuota;
        LogRepository.registrarLog(idPrestamo,edad,cuota,ipConsulta);
        //Console.WriteLine(CalcularCuota(prestamo.fechaPrestamo,prestamo.montoPrestamo,prestamo.mesesPrestamo.valorMes));
        return View("/Views/Prestamo/ConsultarCuota.cshtml");
    }
    public static double CalcularCuota(string fechaPrestamo, double monto, int meses)
    {
        int edad = ObtenerEdad(Convert.ToDateTime(fechaPrestamo));
        List<TasaModel> tasas = PrestamosRepository.obtenerTasas();
        double calculo = 0;
        for(int i = 0; i < tasas.Count;i++)
        {
            if(tasas[i].edadTasa == edad)
            {
                calculo = ((monto)*(tasas[i].valorTasa))*(meses);
            }
        }
        return calculo;
    }
    public static string obtenerIp(HttpContext context)
    {
        string ip = string.Empty;
	    if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
	    {
		    ip = context.Request.Headers["X-Forwarded-For"];
	    }
	    else
	    {
		    ip = context.Request.HttpContext.Connection.RemoteIpAddress.ToString();
	    }
	    return ip;
    }
    public static int ObtenerEdad(DateTime fecha)
    {
        var calculo = DateTime.Now - fecha;
        return (int)(calculo.TotalDays / 365.255);
    }
    [Route("/Prestamo/Crear")]
    public IActionResult Crear(String fecha, int meses, double monto)
    {
        ViewBag.meses = PrestamosRepository.obtenerMeses();
        if(HttpContext.Request.Method == "POST"){
            PrestamosRepository.registrarPrestamo(fecha,(decimal)(monto),meses);
        }
        return View("/Views/Prestamo/Crear.cshtml");
    }
}