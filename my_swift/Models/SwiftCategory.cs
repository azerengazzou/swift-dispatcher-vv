namespace my_swift.Models
{
    public class SwiftCategory
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public List<SwiftMessage> Messages { get; set; } = new();
    }
}
