using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administracion_contable
{
    public class ElementoPlanContable
    {
        public string numeroIdentificador { get; set; }
        public string nombre { get; set; }

        public ElementoPlanContable(string id, string nombre)
        {
            this.numeroIdentificador = id;
            this.nombre = nombre;
        }
    }
}
