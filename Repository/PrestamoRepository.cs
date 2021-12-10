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
            MesModel mes = new MesModel();
            int idMes = 0;
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
                        idMes=Convert.ToInt32(objetoSalida.Rows[i]["FKMESES"]);
                        mes = obtenerMes(idMes);
                        u.mesesPrestamo = mes;
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
        public static MesModel obtenerMes(int idMes){
            MesModel objeto = new MesModel();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Global.getConnectionString();
            SqlParameter mes = new SqlParameter();
            mes.ParameterName = "@idMes";
            mes.Value = idMes;
            List<SqlParameter> listadoParametros = new List<SqlParameter>
            {
                mes
            };
            DataTable objetoSalida = ejecucionSP.ExecuteSPWithDataReturn("dbo.sp_obtener_mes", listadoParametros, con);
            if (objetoSalida.Rows.Count > 0)
            {
                objeto.valorMes = Convert.ToInt32(objetoSalida.Rows[0]["VALOR_VALOR"]);
                objeto.idMes = idMes;
                objeto.descripcionMes = objetoSalida.Rows[0]["DESCRIPCION_VALOR"].ToString();
            }
            return objeto;
        }
        public static Object registrarPrestamo(String fecha, decimal monto, int meses){
            bool flag = true;
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Global.getConnectionString();

                SqlParameter fechaPres = new SqlParameter();
                fechaPres.ParameterName = "@fechaPrestamo"; 
                fechaPres.Value = fecha; 

                SqlParameter montoPres = new SqlParameter();
                montoPres.ParameterName = "@montoPrestamo"; 
                montoPres.Value = monto; 

                SqlParameter mesesPres = new SqlParameter();
                mesesPres.ParameterName = "@mesesPrestamo"; 
                mesesPres.Value = meses; 

                List<SqlParameter> listadoParametros = new List<SqlParameter>(){
                    fechaPres,
                    montoPres,
                    mesesPres
                };
                //dbo.
                //ejecucionSP.EjecutarSPConSalidas("sp_registrar_prestamo" , listadoParametros , con, "@id" ,SqlDbType.Int ,ref flag);
                ejecucionSP.ExecuteSPWithNoDataReturn("sp_registrar_prestamo", listadoParametros, con, ref flag);
            }
            catch
            {
                Console.WriteLine("Algo salio mal");
            }
            return flag;
        }
        public static List<MesModel> obtenerMeses()
        {
            List<MesModel> PresList = new List<MesModel>();
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
                DataTable objetoSalida = ejecucionSP.ExecuteSPWithDataReturn("sp_obtener_meses", listadoParametros, con);
                //DataTable objetoSalida2 = ejecucionSP.ExecuteSPWithDataReturn("sp_obtener_usuarios", listadoParametros, con);
                if (objetoSalida.Rows.Count > 0)
                {
                    for (int i = 0; i < objetoSalida.Rows.Count; i++)
                    {
                        MesModel u = new MesModel();
                        u.idMes = Convert.ToInt32(objetoSalida.Rows[i]["ID_VALOR"]);
                        u.descripcionMes = objetoSalida.Rows[i]["DESCRIPCION_VALOR"].ToString();
                        u.valorMes=Convert.ToInt32(objetoSalida.Rows[i]["VALOR_VALOR"]);
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

        public static List<TasaModel> obtenerTasas()
        {
            List<TasaModel> PresList = new List<TasaModel>();
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
                DataTable objetoSalida = ejecucionSP.ExecuteSPWithDataReturn("sp_obtener_tasas", listadoParametros, con);
                //DataTable objetoSalida2 = ejecucionSP.ExecuteSPWithDataReturn("sp_obtener_usuarios", listadoParametros, con);
                if (objetoSalida.Rows.Count > 0)
                {
                    for (int i = 0; i < objetoSalida.Rows.Count; i++)
                    {
                        TasaModel u = new TasaModel();
                        u.idTasa = Convert.ToInt32(objetoSalida.Rows[i]["ID_TASAS"]);
                        u.valorTasa = Convert.ToDouble(objetoSalida.Rows[i]["TASA_TASAS"]);
                        u.edadTasa  =  Convert.ToInt32(objetoSalida.Rows[i]["EDAD_TASAS"]);
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
        public static PrestamoModel obtenerPrestamo(int idPrestamo){
            PrestamoModel prestamo = new PrestamoModel();
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Global.getConnectionString();

                SqlParameter idPres = new SqlParameter();
                idPres.ParameterName = "@idPrestamo"; 
                idPres.Value = idPrestamo; 

                List<SqlParameter> listadoParametros = new List<SqlParameter>(){
                    idPres
                };
                DataTable objetoSalida = ejecucionSP.ExecuteSPWithDataReturn("dbo.sp_obtener_prestamo" , listadoParametros , con);
                if (objetoSalida.Rows.Count > 0)
                {
                    prestamo.idPrestamo = idPrestamo;
                    Console.WriteLine(Convert.ToInt32(objetoSalida.Rows[0]["FKMESES"]));
                    int idMes = Convert.ToInt32(objetoSalida.Rows[0]["FKMESES"]);
                    MesModel mes = obtenerMes(idMes);
                    prestamo.mesesPrestamo = mes;
                    prestamo.fechaPrestamo = objetoSalida.Rows[0]["FECHAPRESTAMO"].ToString();
                    prestamo.montoPrestamo = Convert.ToDouble(objetoSalida.Rows[0]["MONTOPRESTAMO"]);
                }
                //dbo.
                //ejecucionSP.EjecutarSPConSalidas("sp_registrar_prestamo" , listadoParametros , con, "@id" ,SqlDbType.Int ,ref flag);
                //ejecucionSP.ExecuteSPWithNoDataReturn("sp_registrar_prestamo", listadoParametros, con, ref flag);
            }
            catch
            {
                Console.WriteLine("Algo salio mal");
            }
            return prestamo;
        }
        public static bool eliminarPrestamo(int idPrestamo)
        {
            bool flag = true;
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Global.getConnectionString();

                SqlParameter idPres = new SqlParameter();
                idPres.ParameterName = "@idPrestamo"; 
                idPres.Value = idPrestamo; 

                List<SqlParameter> listadoParametros = new List<SqlParameter>(){
                    idPres
                };
                //dbo.
                //ejecucionSP.EjecutarSPConSalidas("sp_registrar_prestamo" , listadoParametros , con, "@id" ,SqlDbType.Int ,ref flag);
                ejecucionSP.ExecuteSPWithNoDataReturn("sp_eliminar_prestamo", listadoParametros, con, ref flag);
            }
            catch
            {
                Console.WriteLine("Algo salio mal");
            }
            return flag;
        }
    }
}
