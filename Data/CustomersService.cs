using Microsoft.Data.SqlClient;

namespace testing_app.Data
{
    public class CustomerService
    {
        connecter ccInstance = new connecter(); //class for establishing a connection to the database. All connections will be named cc for current connection
        public async Task<List<Customer>> GetCustomerAsync()
        {
            List<Customer> customerTable = new List<Customer>(); 
            string sql = "SELECT CustomerName, ID FROM Customer";
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
                                Customer customer = new Customer();

                                //Get all this data (below)
                                customer.CustomerName = reader.GetString(0);
                                customer.Id = reader.GetInt32(1);

                                //add this object (row) to the list of objects (the table)
                                customerTable.Add(customer);
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
            return customerTable; //return the list of rows(the table)
        }
        public void customerDeleter(Customer customerDeleted) //method for delete button, used to delete a row from the table
        {
            string cc = ccInstance.connectionGetter();
            string command = "DELETE FROM Customer WHERE ID = " + customerDeleted.Id; //deletes by ID
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
        public void customerEditer(Customer customerEdited) //method for editing entries in the site schedule table
        {
            string cc = ccInstance.connectionGetter();
            string command = "UPDATE Customer SET CustomerName = '" + customerEdited.CustomerName + "' WHERE ID = " + customerEdited.Id;
            try
            {
                using (SqlConnection conn = new SqlConnection(cc))
                {
                    using (SqlCommand cmd = new SqlCommand(command, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //Below are all the parameters to be be updated 
                        cmd.Parameters.AddWithValue("CustomerName", customerEdited?.CustomerName?.ToString() ?? String.Empty);
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
        public void customerAdder(Customer customerAdded) //method for add button, used to add a row to the site schedule table
        {
            string cc = ccInstance.connectionGetter();
            string command = "INSERT INTO Customer VALUES ('" + customerAdded.CustomerName + "')";
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
        public List<string> getCustomerNames()
        {
            List<string> customerNames = new List<string>(); 
            string cc = ccInstance.connectionGetter();
            string command = "SELECT CustomerName FROM Customer";
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
                                customerNames.Add(Convert.ToString(reader[0]?.ToString()) ?? String.Empty);
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
            return customerNames; //return the list of rows(the table)
        }
    }
}