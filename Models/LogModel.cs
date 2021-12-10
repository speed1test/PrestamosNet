using System;
using System.ComponentModel.DataAnnotations;
namespace Prestamos.Models
{
    public class LogModel
    {
        public int idConsulta {get; set;}
        public PrestamoModel idFkPrestamo {get; set;}
        public string fechaConsulta {get; set;}
        public string ipConsulta {get; set;}
        public double valorConsulta {get; set;}
        public int edadConsulta {get; set;}
    }
}