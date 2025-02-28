using MediatR;

namespace CQRS_with_dapper.Data.Command
{
    public class updateFarmerCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone_number { get; set; } = "";
        public updateFarmerCommand(int id)
        {
            Id = id;
        }

        public updateFarmerCommand(int id, string name, string address, string phone_number)
        {
            Id = id;
            Name = name;
            Address = address;
            Phone_number = phone_number;
        }
    }
}
