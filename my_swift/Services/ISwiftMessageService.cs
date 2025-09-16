using my_swift.Models;

namespace my_swift.Services
{
    public interface ISwiftMessageService
    {
        List<SwiftCategory> GetAllCategories();
    }
}
