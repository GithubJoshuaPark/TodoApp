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
    public partial class EditTodoForm : Form
    {
        public string TodoTitle => txtTitle.Text;
        public string TodoContent => txtContent.Text;
        public DateTime StartDate => dtStart.Value;
        public DateTime EndDate => dtEnd.Value;
        public int Priority => (int)numPriority.Value;
        public bool IsFinished => chkFinished.Checked;

        private readonly int todoId;
        private readonly Database db;

        public EditTodoForm(Database database, int id, string title, string content, DateTime start, DateTime end, int priority, bool isFinished)
        {
            InitializeComponent();

            db = database;
            todoId = id;

            txtTitle.Text = title;
            txtContent.Text = content;
            dtStart.Value = start;
            dtEnd.Value = end;
            numPriority.Value = priority;
            chkFinished.Checked = isFinished;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            db.UpdateTodo(
                todoId,
                txtTitle.Text,
                txtContent.Text,
                dtStart.Value,
                dtEnd.Value,
                (int)numPriority.Value,
                chkFinished.Checked
            );
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to delete this todo?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                db.DeleteTodo(todoId);
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

}
