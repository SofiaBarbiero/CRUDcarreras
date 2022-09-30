using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDcarreras.Dominio
{
    class Carrera
    {
        public int IdCarrera { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public List<DetalleCarrera> Detalles { get; set; }

        public Carrera()
        {
            IdCarrera = 0;
            Nombre = string.Empty;
            Titulo = string.Empty;
            Detalles = new List<DetalleCarrera>();
        }
        public Carrera(int id, string nombre, string titulo, List<DetalleCarrera> detalles)
        {
            IdCarrera= id;
            Nombre = nombre;
            Titulo = titulo;
            Detalles = detalles;
        }

        public void AgregarDetalle(DetalleCarrera detalle)
        {
            Detalles.Add(detalle);
        }
        public void EliminarDetalle(int id)
        {
            Detalles.RemoveAt(id);
        }

        public int CalcularTotal()
        {
            int total = 0;
            foreach (DetalleCarrera detalle in Detalles)
            {
                total++;
            }
            return total;
        }

        public override string ToString()
        {
            return Nombre; 
        }
    }
}
