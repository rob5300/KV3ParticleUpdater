
public class Log
{
    private static List<string> logText = new List<string>();

    public static void WriteLine(string text)
    {
        logText.Add(text);
        Console.WriteLine(text);
    }

    public static void WriteToFile(string dir)
    {
        string path = Path.Combine(dir, "log.txt");
        File.WriteAllLines(path, logText);
    }
}

