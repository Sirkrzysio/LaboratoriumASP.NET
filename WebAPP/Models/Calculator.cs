namespace WebApp.Models;

public class Calculator
{
    public Operator? Operator { get; set; }
    public double? X { get; set; }
    public double? Y { get; set; }

    public string Op
    {
        get
        {
            return Operator switch
            {
                Models.Operator.ADD => "+",
                Models.Operator.SUB => "−",
                Models.Operator.MUL => "×",
                Models.Operator.DIV => "÷",
                Models.Operator.POW => "^",
                Models.Operator.SIN => "sin",
                _ => ""
            };
        }
    }

    public bool IsValid()
    {
        return Operator != null &&
               X != null &&
               (Y != null || Operator == Models.Operator.SIN);
    }

    public double Calculate()
    {
        return Operator switch
        {
            Models.Operator.ADD => (double)(X + Y)!,
            Models.Operator.SUB => (double)(X - Y)!,
            Models.Operator.MUL => (double)(X * Y)!,
            Models.Operator.DIV => (double)(X / Y)!,
            Models.Operator.POW => Math.Pow((double)X!, (double)Y!),
            Models.Operator.SIN => Math.Sin((double)X!),
            _ => double.NaN
        };
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