using CQRS_with_dapper.Models;
using MediatR;

namespace CQRS_with_dapper.Data.Query
{
    public class GetFarmerById:IRequest<Farmer ?>
    {
        public int id { get; set; }

        public GetFarmerById(int id)
        {
            this.id = id;
        }
    }
}
