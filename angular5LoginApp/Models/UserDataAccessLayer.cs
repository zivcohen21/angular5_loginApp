using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace angular5LoginApp.Models
{

    public class UserDataAccessLayer
    {
        string connectionString = "server=localhost;port=3306;database=userslist;username=root;password=zivcohen123;SslMode=none";
        //To View all users details
        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                List<User> lstuser = new List<User>();
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("spGetAllUsers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        User user = new User();
                        user.UserID = Convert.ToInt32(rdr["UserId"]);
                        user.FirstName = rdr["FirstName"].ToString();
                        user.LastName = rdr["LastName"].ToString();
                        user.Username = rdr["Username"].ToString();
                        user.Password = rdr["Password"].ToString();
                        lstuser.Add(user);
                    }
                    con.Close();
                }
                return lstuser;
            }
            catch
            {
                throw;
            }
        }
        //To Add new user record 
        public int AddUser(User user)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("spAddUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("firstName", user.FirstName);
                    cmd.Parameters.AddWithValue("lastName", user.LastName);
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("password", user.Password);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }
        //To Update the records of a particluar user
        public int UpdateUser(User user)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("spUpdateUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("userId", user.UserID);
                    cmd.Parameters.AddWithValue("firstName", user.FirstName);
                    cmd.Parameters.AddWithValue("lastName", user.LastName);
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("password", user.Password);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }
        //Get the details of a particular user
        public User GetUserData(int id)
        {
            try
            {
                User user = new User();
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT * FROM tblUser WHERE UserId= " + id;
                    MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        user.UserID = Convert.ToInt32(rdr["UserId"]);
                        user.FirstName = rdr["FirstName"].ToString();
                        user.LastName = rdr["LastName"].ToString();
                        user.Username = rdr["Username"].ToString();
                        user.Password = rdr["Password"].ToString();
                    }
                }
                return user;
            }
            catch
            {
                throw;
            }
        }
        //To Delete the record on a particular user
        public int DeleteUser(int id)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("spDeleteUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("userId", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public User Auth(AuthData authData)
        {
            try
            {
                User user =  new User();
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT * FROM tblUser WHERE Password= " + authData.Password + " AND Username= " + authData.Username;
                    MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        user.UserID = Convert.ToInt32(rdr["UserId"]);
                        user.FirstName = rdr["FirstName"].ToString();
                        user.LastName = rdr["LastName"].ToString();
                        user.Username = rdr["Username"].ToString();
                        user.Password = rdr["Password"].ToString();
                    }
                }
                return user;
            }
            catch
            {
                throw;
            }
        }
    }
}
