using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_HTPT
{
    class SQLConnectionManager
    {
        private static string serverName = "", databaseName = "", userID = "", password = "";

        private static SqlConnection connection;

        public static SqlConnection Connection => connection;

        public static string ServerName
        {
            get => serverName;

            set
            {
                if (value != null)
                {
                    serverName = value;
                }
            }
        }

        public static string DatabaseName
        {
            get => databaseName;

            set
            {
                if (value != null)
                {
                    databaseName = value;
                }
            }
        }

        public static string UserID
        {
            get => userID;

            set
            {
                userID = value;
            }
        }

        public static string Password
        {
            get => password;

            set
            {
                if (value != null)
                {
                    password = value;
                }
            }
        }

        public static bool Login()
        {
            bool check = true;
            try
            {
                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }
                connection = new SqlConnection
                {
                    ConnectionString = $"Data Source={serverName};Initial Catalog={databaseName};User ID={userID};Password={password};"
                };
                connection.Open();
                connection.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Class CSConnectSQL error: " + e.Message);
                check = false;
            }
            return check;
        }

        public static bool Login(string serverName, string databaseName, string userID, string password)
        {
            bool check = true;
            try
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                connection = new SqlConnection
                {
                    ConnectionString = $"Data Source={serverName};Initial Catalog={databaseName};User ID={userID};Password={password};"
                };
                connection.Open();
                connection.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Class CSConnectSQL error: " + e.Message);
                check = false;
            }
            return check;
        }

        public static bool Open()
        {
            bool check = true;
            if (Connection != null && connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Class CSConnectSQL error: " + e.Message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    check = false;
                }
            }
            else
            {
                check = false;
            }
            return check;
        }

        public static bool Close()
        {
            bool check = true;
            if (connection != null && connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Close();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Class CSConnectSQL error: " + e.Message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    check = false;
                }
            }
            return check;
        }

        public static DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();
            if (connection != null)
            {
                try
                {
                    SQLConnectionManager.Open();
                    using (SqlCommand command = new SqlCommand(query, Connection))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                    SQLConnectionManager.Close();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Class CSConnectSQL error: " + e.Message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dataTable;
        }

        public static DataTable ExecuteQuery(SqlCommand cmd)
        {
            DataTable dataTable = new DataTable();
            if (connection != null)
            {
                try
                {
                    SQLConnectionManager.Open();
                    cmd.Connection = connection;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                    SQLConnectionManager.Close();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Class CSConnectSQL error: " + e.Message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dataTable;
        }



        public static bool ExecuteNonQuery(string sql)
        {
            bool check = true;
            try
            {
                SQLConnectionManager.Open();
                using (SqlCommand command = new SqlCommand(sql, Connection))
                {
                    command.ExecuteNonQuery();
                }
                SQLConnectionManager.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Class CSConnectSQL error: " + e.Message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                check = false;
            }
            return check;
        }

        public static bool ExecuteNonQuery(SqlCommand cmd)
        {
            bool check = true;
            try
            {
                SQLConnectionManager.Open();
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                SQLConnectionManager.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Class CSConnectSQL error: " + e.Message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                check = false;
            }
            return check;
        }

        public static object ExecuteScalar(string sql)
        {
            object res = null;
            try
            {
                SQLConnectionManager.Open();
                using (SqlCommand command = new SqlCommand(sql, Connection))
                {
                    res = command.ExecuteScalar();
                }
                SQLConnectionManager.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Class CSConnectSQL error: " + e.Message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return res;
        }
        public static object ExecuteScalar2(string storedProcedureName, SqlParameter[] parameters)
        {
            object res = null;
            try
            {
                SQLConnectionManager.Open();
                using (SqlCommand command = new SqlCommand(storedProcedureName, Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    res = command.ExecuteScalar();
                }
                SQLConnectionManager.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Class CSConnectSQL error: " + e.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return res;
        }


        public static object ExecuteScalar(SqlCommand cmd)
        {
            object res = null;
            try
            {
                SQLConnectionManager.Open();
                res = cmd.ExecuteScalar();
                SQLConnectionManager.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Class CSConnectSQL error: " + e.Message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                res = new object();
            }
            return res;
        }
    }
}
