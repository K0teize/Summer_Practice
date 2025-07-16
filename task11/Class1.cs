namespace task11;

using System;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public interface ICalculator
    {
        int Add(int a, int b);
        int Minus(int a, int b);
        int Mul(int a, int b);
        int Div(int a, int b);
    }
public class ClassGenerator
{
    public static ICalculator CalculatorGenerator()
    {
        string code = @"
        public class Calculator : task11.ICalculator
        {
            public int Add(int a, int b) => a + b;
            public int Minus(int a, int b) => a - b;
            public int Mul(int a, int b) => a * b;
            public int Div(int a, int b) => a / b;
        }";

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        
        var references = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location)
        };

        var compilation = CSharpCompilation.Create("TempAssembly")
            .AddReferences(references)
            .AddSyntaxTrees(syntaxTree)
            .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using var ms = new MemoryStream();
        compilation.Emit(ms);
    
        var assembly = Assembly.Load(ms.ToArray());
        Type? calculatorType = assembly.GetType("Calculator");
        ICalculator? type = Activator.CreateInstance(calculatorType!) as ICalculator;
        return type!;
    }
}

