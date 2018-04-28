using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic
{
    class RuleAdapter
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public RuleAdapter()
        {

        }

        public void AddRule(string table, string[] data)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"insert into " + table + " (Id,Input,Ouput,RelatedUsersId,Status)values('" + data[0] + "','" + data[1] + "','" + data[2] + "','" + data[3] + "','" + data[4] + "')";
                var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        public void RemoveRule(string table, string Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"delete from " + table + " where Id='" + Id + "'";
                var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        public void EditRule(string table, string[] data)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"update " + table + " set Input='" + data[1] + "',Output='" + data[2] + "',RelatedUsersId='" + data[3] + "'where Id='" + data[0] + "'";
                var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        public string FindSingleValue(string table, string parameterType, string parameter, string clueType)
        {
            string result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"select " + clueType + " from " + table + " where " + parameterType + "='" + parameter;
                var command = new SqlCommand(query, connection);
                result = command.ExecuteScalar().ToString();
                command.Dispose();
            }
            return result;
        }
    }
}
