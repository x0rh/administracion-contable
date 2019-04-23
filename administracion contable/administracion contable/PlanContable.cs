using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace administracion_contable
{
    public partial class PlanContable : Form
    {
        //aca van a ir todos los elementos del plan contable antes de ir a la base de datos
        public List<ElementoPlanContable> elementosPlanContables { get; set; }

        public PlanContable()
        {
            elementosPlanContables = new List<ElementoPlanContable>();
            InitializeComponent();
        }

        
    }
}
