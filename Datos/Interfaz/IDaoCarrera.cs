using CRUDcarreras.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDcarreras.Servicios.Interfaz
{
    interface IDaoCarrera
    {
        int ObtenerProximo();
        List<Materia> ObtenerMaterias();
    }
}
