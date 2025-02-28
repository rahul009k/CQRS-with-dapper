using CQRS_with_dapper.Data.Command;
using CQRS_with_dapper.DBconnection;
using Dapper;
using MediatR;
using System.Data;

namespace CQRS_with_dapper.Data.Handler
{
    public class UpdateFarmerCommandHandler : IRequestHandler<updateFarmerCommand, bool>
    {
        private readonly DBConnectionApp _connection;

        public UpdateFarmerCommandHandler(DBConnectionApp connection)
        {
            _connection = connection;
        }

        public async Task<bool> Handle(updateFarmerCommand request, CancellationToken cancellationToken)
        {
            using (var con = _connection.GetSqlConnection())
            {
                var param = new
                {
                    Id=request.Id,
                    Name = request.Name,
                    Address = request.Address,
                    Phone_Number = request.Phone_number
                };
                int result = await con.QueryFirstOrDefaultAsync<int>("UpdateFarmer", param, commandType: CommandType.StoredProcedure);
                return result >0;
            }
            }
    }
}
