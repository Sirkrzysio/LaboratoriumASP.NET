using Microsoft.AspNetCore.Mvc;
using WebAPP.Models;

namespace WebAPP.Controllers;

public class CalculatorController : Controller
{

    private readonly ILogger<HomeController> _logger;

    public CalculatorController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Form()
    {
        return View();
    }

    public IActionResult Calculator(Operator? op, double? x, double? y = null)
    {
        // var op =Request.Query["op"];
        // var x =double.Parse(Request.Query["x"]);
        // var y =double.Parse(Request.Query["y"]);
        if (x is null || y is null && op != Operator.Sin)
        {
            ViewBag.ErrorMessage = "Niepoprawny format parametru x lub y";
            return View("CalculatorError");
        }

        if (op is null)
        {
            ViewBag.ErrorMessage = "niezly wymysliles operator szacun";
            return View("CalculatorError");
        }
        
        switch (op)
        {
            case Operator.Add:
                ViewBag.Result = x + y;
                break;
            case Operator.Sub:
                ViewBag.Result = x - y;
                break;
            case Operator.Mul:
                ViewBag.Result = x * y;
                break;
            case Operator.Div:
                ViewBag.Result = x / y;
                break;
            case Operator.Pow:
                ViewBag.Result = Math.Pow((double)x,(double)y);
                break;
            case Operator.Sin:
                ViewBag.Result = Math.Sin((double)x);
                break;
        }
        return View();
    }
    public enum Operator
    {
        Add, Sub, Mul, Div, Sin, Pow
    }
    public IActionResult Result(Calculator model)
    {
        if (!model.IsValid())
        {
            return View("Error");
        }
        return View(model);
    }
}