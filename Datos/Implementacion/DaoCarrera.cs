using CRUDcarreras.Datos;
using CRUDcarreras.Dominio;
using CRUDcarreras.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDcarreras.Servicios.Implementacion
{
    class DaoCarrera : IDaoCarrera
    {
        public List<Materia> ObtenerMaterias()
        {
            List<Materia> lst = new List<Materia>();
            string sp = "sp_cargar_materias";
            DataTable table = Helper.ObtenerInstancia().CargarCombo(sp);
            foreach(DataRow dr in table.Rows)
            {
                int id = int.Parse(dr["idMateria"].ToString());
                string nombre = dr["nombre"].ToString();
                Materia m = new Materia(id, nombre);
                lst.Add(m);
            }
            return lst;

        }

        public int ObtenerProximo()
        {
            string nombreSp = "sp_proximo";
            string nombrePOut = "@next";
            return Helper.ObtenerInstancia().ObtenerProximo(nombreSp, nombrePOut);  
        }
    }
}
