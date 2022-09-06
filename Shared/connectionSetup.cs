using sys = System.Configuration;

namespace testing_app
{
    public class connecter
    {
        //EDIT THE VARIABLES IN CONNECTIONGETTER. This will build a connection string to wherever relevant. Alter the final string 'connection' if changing the datasource database etc. variables causes problems
        public string connectionGetter() //used to establish a connection to a sql database, to load the content of the webpage
        {
            string datasource = @"L128"; 
            string database = "testing";
            string username = "sa";
            string password = "temppassword1";
            string connection =  @"Data Source="+datasource+";Initial Catalog="+database+";Persist Security Info=True;User ID="+username+";Password="+password+";Encrypt=False"; 
            return connection;
        }

        //This is a better method that can be implimented in the future. Allows for easier access to different connection strings that can be specified in the appSettings file. Ignore for now
        //-----------------------------------------------------------------------------------------------
        public static string ConnectionValue(string name)
        {
            //var connectionString = config.GetSection("ConnectionStrings"
            var connString = sys.ConfigurationManager.ConnectionStrings[name].ConnectionString;
            return connString;
        }

    }

}