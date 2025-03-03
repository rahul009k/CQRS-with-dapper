using CQRS_with_dapper.Models;
using MediatR;

namespace CQRS_with_dapper.Data.Query
{
    public class GetAllFarmer:IRequest<List<Farmer?>>
    {
    }
}
