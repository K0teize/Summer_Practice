namespace task09tests;

using Xunit;
using task09;

public class UnitTest1
{
    [Fact]
    public void MetadataAnalyze_PrintCorrectClass()
    {
        var dir = AppContext.BaseDirectory;
        var path = Path.Combine(dir, "task06.dll");
        var output = new StringWriter();
        Console.SetOut(output);
        MetadataAnalyze.PrintInfo(path);
        Assert.Contains("task06.SampleClass", output.ToString());
    }

    [Fact]
    public void MetadataAnalyze_PrintCorrectMethodsAndParameters()
    {
        var dir = AppContext.BaseDirectory;
        var path = Path.Combine(dir, "task06.dll");
        var output = new StringWriter();
        Console.SetOut(output);
        MetadataAnalyze.PrintInfo(path);
        Assert.Contains("TestMethod", output.ToString());
        Assert.Contains("get_Number", output.ToString());
        Assert.Contains("value, Int32", output.ToString());
    }

    [Fact]
    public void MetadataAnalyze_PrintCorrectConstructorsAndParameters()
    {
        var dir = AppContext.BaseDirectory;
        var path = Path.Combine(dir, "task06.dll");
        var output = new StringWriter();
        Console.SetOut(output);
        MetadataAnalyze.PrintInfo(path);
        Assert.Contains(".ctor", output.ToString());
    }
    [Fact]
    public void MetadataAnalyze_PrintCorrectAttributes()
    {
        var dir = AppContext.BaseDirectory;
        var path = Path.Combine(dir, "task06.dll");
        var output = new StringWriter();
        Console.SetOut(output);
        MetadataAnalyze.PrintInfo(path);
        Assert.Contains("DisplayNameAttribute", output.ToString());
        Assert.Contains("VersionAttribute", output.ToString());
    }
}
