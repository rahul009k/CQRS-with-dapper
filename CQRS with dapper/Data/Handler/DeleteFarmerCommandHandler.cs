using CQRS_with_dapper.Data.Command;
using CQRS_with_dapper.DBconnection;
using Dapper;
using MediatR;
using System.Data;

namespace CQRS_with_dapper.Data.Handler
{
    public class DeleteFarmerCommandHandler : IRequestHandler<DeleteFarmerCommand, bool>
    {
        private readonly DBConnectionApp _connection;

        public DeleteFarmerCommandHandler(DBConnectionApp connection)
        {
            _connection = connection;
        }

        public async Task<bool> Handle(DeleteFarmerCommand request, CancellationToken cancellationToken)
        {
            using (var con = _connection.GetSqlConnection())
            {
                var result = await con.QueryFirstOrDefaultAsync<int>("DeleteFarmer", new { Id = request.Id }, commandType: CommandType.StoredProcedure);
                return result >0;
            }
            }
    }
}
