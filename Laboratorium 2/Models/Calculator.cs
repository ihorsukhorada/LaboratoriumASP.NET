namespace Laboratorium_2.Models;

public class Calculator
{
    public Operator? Operators { get; set; }
    public double? X { get; set; }
    public double? Y { get; set; }

    public string Op
    {
        get
        {
            return Operators switch
            {
                Operator.Add => "+",
                Operator.Div => ":",
                Operator.Mul => "*",
                Operator.Sub => "-",
                Operator.Unknown => "",
                _ => ""
            };
        }
    }

    public bool IsValid()
    {
        return Operators != null && X != null && Y != null;
    }

    public double Calculate()
    {
        return Operators switch
        {
            Operator.Add => (double)(X + Y)!,
            Operator.Div => (double)(X / Y)!,
            Operator.Mul => (double)(X * Y)!,
            Operator.Sub => (double)(X - Y)!,
            _ => double.NaN
        };
    }
}