namespace task11tests;

using task11;
using Xunit;
public class CalculatorGenTest
{
    [Fact]
    public void ClassGenerator_CalculatorAdd_ReturnsCorrectValue()
    {
        ICalculator calculator = ClassGenerator.CalculatorGenerator();
        var result = calculator.Add(10, 15);
        Assert.Equal(25, result);
    }
    [Fact]
    public void ClassGenerator_CalculatorMinus_ReturnsCorrectValue()
    {
        ICalculator calculator = ClassGenerator.CalculatorGenerator();
        var result = calculator.Minus(30, 15);
        Assert.Equal(15, result);
    }
    [Fact]
    public void ClassGenerator_CalculatorMul_ReturnsCorrectValue()
    {
        ICalculator calculator = ClassGenerator.CalculatorGenerator();
        var result = calculator.Mul(5, 6);
        Assert.Equal(30, result);
    }
    [Fact]
    public void ClassGenerator_CalculatorDiv_ReturnsCorrectValue()
    {
        ICalculator calculator = ClassGenerator.CalculatorGenerator();
        var result = calculator.Div(25, 5);
        Assert.Equal(5, result);
    }
    [Fact]
    public void DivByZero_ReturnDivideByZeroException()
    {
        ICalculator calculator = ClassGenerator.CalculatorGenerator();
        Assert.Throws<DivideByZeroException>(() => calculator.Div(1,0));
    }
}