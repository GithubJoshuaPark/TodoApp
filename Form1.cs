using System;
using System.Data;
using System.Windows.Forms;

namespace TodoApp
{
    public partial class Form1 : Form
    {
        private Database db = new Database();

        public Form1()
        {
            InitializeComponent();
            comboUsers.SelectedIndexChanged += comboUsers_SelectedIndexChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var splash = new SplashForm())
            {
                splash.ShowDialog();
            }

            // Then load users/todos etc.
            LoadUsers();
        }

        private void LoadUsers()
        {
            DataTable users = db.GetUsers();
            comboUsers.DataSource = users;
            comboUsers.DisplayMember = "name";
            comboUsers.ValueMember = "id";

            if (users.Rows.Count > 0)
                LoadTodos(Convert.ToInt32(comboUsers.SelectedValue));
        }

        private void AdjustGridColumnWidths()
        {
            dataGridView1.Columns["id"].Width = 50;
            dataGridView1.Columns["user_id"].Width = 60;
            dataGridView1.Columns["title"].Width = 150;
            dataGridView1.Columns["content"].Width = 200;
            dataGridView1.Columns["start_dt"].Width = 90;
            dataGridView1.Columns["end_dt"].Width = 90;
            dataGridView1.Columns["priority"].Width = 60;
            dataGridView1.Columns["is_finished"].Width = 70;
            dataGridView1.Columns["created_at"].Width = 120;
            dataGridView1.Columns["updated_at"].Width = 120;
            dataGridView1.Columns["Edit"].Width = 60;
        }

        private void comboUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboUsers.SelectedValue != null && comboUsers.SelectedValue is int)
            {
                LoadTodos(Convert.ToInt32(comboUsers.SelectedValue));
            }
        }

        private void LoadTodos(int userId)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = db.GetTodos(userId);
            AddEditButtonColumn();
            AdjustGridColumnWidths();
        }

        private void menuAddTodo_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddTodoForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    int userId = Convert.ToInt32(comboUsers.SelectedValue);
                    db.AddTodo(
                        userId,
                        addForm.TodoTitle,
                        addForm.TodoContent,
                        addForm.StartDate,
                        addForm.EndDate,
                        addForm.Priority
                    );

                    LoadTodos(userId);
                }
            }
        }

        private void AddEditButtonColumn()
        {
            if (!dataGridView1.Columns.Contains("Edit"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "Edit";
                btn.Text = "Edit";
                btn.Name = "Edit";

                // Set button styles  
                btn.DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;
                btn.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                btn.DefaultCellStyle.Font = new System.Drawing.Font(dataGridView1.Font, System.Drawing.FontStyle.Bold);

                btn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btn);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                int id = Convert.ToInt32(row.Cells["id"].Value);
                string title = row.Cells["title"].Value.ToString();
                string content = row.Cells["content"].Value?.ToString() ?? "";
                DateTime start = Convert.ToDateTime(row.Cells["start_dt"].Value);
                DateTime end = Convert.ToDateTime(row.Cells["end_dt"].Value);
                int priority = Convert.ToInt32(row.Cells["priority"].Value);
                bool isFinished = Convert.ToBoolean(row.Cells["is_finished"].Value);

                using (var editForm = new EditTodoForm(db, id, title, content, start, end, priority, isFinished))
                {
                    var result = editForm.ShowDialog();
                    if (result == DialogResult.OK || result == DialogResult.Abort)
                    {
                        LoadTodos(Convert.ToInt32(comboUsers.SelectedValue));
                    }
                }
            }
        }

    }
}
