namespace my_swift.Models
{
    public class FileUploadDto
    {
        public string FileName { get; set; } = "";
        public long Size { get; set; }
        public string Content { get; set; } = "";
    }
}
