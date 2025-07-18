namespace task10;

using System.Reflection;
public interface IPlugin
{
    void Execute();
}

[AttributeUsage(AttributeTargets.Class)]
public class PluginLoadAttribute : Attribute
{
    public string[] Dependencies { get; }
    public PluginLoadAttribute(params string[] dependencies) => Dependencies = dependencies;
}

public class PluginLoader
{
    public static void LoadPlugins(string path)
    {
        var plugins = new List<(Type Type, string Name, string[] Dependencies)>();

        foreach (var dll in Directory.GetFiles(path, "*.dll"))
        {
            var assembly = Assembly.LoadFrom(dll);
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<PluginLoadAttribute>() is PluginLoadAttribute attr
                    && typeof(IPlugin).IsAssignableFrom(type))
                {
                    plugins.Add((type, type.Name, attr.Dependencies));
                }
            }
        }

        var loaded = new HashSet<string>();
        var unloaded = new List<(Type, string, string[])>(plugins);

        while (unloaded.Count > 0)
        {
            var ready = unloaded.Where(p => p.Item3.All(d => loaded.Contains(d))).ToList();

            if (ready.Count == 0)
            {
                Console.WriteLine("Невозможно загрузить плагины.");
                return;
            }

            foreach (var plugin in ready)
            {
                ((IPlugin)Activator.CreateInstance(plugin.Item1)).Execute();
                loaded.Add(plugin.Item2);
                unloaded.Remove(plugin);
            }
        }
    }
}
