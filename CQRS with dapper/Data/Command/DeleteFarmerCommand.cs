using MediatR;

namespace CQRS_with_dapper.Data.Command
{
    public class DeleteFarmerCommand:IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteFarmerCommand(int id)
        {
            Id = id;
        }
    }
}
