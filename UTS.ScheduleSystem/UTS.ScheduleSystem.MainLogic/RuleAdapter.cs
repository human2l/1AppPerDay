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

        //public SqlDataReader LoadApprovedRules()
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string query = @"select Input, Output from ConversationalRule where Status='Approved'";
        //        SqlCommand command = new SqlCommand(query, connection);
        //        SqlDataReader dataReader = command.ExecuteReader();
        //        return dataReader;
        //    }
        //}

        //public SqlDataReader LoadRules(string[] columns, string table, string parameterType, string parameter)
        //{
        //    string _columns = UpCaseTypeLetter(columns[0]);
        //    for(int x = 1; x < columns.Length; x++)
        //    {
        //        _columns = _columns + ", " + UpCaseTypeLetter(columns[x]);
        //    }
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string query = @"select " + _columns + " from " + UpCaseTypeLetter(table) + " where " + UpCaseTypeLetter(parameterType) + "='" + parameter + "'";
        //        SqlCommand command = new SqlCommand(query, connection);
        //        SqlDataReader dataReader = command.ExecuteReader();
        //        return dataReader;
        //    }
        //}

        //public SqlDataReader LoadAllRules(string[] columns, string table)
        //{
        //    string _columns = UpCaseTypeLetter(columns[0]);
        //    for (int x = 1; x < columns.Length; x++)
        //    {
        //        _columns = _columns + ", " + UpCaseTypeLetter(columns[x]);
        //    }
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string query = @"select " + _columns + " from " + UpCaseTypeLetter(table);
        //        SqlCommand command = new SqlCommand(query, connection);
        //        SqlDataReader dataReader = command.ExecuteReader();
        //        return dataReader;
        //    }
        //}

        private string UpCaseTypeLetter(string type)
        {
            string output = type.Substring(0, 1).ToUpper() + type.Substring(1);
            return output;
        }
    }
}
