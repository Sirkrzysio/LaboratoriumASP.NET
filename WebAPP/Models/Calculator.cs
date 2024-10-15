using WebAPP.Controllers;

namespace WebAPP.Models;

public class Calculator
{
    public CalculatorController.Operator? Operator { get; set; }
    public double? X { get; set; }
    public double? Y { get; set; }

    public String Op
    {
        get
        {
            switch (Operator)
            {
                case CalculatorController.Operator.Add:
                    return "+";
                case CalculatorController.Operator.Sub:
                    return "-";
                case CalculatorController.Operator.Mul:
                    return "*";
                case CalculatorController.Operator.Div:
                    return "/";
                case CalculatorController.Operator.Pow:
                    return "^";
                case CalculatorController.Operator.Sin:
                    return "sin";
                default:
                    return "";
            }
        }
    }

    public bool IsValid()
    {
        return Operator != null && X != null && (Y != null || Operator == CalculatorController.Operator.Sin);
    }

    public double Calculate() {
        switch (Operator)
        {
            case CalculatorController.Operator.Add:
                return (double) (X + Y);
            case CalculatorController.Operator.Sub:
                return (double) (X - Y);
            case CalculatorController.Operator.Mul:
                return (double) (X * Y);
            case CalculatorController.Operator.Div:
                return (double) (X / Y);
            case CalculatorController.Operator.Pow:
                return  Math.Pow((double)X,(double)Y);
            case CalculatorController.Operator.Sin:
                return  Math.Sin((double)X);
            
                
            default: return double.NaN;
        }
    }
}