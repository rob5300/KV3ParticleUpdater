using KeyValue3Updater;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

var currentAssembly = Assembly.GetCallingAssembly();
var updaterType = typeof(Updater);
var UpdaterTypes = currentAssembly.GetTypes().Where(x => !x.IsAbstract && x.IsSubclassOf(updaterType)).ToArray();

Console.WriteLine("KeyValue3Updater by rob5300. (github.com/rob5300/KeyValue3Updater)");
Console.WriteLine("-= Input folder to update files in: (Press enter to begin or leave blank to process the current folder)");

string targetFolder = Console.ReadLine();
string outputFolder;

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
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    outputFolder = Path.Combine(targetFolder, "out");
    if(!Directory.Exists(outputFolder))
    {
        Directory.CreateDirectory(outputFolder);
    }
    Log.WriteLine($"-= Will update files in directory '{targetFolder}'");

    //Get all files in the target directory that we want to update
    var files = new List<string>(Directory.EnumerateFiles(targetFolder, "*.vpcf", SearchOption.AllDirectories));
    //Remove file paths that are in an existing "out" dir.
    files.RemoveAll((x) => Path.GetDirectoryName(x).Contains(outputFolder));

    Log.WriteLine($"-= Will process '{files.Count}' files.");

    //Split into chunks to process concurrently
    List<Task> tasks = new();
    var chunks = files.Chunk(300);
    int chunkNum = 1;
    foreach (string[] fileList in chunks)
    {
        string[] _fileList = fileList;
        int num = chunkNum++;
        tasks.Add(
            Task.Run(() =>
            {
                Log.WriteLine($"-= Started processing file thread ({num}/{chunks.Count()})");
                UpdateFiles(_fileList);
                Log.WriteLine($"-= Finished processing file thread ({num}/{chunks.Count()})");
            })
        );
    }

    Task.WaitAll(tasks.ToArray(), 10 * 60 * 60 * 1000);

    //Write log to program dir
    Log.WriteToFile(Directory.GetCurrentDirectory());

    stopwatch.Stop();
    Console.WriteLine($"Done! Took {stopwatch.Elapsed}.");
}
else
{
    Log.WriteLine("-= Directory doesnt exist, exiting...");
}
Console.ReadKey();


void UpdateFiles(string[] fileList)
{
    //Create a new set of updaters and log string builder.
    List<Updater> updaters = new(UpdaterTypes.Length);
    StringBuilder logBuilder = new();

    foreach (Type type in UpdaterTypes)
    {
        Updater newUpdater = (Updater)Activator.CreateInstance(type);
        newUpdater.SetLogBuilder(logBuilder);
        updaters.Add(newUpdater);
    }

    foreach (string _file in fileList)
    {
        ProcessFile(_file, updaters, logBuilder);
        Log.WriteLine(logBuilder.ToString());
        logBuilder.Clear();
    }
}

void ProcessFile(string file, IEnumerable<Updater> updaters, StringBuilder logBuilder)
{
    logBuilder.AppendLine($"\n-= Processing File: '{file}'");
    string text = File.ReadAllText(file);
    text = text.Replace("\t", "").Replace("\r", "");
    //Remove all space blocks that are more than 1 in length (hopefully just visual formatting)
    text = Regex.Replace(text, @" {2,}", "");

    foreach (Updater updater in updaters)
    {
        string changed = updater.Process(ref text);
        text = changed;
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
    logBuilder.AppendLine($"-= Updated '${filename}'");
}