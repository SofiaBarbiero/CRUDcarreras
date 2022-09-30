using CRUDcarreras.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDcarreras.Datos
{
    class Helper
    {
        public static Helper instancia;
        public SqlConnection cnn;

        public static Helper ObtenerInstancia()
        {
            if(instancia == null)
            {
                instancia = new Helper();
            }
            return instancia;
        }
        public Helper()
        {
            cnn = new SqlConnection(Properties.Resources.cnnString);
        }

        public int ObtenerProximo(string nombreSp, string nombrePOut)
        {
            SqlCommand cmdProx = new SqlCommand();
            cnn.Open();
            cmdProx.Connection = cnn;
            cmdProx.CommandType = CommandType.StoredProcedure;
            cmdProx.CommandText = nombreSp;
            SqlParameter pOut = new SqlParameter();
            pOut.ParameterName = nombrePOut;
            pOut.DbType = DbType.Int32;
            pOut.Direction = ParameterDirection.Output;
            cmdProx.Parameters.Add(pOut);
            cmdProx.ExecuteNonQuery();
            cnn.Close();
            return (int)pOut.Value;
        }

        public DataTable CargarCombo(string sp)
        {
            DataTable table = new DataTable();
            SqlCommand cmdCombo = new SqlCommand();
            cnn.Open();
            cmdCombo.Connection = cnn;
            cmdCombo.CommandType = CommandType.StoredProcedure;
            cmdCombo.CommandText = sp;
            table.Load(cmdCombo.ExecuteReader());
            cnn.Close();
            return table;
        }

        public bool ConfirmarCarrera(Carrera c)
        {
            bool ok = true;
            SqlTransaction t = null;
            try
            {
                SqlCommand cmdMaestro = new SqlCommand();
                cnn.Open();
                t = cnn.BeginTransaction();
                cmdMaestro.Connection = cnn;
                cmdMaestro.Transaction = t;
                cmdMaestro.CommandType = CommandType.StoredProcedure;
                cmdMaestro.CommandText = "sp_insertar_carrera";
                cmdMaestro.Parameters.AddWithValue("@nombre", c.Nombre);
                cmdMaestro.Parameters.AddWithValue("@titulo", c.Titulo);
                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@idCarrera";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                cmdMaestro.Parameters.Add(pOut);
                cmdMaestro.ExecuteNonQuery();

                int idCarrera = (int)pOut.Value;

                foreach(DetalleCarrera d in c.Detalles)
                {
                    SqlCommand cmdDetalle = new SqlCommand();
                    cmdDetalle.Connection = cnn;
                    cmdDetalle.Transaction = t;
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.CommandText = "sp_insertar_detalle";
                    cmdDetalle.Parameters.AddWithValue("@idCarrera", idCarrera);
                    cmdDetalle.Parameters.AddWithValue("@anioCursado", d.AnioCursado);
                    cmdDetalle.Parameters.AddWithValue("@cuatrimestre", d.Cuatrimestre);
                    cmdDetalle.Parameters.AddWithValue("@idMateria", d.Materia.IdMateria);
                    cmdDetalle.ExecuteNonQuery(); 
                }
                t.Commit();
            }
            catch(Exception)
            {
                if(t != null)
                {
                    t.Rollback();
                    ok = false;
                }
            }
            finally
            {
                if(cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return ok;

        }

    }
}
