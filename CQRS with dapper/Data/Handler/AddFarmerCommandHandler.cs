using CQRS_with_dapper.Data.Command;
using CQRS_with_dapper.DBconnection;
using CQRS_with_dapper.Models;
using Dapper;
using MediatR;
using System.Data;

namespace CQRS_with_dapper.Data.Handler
{
    public class AddFarmerCommandHandler : IRequestHandler<AddFarmerCommand, FarmerDto>
    {
        private readonly DBConnectionApp _connection;

        public AddFarmerCommandHandler(DBConnectionApp connection)
        {
            _connection = connection;
        }

        public async Task<FarmerDto> Handle(AddFarmerCommand request, CancellationToken cancellationToken)
        {
            using(var con=_connection.GetSqlConnection())
            {
                var param = new
                {
                    
                    Name = request.Name,
                    Address = request.Address,
                    Phone_Number = request.Phone_number
                };
                var result = await con.QuerySingleOrDefaultAsync<FarmerDto>(
                    "AddFarmer",
                    param,
                    commandType: CommandType.StoredProcedure
                );
                if (result != null)
                    return result;
                else 
                    throw new Exception("Failed to insert farmer into the database.");
            }
        }
    }
}
