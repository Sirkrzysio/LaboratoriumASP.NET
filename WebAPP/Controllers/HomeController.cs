using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

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

    public IActionResult Calculator(Operator? op, double? x, double? y = null)
    {
        // var op = Request.Query["op"];
        // var x = double.Parse(Request.Query["x"]!);
        // var y = double.Parse(Request.Query["y"]!);
        if (x is null || y is null && op != Operator.SIN)
        {
            ViewBag.ErrorMessage = "Niepoprawny format liczby w parametrze x lub y";
            return View("CalculatorError");
        }

        if (op == null)
        {
            ViewBag.ErrorMessage = "Niepoprawny operator";
            return View("CalculatorError");
        }
        //
        // if (op == Operator.SIN)
        // {
        //     y = null;
        // }

        switch (op)
        {
            case Operator.ADD:
            {
                ViewBag.Result = x + y ?? 0;
                break;
            }
            case Operator.SUB:
            {
                ViewBag.Result = x - y ?? 0;
                break;
            }
            case Operator.MUL:
            {
                ViewBag.Result = x * y ?? 0;
                break;
            }
            case Operator.DIV:
            {
                ViewBag.Result = x / y ?? 0;
                break;
            }
            case Operator.POW:
            {
                ViewBag.Result = Math.Pow((double)x, (double)y!);
                break;
            }
            case Operator.SIN:
            {
                ViewBag.Result = Math.Sin((double)x);
                break;
            }
            // default:
            // {
            //     ViewBag.ErrorMessage = "Nieznany operator";
            //     return View("CalculatorError");
            // }
        }

        return View();
    }

    public IActionResult CalculatorError()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public enum Operator
{
    ADD,
    SUB,
    MUL,
    DIV,
    POW,
    SIN
}