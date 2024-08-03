namespace Neptune.Client;

public class ResourceManager {
    public static string GetPhysicalPath(string path)
    {
        string[] s = path.Split(':');

        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", s[0], s[1]);
    }
}