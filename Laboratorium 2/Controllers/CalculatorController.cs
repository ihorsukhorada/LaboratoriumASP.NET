using Laboratorium_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorium_2.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Form()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Result( [FromForm] Calculator model)
        {
            return !model.IsValid() ? View("Error") : View(model);
        }
    }
}
