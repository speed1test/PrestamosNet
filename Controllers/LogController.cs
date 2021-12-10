using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Prestamos.Models;
using Log.Repository;

namespace Log.Controllers;

public class LogController : Controller
{
    private readonly ILogger<LogController> _logger;

    public LogController(ILogger<LogController> logger)
    {
        _logger = logger;
    }
    [Route("/Logs")]
     public IActionResult Ver()
     {
        try{
            List<LogModel> objeto=LogRepository.obtenerLogs();
            ViewBag.logs = objeto;
            return View("/Views/Registro/Log.cshtml");
        }
        catch
        {
            return RedirectToAction(actionName:"Error", controllerName:"Prestamo");
        }
     }

}