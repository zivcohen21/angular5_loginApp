using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.IdentityModel.Tokens.Jwt;

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
                string hashedPassword = hashingPassword(user.Password);
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("spAddUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("firstName", user.FirstName);
                    cmd.Parameters.AddWithValue("lastName", user.LastName);
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("password", hashedPassword);
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
                string hashedPassword = hashingPassword(user.Password);
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("spUpdateUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("userId", user.UserID);
                    cmd.Parameters.AddWithValue("firstName", user.FirstName);
                    cmd.Parameters.AddWithValue("lastName", user.LastName);
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("password", hashedPassword);
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
                    string sqlQuery = "SELECT * FROM tbluser WHERE UserId= " + id;
                    MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        user.UserID = Convert.ToInt32(rdr["UserId"]);
                        user.FirstName = rdr["FirstName"].ToString();
                        user.LastName = rdr["LastName"].ToString();
                        user.Username = rdr["Username"].ToString();
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

        public Result Auth(AuthData authData)
        {
            try
            {

                string hashedPassword = hashingPassword(authData.password);

                Result result = new Result();
                User user =  new User();
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    Console.Write(authData);
                    string sqlQuery = "SELECT * FROM tbluser WHERE Username='" + authData.username + "'";
                    MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();               
                    while (rdr.Read())
                    {
                        String a = rdr["Password"].ToString();
                        if (KeyDerivation.Equals(hashedPassword, a))
                        {
                            user.UserID = Convert.ToInt32(rdr["UserId"]);
                            user.FirstName = rdr["FirstName"].ToString();
                            user.LastName = rdr["LastName"].ToString();
                            user.Username = rdr["Username"].ToString();

                            result.User = user;
                            return result;
                        }                        
                    }
                }
                result.Message = "Invalid Username or Password";    
                
                return result;
                
            }
            catch
            {
                throw;
            }
        }

        public string hashingPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(salt);
            //}
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));            
            return hashed;
        }
    }
}
