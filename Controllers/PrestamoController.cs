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
        try{
            List<PrestamoModel> objeto=PrestamosRepository.obtenerPrestamos();
            ViewBag.prestamos = objeto;
            return View("/Views/Prestamo/Index.cshtml");
        }
        catch
        {
            return RedirectToAction(actionName:"Error", controllerName:"Prestamo");
        }
    }
    [Route("/Prestamo/Consultar/Cuota/{idPrestamo}")]
    public IActionResult ConsultarCuota(int idPrestamo)
    {
        try{
        PrestamoModel prestamo = PrestamosRepository.obtenerPrestamo(Convert.ToInt32(idPrestamo));
        double cuota = CalcularCuota(prestamo.fechaPrestamo,prestamo.montoPrestamo,prestamo.mesesPrestamo.valorMes);
        int edad = ObtenerEdad(Convert.ToDateTime(prestamo.fechaPrestamo));
        string ipConsulta = obtenerIp(HttpContext);
        ViewBag.cuota = cuota;
        bool flag = LogRepository.registrarLog(idPrestamo,edad,cuota,ipConsulta);
        //Console.WriteLine(CalcularCuota(prestamo.fechaPrestamo,prestamo.montoPrestamo,prestamo.mesesPrestamo.valorMes));
        return View("/Views/Prestamo/ConsultarCuota.cshtml");
        }
        catch{
            return RedirectToAction(actionName:"Error", controllerName:"Prestamo");
        }
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
        try{
        ViewBag.meses = PrestamosRepository.obtenerMeses();
        if(HttpContext.Request.Method == "POST"){
            int edad = ObtenerEdad(Convert.ToDateTime(fecha));
            List<int> edades = new List<int>();
            List<TasaModel> tasas = new List<TasaModel>();
            tasas = PrestamosRepository.obtenerTasas();
            for(int i=0;i<tasas.Count;i++)
            {
                edades.Add(tasas[i].edadTasa);
            }
            if(edad < edades.Min()){
                ViewBag.estado = 0;
            }
            else{
                if(edad > edades.Max())
                {
                    ViewBag.estado = 1;
                }
                else
                {
                    ViewBag.estado = 2;
                    PrestamosRepository.registrarPrestamo(fecha,(decimal)(monto),meses);
                    return RedirectToAction(actionName: "Ver");
                }
            }
        }
        return View("/Views/Prestamo/Crear.cshtml");
        }
        catch{
            return RedirectToAction(actionName:"Error", controllerName:"Prestamo");
        }
    }
    [Route("/Prestamo/Eliminar/{idPrestamo}")]
    public IActionResult Eliminar(int idPrestamo)
    {
        try{
        if(HttpContext.Request.Method == "POST"){
            PrestamosRepository.eliminarPrestamo(idPrestamo);
            return RedirectToAction(actionName: "Ver");
        }
        return View("/Views/Prestamo/Eliminar.cshtml");
        }
        catch
        {
            return RedirectToAction(actionName:"Error", controllerName:"Prestamo");
        }
    }
    public IActionResult Error(int idPrestamo)
    {
        ViewBag.error = true;
        return View("/Views/Error/mensaje.cshtml");
    }
}
