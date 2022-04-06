using KeyValue3Updater;
using KeyValue3Updater.Updaters;

Updater[] updaters = {
    new RandomColorUpdater(),
    new RandomLifeTimeUpdater(),
    new RandomRadiusUpdater(),
    new RandomSequenceUpdater(),
    new RandomAlphaUpdater()
};

Console.WriteLine("Enter folder to update files in:");

string targetFolder = Console.ReadLine();

if (string.IsNullOrEmpty(targetFolder))
{
    targetFolder = Directory.GetCurrentDirectory();
}
else if (!Directory.Exists(targetFolder))
{
    targetFolder = Path.Combine(Directory.GetCurrentDirectory(), targetFolder);
}

if(Directory.Exists(targetFolder))
{
    string outputFolder = Path.Combine(targetFolder, "out");
    if(!Directory.Exists(outputFolder))
    {
        Directory.CreateDirectory(outputFolder);
    }
    Log.WriteLine($"Will update files in directory '{targetFolder}'");

    foreach (var file in Directory.EnumerateFiles(targetFolder))
    {
        if (Path.GetExtension(file) != ".vpcf") continue;

        Log.WriteLine($"\nWill update '{file}'");

        string text = File.ReadAllText(file);
        text = text.Replace("\t", "").Replace("\r", "");

        foreach (Updater updater in updaters)
        {
            text = updater.Process(ref text);
        }

        string filename = Path.GetFileName(file);
        File.WriteAllText(Path.Combine(outputFolder, filename), text);
        Log.WriteLine($"Updated '${filename}'");
    }
}
else
{
    Log.WriteLine("Directory doesnt exist, exiting...");
}

//Write log to program dir
Log.WriteToFile(Directory.GetCurrentDirectory());

Console.WriteLine("Done!");
Console.ReadKey();