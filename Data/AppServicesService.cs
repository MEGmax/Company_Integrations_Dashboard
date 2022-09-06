using Microsoft.Data.SqlClient;

namespace testing_app.Data
{
    public class AppServicesService
    {
        connecter ccInstance = new connecter(); //class for establishing a connection to the database. All connections will be named cc for current connection
        public async Task<List<AppService>> GetAppServices()
        {
            List<AppService> appServiceTable = new List<AppService>(); 
            string sql = "SELECT Id, AppServiceName FROM AppService";
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
                                AppService appService = new AppService();

                                //Get all this data (below)
                                appService.Id = reader.GetInt32(0);
                                appService.AppServiceName = reader.GetString(1);

                                //add this object (row) to the list of objects (the table)
                                appServiceTable.Add(appService);
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
            return appServiceTable; //return the list of rows(the table)
        }
        public void appServiceDeleter(AppService appServiceDeleted) //method for delete button, used to delete a row from the table
        {
            string cc = ccInstance.connectionGetter();
            string command = "DELETE FROM AppService WHERE ID = " + appServiceDeleted.Id; //deletes by ID
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
        public void appServiceEditer(AppService appServiceEdited) //method for editing entries in the site schedule table
        {
            string cc = ccInstance.connectionGetter();
            string command = "UPDATE AppService SET AppServiceName = '" + appServiceEdited.AppServiceName + "' WHERE ID = " + appServiceEdited.Id;
            try
            {
                using (SqlConnection conn = new SqlConnection(cc))
                {
                    using (SqlCommand cmd = new SqlCommand(command, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //Below are all the parameters to be be updated 
                        cmd.Parameters.AddWithValue("AppServiceName", appServiceEdited?.AppServiceName?.ToString() ?? String.Empty);
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
        public void appServiceAdder(AppService appServiceAdded) //method for add button, used to add a row to the site schedule table
        {
            string cc = ccInstance.connectionGetter();
            string command = "INSERT INTO AppService (AppServiceName) VALUES ('" + appServiceAdded.AppServiceName + "')";
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
        public List<string> getAppServiceNames()
        {
            List<string> appServiceNames = new List<string>(); 
            string cc = ccInstance.connectionGetter();
            string command = "SELECT AppServiceName FROM AppService";
            try
            {
                using (SqlConnection connection = new SqlConnection(cc))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(command, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read()) //while there are still rows to read through
                            {
                                appServiceNames.Add(Convert.ToString(reader[0]?.ToString()) ?? String.Empty);
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
            return appServiceNames; //return the list of rows(the table)
        }
    }
}