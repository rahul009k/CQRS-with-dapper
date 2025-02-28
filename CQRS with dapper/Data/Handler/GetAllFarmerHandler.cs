using CQRS_with_dapper.Data.Query;
using CQRS_with_dapper.DBconnection;
using CQRS_with_dapper.Models;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CQRS_with_dapper.Data.Handler
{
    public class GetAllFarmerHandler : IRequestHandler<GetAllFarmer, List<Farmer>>
    {
        private readonly DBConnectionApp _connection;

        public GetAllFarmerHandler(DBConnectionApp connection)
        {
            _connection = connection;
        }

        public async Task<List<Farmer>> Handle(GetAllFarmer request, CancellationToken cancellationToken)
        {
           using(SqlConnection con=_connection.GetSqlConnection())
            {
                var result = await con.QueryAsync<Farmer>("GetallFarmer", CommandType.StoredProcedure);
                if(result!=null)
                {
                    return result.ToList(); 
                }
                else
                {
                    return new List<Farmer>();
                }

            }
        }
    }
}
