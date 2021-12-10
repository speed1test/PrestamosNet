using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Prestamos.Models;
using Prestamos.Utils;
using Microsoft.Extensions.Logging;
using Prestamos.Controllers;
using Prestamos.Repository;

namespace Log.Repository
{
    public class LogRepository
    {
        public static bool registrarLog(int idPrestamo, int edad, double cuota, string ip){
            bool flag = true;
            PrestamoModel prestamo = PrestamosRepository.obtenerPrestamo(idPrestamo);
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Global.getConnectionString();

                SqlParameter idPres = new SqlParameter();
                idPres.ParameterName = "@idPrestamo"; 
                idPres.Value = idPrestamo; 

                SqlParameter fechaCon = new SqlParameter();
                fechaCon.ParameterName = "@fechaConsula"; 
                fechaCon.Value = DateTime.Now; 

                SqlParameter edadCon = new SqlParameter();
                edadCon.ParameterName = "@edadConsulta"; 
                edadCon.Value = edad;

                SqlParameter ipCon = new SqlParameter();
                ipCon.ParameterName = "@ipConsulta"; 
                ipCon.Value = ip;

                SqlParameter valorCon = new SqlParameter();
                valorCon.ParameterName = "@valorConsulta"; 
                valorCon.Value = prestamo.montoPrestamo;

                List<SqlParameter> listadoParametros = new List<SqlParameter>(){
                    idPres,
                    fechaCon,
                    edadCon,
                    ipCon,
                    valorCon
                };
                //dbo.
                //ejecucionSP.EjecutarSPConSalidas("sp_registrar_prestamo" , listadoParametros , con, "@id" ,SqlDbType.Int ,ref flag);
                ejecucionSP.ExecuteSPWithNoDataReturn("sp_registrar_log", listadoParametros, con, ref flag);
            }
            catch
            {
                Console.WriteLine("Algo salio mal");
            }
            //Console.WriteLine(cuota);
            return flag;
        }
    }
}