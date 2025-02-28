using CQRS_with_dapper.Data.Query;
using CQRS_with_dapper.DBconnection;
using CQRS_with_dapper.Models;
using Dapper;
using MediatR;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CQRS_with_dapper.Data.Handler
{
    public class GetFarmerByIdHandler : IRequestHandler<GetFarmerById, Farmer?>
    {
        private readonly DBConnectionApp _connection;

        public GetFarmerByIdHandler(DBConnectionApp connection)
        {
            _connection = connection;
        }

        public async Task<Farmer?> Handle(GetFarmerById request, CancellationToken cancellationToken)
        {
            using (SqlConnection con = _connection.GetSqlConnection())
            {
                var farmer = await con.QueryFirstOrDefaultAsync<Farmer>(
                "GetByFarmerId",
                new { id = request.id },
                commandType: CommandType.StoredProcedure
            );

                return farmer; // Return the farmer (or null if not found)
            }
        }
    }
}
