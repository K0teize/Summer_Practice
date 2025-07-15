namespace task09;

using System.Reflection;
using task06;
public class MetadataAnalyze
{
    public static void PrintInfo(string path)
    {
        Assembly assembly = Assembly.LoadFrom(path);
        Type[] types = assembly.GetTypes();
        foreach (Type type in types)
        {
            Console.WriteLine(type.FullName);
            foreach (var method in type.GetMethods())
            {
                Console.WriteLine(method.Name);
                foreach (var param in method.GetParameters())
                {
                    Console.WriteLine($"{param.Name}, {param.ParameterType.Name}");
                }
            }
            foreach (var attr in type.GetCustomAttributes())
            {
                Console.WriteLine(attr.GetType().Name);
            }
            foreach (var constr in type.GetConstructors())
            {
                Console.WriteLine(constr.Name);
                foreach (var param in constr.GetParameters())
                {
                    Console.WriteLine($"{param.Name}, {param.ParameterType.Name}");
                }
            }
        }
    }
}
