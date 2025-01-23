using Laboratorium_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorium_2.Controllers
{
    public class BirthController : Controller
    {
        [HttpPost]
        public IActionResult Result([FromForm] Age model )
        {
         if (!model.IsValid()) return View("Error");
         
         model.Birth();
         
         return View(model);
        }

        public IActionResult Form()
        {
            return View();
        }
    }
}
