using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
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

        public string GetUserNameByToken(string token)
        {
            string userName;
            using (var connection = new SqlConnection(_connStrSec)) {

                connection.Open();
                userName = connection.QueryFirstOrDefault<string>("SELECT [UserName] FROM [dbo].[UsersTokens] where Token=@token", new { token });
            }
            return userName;
        }


        public void p_WF_TraceLogInsert(int processId, int taskId, int stageId,string title, string traceMessage, string  login) {
            var values = new { ProcessId = processId, TaskId = taskId, StageId= stageId, Title= title, TraceMessage= traceMessage, Login= login };
            using (var connection = new SqlConnection(_connStr)) {
                connection.Open();
                connection.Execute("p_WF_TraceLogInsert", values, commandType: CommandType.StoredProcedure);
            }
        }

        //public IEnumerable<QuestionGetManyResponse> GetQuestions() {
        //    using (var connection = new SqlConnection(_connectionString)) {
        //    }
        //}


        /*
         [p_WF_TraceLogInsert](@ProcessId int,@TaskId int=null,@StageId int =null,@Title nvarchar(500) = null, @TraceMessage nvarchar(max),@Login varchar(50))
         */

    }
}
