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
    
    public IActionResult Calculator(Operator op, double a, double b)
    {
        ViewBag.Op = op;
        ViewBag.A = a;
        ViewBag.B = b;
        ViewBag.Result = op switch
        {
            Operator.Add => a + b,
            Operator.Div => a / b,
            Operator.Mul => a * b,
            Operator.Sub => a - b,
            _ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
        };
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}