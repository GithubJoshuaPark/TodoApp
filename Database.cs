using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace TodoApp
{
    public class Database
    {
        private string connectionString = "server=localhost;port=3307;database=todo_db;uid=root;pwd=cdcdcd001!";

        public DataTable GetTodos(int userId)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM todos WHERE user_id = @userId ORDER BY start_dt ASC";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                var adapter = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public void AddTodo(int userId, string title, string content, DateTime startDt, DateTime endDt, int priority)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO todos (user_id, title, content, start_dt, end_dt, priority, is_finished)
                                 VALUES (@userId, @title, @content, @startDt, @endDt, @priority, 0)";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@content", content ?? "");
                cmd.Parameters.AddWithValue("@startDt", startDt);
                cmd.Parameters.AddWithValue("@endDt", endDt);
                cmd.Parameters.AddWithValue("@priority", priority);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateTodo(int id, string title, string content, DateTime startDt, DateTime endDt, int priority, bool isFinished)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE todos SET 
                                 title=@title,
                                 content=@content,
                                 start_dt=@startDt,
                                 end_dt=@endDt,
                                 priority=@priority,
                                 is_finished=@isFinished,
                                 updated_at=NOW()
                                 WHERE id=@id";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@content", content ?? "");
                cmd.Parameters.AddWithValue("@startDt", startDt);
                cmd.Parameters.AddWithValue("@endDt", endDt);
                cmd.Parameters.AddWithValue("@priority", priority);
                cmd.Parameters.AddWithValue("@isFinished", isFinished);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTodo(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM todos WHERE id=@id";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetUsers()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, name FROM user_profiles ORDER BY name";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                var adapter = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

    }
}
