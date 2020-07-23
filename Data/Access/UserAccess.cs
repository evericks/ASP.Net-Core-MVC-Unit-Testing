using Data.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Data.Access
{
    public class UserAccess
    {
        readonly SqlConnection connect = new SqlConnection("Server=tcp:oiog.database.windows.net,1433;Initial Catalog=Supper;Persist Security Info=False;User ID=oiog;Password=Sieuadmin123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public User CheckLogin(User user)
        {
            User result = null;
            SqlCommand command = new SqlCommand("CheckLogin", connect);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@password", user.Password);
            connect.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                result = new User();
                result.Username = reader.GetString(0);
                result.Name = reader.GetString(1);
                result.Email = reader.GetString(2);
                result.Role = reader.GetString(3);
                result.Status = reader.GetBoolean(4);
            }
            connect.Close();
            return result;
        }
        public int CheckDuplicate(string username, string email)
        {
            SqlCommand command = new SqlCommand("CheckDuplicate", connect);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@email", email);
            SqlParameter create = new SqlParameter();
            create.ParameterName = "@result";
            create.SqlDbType = SqlDbType.Int;
            create.Direction = ParameterDirection.Output;
            command.Parameters.Add(create);
            connect.Open();
            command.ExecuteNonQuery();
            int res = Convert.ToInt32(create.Value);
            connect.Close();
            return res;
        }

        public int CreateUser(User user)
        {
            int res = 0;
            if (CheckDuplicate(user.Username, user.Email) == 0)
            {
                SqlCommand command = new SqlCommand("CreateUser", connect);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@email", user.Email);
                SqlParameter create = new SqlParameter();
                create.ParameterName = "@result";
                create.SqlDbType = SqlDbType.Int;
                create.Direction = ParameterDirection.Output;
                command.Parameters.Add(create);
                connect.Open();
                command.ExecuteNonQuery();
                res = Convert.ToInt32(create.Value);
                connect.Close();
            }
            return res;
        }

        public bool ChangePass(User user)
        {
            bool res = false;
            SqlCommand command = new SqlCommand("ChangePass", connect);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@password", user.Password);
            connect.Open();
            res = command.ExecuteNonQuery() > 0;
            connect.Close();
            return res;
        }
    }
}
