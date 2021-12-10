using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Prestamos.Models;
using Prestamos.Utils;
using Microsoft.Extensions.Logging;
using Prestamos.Controllers;
using Prestamos.Repository;
using Microsoft.Extensions.Configuration;
using System.IO;
//using Microsoft.Data.SqlClient;

namespace Log.Repository
{
    public class LogRepository
    {
        public static bool registrarLog(int idPrestamo, int edad, double cuota, string ip){
            bool flag = true;
            PrestamoModel prestamo = PrestamosRepository.obtenerPrestamo(idPrestamo);
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "INSERT INTO LOG_REGISTRO(IDPRESTAMO,FECHACONSULTA,EDADCONSULTA,IPCONSULTA,VALORCONSULTA) values ('" + idPrestamo + "','"+ DateTime.Now + "','"+ edad + "','" + ip + "','" + cuota + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            Console.WriteLine("El resultado es: "+i);
            /*try
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
                //ejecucionSP.EjecutarSPConSalidas("sp_registrar_log" , listadoParametros , con, "@id" ,SqlDbType.Int ,ref flag);
                ejecucionSP.ExecuteSPWithNoDataReturn("sp_registrar_log", listadoParametros, con, ref flag);
            }
            catch
            {
                Console.WriteLine("Algo salio mal");
            }
            //Console.WriteLine(cuota);*/
            return flag;
        }
        public static List<LogModel> obtenerLogs()
        {
            List<LogModel> logList = new List<LogModel>();
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Global.getConnectionString();
                List<SqlParameter> listadoParametros = new List<SqlParameter>
                {
                };
                List<SqlParameter> listadoParametrosSalida = new List<SqlParameter>
                {
                };
                DataTable objetoSalida = ejecucionSP.ExecuteSPWithDataReturn("sp_obtener_logs", listadoParametros, con);
                if (objetoSalida.Rows.Count > 0)
                {
                    for (int i = 0; i < objetoSalida.Rows.Count; i++)
                    {
                        LogModel u = new LogModel();
                        PrestamoModel a = new PrestamoModel();
                        u.idConsulta = Convert.ToInt32(objetoSalida.Rows[i]["IDCONSULTA"]);
                        a = PrestamosRepository.obtenerPrestamo(Convert.ToInt32(objetoSalida.Rows[i]["IDPRESTAMO"]));
                        u.idFkPrestamo = a;
                        u.fechaConsulta =  Convert.ToString(objetoSalida.Rows[i]["FECHACONSULTA"]);
                        u.ipConsulta = Convert.ToString(objetoSalida.Rows[i]["IPCONSULTA"]);
                        u.valorConsulta = Convert.ToDouble(objetoSalida.Rows[i]["VALORCONSULTA"]);
                        u.edadConsulta = Convert.ToInt32(objetoSalida.Rows[i]["EDADCONSULTA"]);
                        logList.Add(u);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Algo salio mal...");
            }
            return logList;
        }
        public static class GetConString
        {
            public static string ConString()
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var config = builder.Build();
                string constring = config.GetConnectionString("DefaultConnection");
                return constring;
            }
        }
    }
}