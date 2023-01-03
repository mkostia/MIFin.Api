using System.Data.SqlClient;
using Dapper;

namespace MIFin.Api.Data
{
    public class DataRepository
    {
        private readonly string _connStr;
        private readonly string _connStrSec;

        public DataRepository(IConfiguration configuration)
        {
            _connStr = configuration["ConnectionStrings:MFin"]!;
            _connStrSec = configuration["ConnectionStrings:MFinSec"]!;
        }

        public string GetLoginByToken(string token)
        {
            string userName;
            using (var connection = new SqlConnection(_connStrSec)) {

                connection.Open();
                userName = connection.QueryFirst<string>("SELECT [UserName] FROM [dbo].[UsersTokens] where Token=@token", new { token });
            }
            return userName;
        }
        //public IEnumerable<QuestionGetManyResponse> GetQuestions() {
        //    using (var connection = new SqlConnection(_connectionString)) {
        //    }
        //}


    }
}
