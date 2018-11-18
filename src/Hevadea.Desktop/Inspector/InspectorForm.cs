using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenGLPlatform.Inspector
{
    public partial class InspectorForm : Form
    {
        public InspectorForm(object o)
        {
            InitializeComponent();
            ObjectInspector.SelectedObject = o;
        }
    }
}
