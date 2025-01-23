using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Laboratorium_1.Models;

namespace Laboratorium_1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

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
    
    public IActionResult Calculator(Operator op, double? a, double? b)
    {
        if (a == null || b == null)
        {
            ViewBag.Error = "Wymagane są oba parametry 'a' i 'b'!";
            return View();
        }
        
        double result;
        string sign;

        switch (op)
        {
            case Operator.Add:
                result = a.Value + b.Value;
                sign = "+";
                break;
            case Operator.Sub:
                result = a.Value - b.Value;
                sign = "-";
                break;
            case Operator.Mul:
                result = a.Value * b.Value;
                sign = "*";
                break;
            case Operator.Div:
                if (b.Value != 0)
                {
                    result = a.Value / b.Value;
                    sign = "/";
                }
                else
                {
                    ViewBag.Error = "Dzielenie przez zero jest niedozwolone!";
                    return View();
                }
                break;
            case Operator.Unknown:
            default:
                ViewBag.Error = "Nieprawidłowa operacja!";
                return View();
        }

        ViewBag.A = a.Value;
        ViewBag.B = b.Value;
        ViewBag.Sign = sign;
        ViewBag.Result = result;

        return View();
    }
    
    public enum Operator
    {
        Unknown, Add, Mul, Sub, Div
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}