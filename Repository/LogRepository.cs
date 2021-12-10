using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Prestamos.Models;
using Prestamos.Utils;
using Microsoft.Extensions.Logging;
using Prestamos.Controllers;
using Prestamos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net; //Include this namespace

namespace Log.Repository
{
    public class LogRepository
    {
        public static bool registrarLog(int idPrestamo, int edad, double cuota, double monto, string ip){
            bool flag = false;
            PrestamoModel prestamo = PrestamosRepository.obtenerPrestamo(idPrestamo);
            /*int edad = ObtenerEdad(Convert.ToDateTime(prestamo.fechaPrestamo));
            double cuota = PrestamosController.CalcularCuota();*/
            return flag;
            
        }
    }
}