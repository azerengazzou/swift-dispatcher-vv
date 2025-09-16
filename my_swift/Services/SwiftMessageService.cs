using my_swift.Models;

namespace my_swift.Services
{
    public class SwiftMessageService : ISwiftMessageService
    {
        public List<SwiftCategory> GetAllCategories()
        {
            //TODO : READ FROM DB OR JSON
            return new List<SwiftCategory>
        {
            new SwiftCategory
            {
                Id = "system",
                Title = "0 - System Messages",
                Messages = new List<SwiftMessage>
                {
                    new() { Code = "MT 001 – 099", Description = "SWIFT system and network management messages." }
                }
            },
            new SwiftCategory
            {
                Id = "payments",
                Title = "1 - Customer Payments and Cheques",
                Messages = new List<SwiftMessage>
                {
                    new() { Code = "MT 101", Description = "Request for Transfer." },
                    new() { Code = "MT 102", Description = "Multiple Customer Credit Transfer." }
                }
            }
        };
        }
    }
}