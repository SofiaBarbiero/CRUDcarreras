using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDcarreras.Dominio
{
    class Materia
    {
        public int IdMateria { get; set; }
        public string Nombre { get; set; }

        public Materia()
        {
            IdMateria = 0;
            Nombre = string.Empty;
        }
        public Materia(int id, string nombre)
        {
            IdMateria = id;
            Nombre = nombre;
        }

        public override string ToString()
        {
            return "Materia: " + Nombre;
        }
       
    }
}
