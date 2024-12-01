using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public enum Operator
{
    Add, Mul, Sub, Div
}

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult About()
    {
        return View();
    }
    
    public IActionResult Calculator(Operator? op, double? a, double? b)
    {
        if (a == null || b == null)
        {
            ViewBag.Error = "Parametry a lub b nie może być równe null";
            return View();
        }

        switch (op)
        {
            case Operator.Add:
                ViewBag.Result = a + b;
                break;
            
            case Operator.Mul:
                ViewBag.Result = a * b;
                break;
            
            case Operator.Sub:
                ViewBag.Result = a - b;
                break;
            
            case Operator.Div:
                if (b == 0)
                {
                    ViewBag.Error = "Nie można dzielić przez zero";
                    return View();
                }

                ViewBag.Result = a / b;
                break;
            
            default:
                ViewBag.Error = "Parameter op nie może być równy null";
                return View();
        }
        
        ViewBag.Op = op;
        ViewBag.A = a;
        ViewBag.B = b;
        ViewBag.Error = "";
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}