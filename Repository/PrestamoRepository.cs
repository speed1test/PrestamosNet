using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Prestamos.Models;
using Prestamos.Utils;
using Microsoft.Extensions.Logging;
using Prestamos.Controllers;
namespace Prestamos.Repository
{
    public class PrestamosRepository
    {
        public static List<PrestamoModel> obtenerPrestamos()
        {
            List<PrestamoModel> PresList = new List<PrestamoModel>();
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
                List<SqlParameter> listadoParametros2 = new List<SqlParameter>
                {
                };
                List<SqlParameter> listadoParametrosSalida2 = new List<SqlParameter>
                {
                };
                DataTable objetoSalida = ejecucionSP.ExecuteSPWithDataReturn("sp_obtener_prestamos", listadoParametros, con);
                //DataTable objetoSalida2 = ejecucionSP.ExecuteSPWithDataReturn("sp_obtener_usuarios", listadoParametros, con);
                if (objetoSalida.Rows.Count > 0)
                {
                    for (int i = 0; i < objetoSalida.Rows.Count; i++)
                    {
                        PrestamoModel u = new PrestamoModel();
                        u.idPrestamo = Convert.ToInt32(objetoSalida.Rows[i]["IDPRESTAMO"]);
                        u.fechaPrestamo = objetoSalida.Rows[i]["FECHAPRESTAMO"].ToString();
                        u.mesesPrestamo = Convert.ToInt32(objetoSalida.Rows[i]["MESESPRESTAMO"]);
                        u.montoPrestamo = Convert.ToDouble(objetoSalida.Rows[i]["MONTOPRESTAMO"]);
                        PresList.Add(u);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Algo salio mal");
            }
            return PresList;
        }
    }
}
