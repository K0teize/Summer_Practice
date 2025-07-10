namespace task06tests;

using System.Reflection;
using Xunit;
using task06;

[DisplayName("Пример класса")]
[Version(1, 0)]
public class SampleClass
{
    [DisplayName("Тестовый метод")]
    public virtual void TestMethod(){}
    [DisplayName("Числовое свойство")]
    public int Number { get; }
}
public class AttributeReflectionTests
{
    [Fact]
    public void Class_HasDisplayNameAttribute()
    {
        var type = typeof(SampleClass);
        var attribute = type.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Пример класса", attribute.DisplayName);
    }

    [Fact]
    public void Method_HasDisplayNameAttribute()
    {
        var method = typeof(SampleClass).GetMethod("TestMethod");
        var attribute = method?.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Тестовый метод", attribute.DisplayName);
    }

    [Fact]
    public void Property_HasDisplayNameAttribute()
    {
        var prop = typeof(SampleClass).GetProperty("Number");
        var attribute = prop?.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Числовое свойство", attribute.DisplayName);
    }

    [Fact]
    public void Class_HasVersionAttribute()
    {
        var type = typeof(SampleClass);
        var attribute = type.GetCustomAttribute<VersionAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal(1, attribute.Major);
        Assert.Equal(0, attribute.Minor);
    }
    [Fact]
    public void ReflectionHelper_ReturnCorrectInfo()
    {
        var type = typeof(SampleClass);
        var output = new StringWriter();
        Console.SetOut(output);
        ReflectionHelper.PrintTypeInfo(type);
        Assert.Contains("Пример класса", output.ToString());
        Assert.Contains("1.0", output.ToString());
        Assert.Contains("TestMethod:Тестовый метод", output.ToString());
        Assert.Contains("Number:Числовое свойство", output.ToString());
    }
}
