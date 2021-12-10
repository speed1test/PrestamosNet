using System;
using System.ComponentModel.DataAnnotations;
namespace Prestamos.Models
{
    public class MesModel
    {
        public int idMes {get; set;}
        public string descripcionMes {get; set;}
        public int valorMes {get; set;}
    }
    public class PrestamoModel
    {
        public int idPrestamo { get; set; }
        public string fechaPrestamo { get; set; }
        public double montoPrestamo { get; set; }
        public MesModel mesesPrestamo { get; set; }

    }
    public class TasaModel
    {
        public int idTasa {get; set;}
        public double valorTasa {get; set;}
        public int edadTasa {get;set;}
    }
    public class ResumenModel
    {
        public PrestamoModel prestamoResumen {get; set;}
        public double cuotaResumen {get; set;}
        public String estadoResumen {get; set;}
    }
}