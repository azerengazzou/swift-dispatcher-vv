using Microsoft.AspNetCore.Mvc;
using my_swift.Models;
using my_swift.Services;

namespace my_swift.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISwiftMessageService _swiftService;

        public HomeController(ISwiftMessageService swiftService)
        {
            _swiftService = swiftService;
        }

        public IActionResult Index()
        {
            var categories = _swiftService.GetAllCategories();
            return View(categories);
        }

        [HttpPost]
        public IActionResult ParseTxtFile([FromBody] FileUploadDto upload)
        {
            if (upload == null || string.IsNullOrWhiteSpace(upload.Content))
                return BadRequest("File is empty");

            // Service parses file content into multiple fields
            var fields = _swiftService.Parse(upload.FileName, upload.Content);

            return Ok(fields); // 👈 this is a List<ParsedField>
        }


        [HttpPost]
        public IActionResult UploadTxtFile([FromBody] FileUploadDto upload)
        {
            if (upload == null || string.IsNullOrEmpty(upload.Content))
            {
                return BadRequest("File is empty");
            }

            // do something with upload.Content / upload.FileName / upload.Size …
            return Ok(new { message = $"Received '{upload.FileName}' ({upload.Size} bytes)" });
        }

    }
}
