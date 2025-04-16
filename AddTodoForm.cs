using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoApp
{
    public partial class AddTodoForm : Form
    {
        public string TodoTitle => txtTitle.Text;
        public string TodoContent => txtContent.Text;
        public DateTime StartDate => dtStart.Value;
        public DateTime EndDate => dtEnd.Value;
        public int Priority => (int)numPriority.Value;

        public AddTodoForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

}
