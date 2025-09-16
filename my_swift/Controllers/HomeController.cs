using Microsoft.AspNetCore.Mvc;
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
        public IActionResult UploadTxtFile(string fileContent)
        {
            if (string.IsNullOrEmpty(fileContent))
            {
                TempData["Message"] = "File content is empty!";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "File content received successfully!";
            return RedirectToAction("Index");
        }

    }
}
