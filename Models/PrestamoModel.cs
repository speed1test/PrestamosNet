using System;
using System.ComponentModel.DataAnnotations;
namespace Prestamos.Models
{
    public class PrestamoModel
    {
        public int idPrestamo { get; set; }
        public string fechaPrestamo { get; set; }
        public double montoPrestamo { get; set; }
        public int mesesPrestamo { get; set; }

    }
    public class LogModel
    {
        public int idConsulta {get; set;}
        public PrestamoModel idFkPrestamo {get; set;}
        public string fechaConsulta {get; set;}
        public string ipConsulta {get; set;}
        public double valorConsulta {get; set;}
    }
}