
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

//NOTE: IndexService uses Dapper to connect to the SQL database. Idealy all connections would be used with thsi for consistantcy sake and for ability to access different connection strings
//this is not the case right now. To connect using the relevant connection string change the argument in the SqlConnetion method.

namespace testing_app
{
    public class IndexService
    {
        connecter cc = new connecter(); //creates connection string to local db

        //method for loading Index Integration table
        public async Task<List<IndexIntegration>> GetIndexIntegrations()
        {
            using(IDbConnection connection = new SqlConnection(cc.connectionGetter()))
            { 
                return connection.Query<IndexIntegration>($"SELECT * FROM Integration").ToList();
            }
        }
    }
}
