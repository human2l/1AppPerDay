using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.ScheduleSystem.MainLogic
{
    class MealScheduleAdapter
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public MealScheduleAdapter()
        {

        }

        public void AddMealSchedule(string[] data)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"insert into MealSchedule(Id,Topic,Participants,Location,StartDate,EndDate,LastEditUserId)values('" + data[0] + "','" + data[1] + "','" + data[2] + "','" + data[3] + "','" + data[4] + "','" + data[5] + "','" + data[6] + "')";
                var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        public void RemoveMealSchedule(string Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"delete from MealSchedule where Id='" + Id + "'";
                var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        public void EditMealSchedule(string[] data)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"update MealSchedule set Topic='" + data[1] + "',Participants='" + data[2] + "',Location='" + data[3] + "',StartDate='" + data[4] + "',EndDate='" + data[5] + "where Id='" + data[0] + "'";
                var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        public string FindSingleValue(string parameterType, string parameter, string clueType )
        {
            string result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"select " + clueType + " from MealSchedule where " + parameterType + "='" + parameter;
                var command = new SqlCommand(query, connection);
                result = command.ExecuteScalar().ToString();
                command.Dispose();
            }
            return result;
        }


    }
}
