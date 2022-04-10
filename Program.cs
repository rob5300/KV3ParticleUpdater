using KeyValue3Updater;
using KeyValue3Updater.Updaters;
using System.Text.RegularExpressions;

Updater[] updaters = {
    new RandomColorUpdater(),
    new RandomLifeTimeUpdater(),
    new RandomRadiusUpdater(),
    new RandomSequenceUpdater(),
    new RandomAlphaUpdater(),
    new RandomRotationUpdater(),
    new RandomTrailLengthUpdater(),
    new RandomYawFlipUpdater(),
    new RandomRotationSpeedUpdater(),
    new RemapCPtoVectorUpdater(),
    new RandomYawUpdater()
};

Console.WriteLine("KeyValue3Updater by rob5300. (github.com/rob5300/KeyValue3Updater)");
Console.WriteLine("-= Input folder to update files in: (Press enter to begin or leave blank to process the current folder)");

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
    Log.WriteLine($"-= Will update files in directory '{targetFolder}'");

    foreach (var file in Directory.EnumerateFiles(targetFolder, "*.vpcf", SearchOption.AllDirectories))
    {
        //Skip output folder
        if (Path.GetDirectoryName(file).Contains(outputFolder)) continue;

        Log.WriteLine($"\n-= Will update '{file}'");

        string text = File.ReadAllText(file);
        text = text.Replace("\t", "").Replace("\r", "");
        //Remove all space blocks that are more than 1 in length (hopefully just visual formatting)
        text = Regex.Replace(text, @" {2,}", "");

        foreach (Updater updater in updaters)
        {
            text = updater.Process(ref text);
        }

        string filename = Path.GetFileName(file);
        string fileDir = Path.GetDirectoryName(file);
        string relativeDir = Path.GetRelativePath(targetFolder, fileDir);
        string newPath = Path.Combine(outputFolder, relativeDir, filename);

        string newPathDir = Path.GetDirectoryName(newPath);
        if (!Directory.Exists(newPathDir))
        {
            Directory.CreateDirectory(newPathDir);
        }

        File.WriteAllText(newPath, text);
        Log.WriteLine($"-= Updated '${filename}'");
    }
}
else
{
    Log.WriteLine("-= Directory doesnt exist, exiting...");
}

//Write log to program dir
Log.WriteToFile(Directory.GetCurrentDirectory());

Console.WriteLine("Done!");
Console.ReadKey();