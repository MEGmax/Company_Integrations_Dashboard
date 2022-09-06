using Microsoft.Data.SqlClient;

namespace testing_app.Data
{
    public class SiteScheduleService
    {
        connecter ccInstance = new connecter(); //class for establishing a connection to the database. All connections will be named cc for current connection
        public async Task<List<SiteSchedule>> GetSiteSchedulesAsync()
        {
            List<SiteSchedule> siteSchedules = new List<SiteSchedule>(); //this list of siteSchedule objects (rows) will make up the table. This is essentially the table
            string sql = "Select Id, SiteName, CronExpression, Enabled, Description, IntegrationName, ConfigFilePath, RunAllScheduled, AppService, CustomerName From SiteSchedulev3";
            string cc = ccInstance.connectionGetter();
            try
            {
                await using (SqlConnection connection = new SqlConnection(cc))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) //while there are still rows to read through
                            {
                                //create a new siteSchedule object(which is the new row)
                                SiteSchedule ss = new SiteSchedule();

                                //Get all this data (below)
                                ss.Id = reader.GetInt32(0);
                                ss.SiteName = reader.GetString(1);
                                ss.CronExpression = reader.GetString(2);
                                ss.Enabled = reader.GetBoolean(3);
                                ss.Description = reader.GetString(4);
                                ss.IntegrationName = reader.GetString(5) == null ? "" : reader.GetString(5);
                                ss.ConfigFilePath = reader.GetString(6) == null ? "" : reader.GetString(6);
                                ss.RunAllScheduled = reader.GetBoolean(7);
                                ss.AppServiceName = getString(reader[8]);
                                ss.CustomerName = getString(reader[9]);

                                //add this object (row) to the list of objects (the table)
                                siteSchedules.Add(ss);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            //Below is used for errors, may not run properly due to console output
            catch(Exception ex) {
                Console.Write(ex.Message);
            }
            return siteSchedules; //return the list of rows(the table)
        }
        public void siteDeleter(SiteSchedule siteDeleted) //method for delete button, used to delete a row from the table
        {
            string cc = ccInstance.connectionGetter();
            string command = "DELETE FROM SiteSchedulev3 WHERE ID = " + siteDeleted.Id; //deletes by ID
            try
            {
                using (SqlConnection conn = new SqlConnection(cc))
                {
                    using (SqlCommand cmd = new SqlCommand(command, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = command;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            //Below is used for errors, may not run properly due to console output
            catch(Exception ex) {
                Console.Write(ex.Message);
            }
        }
        public void siteAdder(SiteSchedule siteAdded) //method for add button, used to add a row to the site schedule table
        {
            string cc = ccInstance.connectionGetter();
            string command = "INSERT INTO SiteSchedulev3 VALUES ('" + siteAdded.SiteName + "', '" + siteAdded.CronExpression + "', '" + siteAdded.Enabled + "', '" + siteAdded.RunAllScheduled + "', '" + siteAdded.Description + "', '" + siteAdded.IntegrationName + "', '" + siteAdded.ConfigFilePath + "', '" + siteAdded.AppServiceName + "', '" + siteAdded.CustomerName + "')";
            try
            {
                using (SqlConnection conn = new SqlConnection(cc))
                {
                    using (SqlCommand cmd = new SqlCommand(command, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = command;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            //Below is used for errors, may not run properly due to console output
            catch(Exception ex) {
                Console.Write(ex.Message);
            }
        }
        public void sqlUpdater(SiteSchedule siteEdited) //method for editing entries in the site schedule table
        {
            string cc = ccInstance.connectionGetter();
            string command = "UPDATE SiteSchedulev3 SET SITENAME ='" + siteEdited.SiteName + "', CRONEXPRESSION = '" + siteEdited.CronExpression + "', ENABLED = '" + siteEdited.Enabled + "', RUNALLSCHEDULED = '" + siteEdited.RunAllScheduled + "', DESCRIPTION = '" + siteEdited.Description + "', INTEGRATIONNAME = '" + siteEdited.IntegrationName + "', CONFIGFILEPATH = '" + siteEdited.ConfigFilePath + "', APPSERVICE = '" + siteEdited.AppServiceName + "', CUSTOMERNAME = '" + siteEdited.CustomerName + "' WHERE ID = " + siteEdited.Id;
            try
            {
                using (SqlConnection conn = new SqlConnection(cc))
                {
                    using (SqlCommand cmd = new SqlCommand(command, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //Below are all the parameters to be be updated 
                        cmd.Parameters.AddWithValue("SiteName", siteEdited.SiteName);
                        cmd.Parameters.AddWithValue("Enabled", siteEdited.Enabled);
                        cmd.Parameters.AddWithValue("CronExpression", siteEdited.CronExpression);
                        cmd.Parameters.AddWithValue("RunAllScheduled", siteEdited.RunAllScheduled);
                        cmd.Parameters.AddWithValue("Description", siteEdited.Description);
                        cmd.Parameters.AddWithValue("IntegrationName", siteEdited.IntegrationName);
                        cmd.Parameters.AddWithValue("ConfigFilePath", siteEdited.ConfigFilePath);
                        cmd.Parameters.AddWithValue("AppService", siteEdited?.AppServiceName?.ToString() ?? String.Empty);
                        cmd.Parameters.AddWithValue("CustomerName", siteEdited?.CustomerName?.ToString() ?? String.Empty);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            //Below is used for errors, may not run properly due to console output
            catch(Exception ex) {
                Console.Write(ex.Message);
            }
        }
        private static string getString(object o) //this method deals with objects that may be null
        {
            if (o == DBNull.Value) //if the object is null return an empty string
            {
                return "";
            }
            return (string) o; //if the object is not null return the object as a string
        }
    }
}