using CQRS_with_dapper.Models;
using MediatR;

namespace CQRS_with_dapper.Data.Command
{
    public class AddFarmerCommand:IRequest<FarmerDto?>
    {
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone_number { get; set; } = "";
        public AddFarmerCommand(string name, string address, string phone_number)
        {
            Name = name;
            Address = address;
            Phone_number = phone_number;
        }

    }
}
