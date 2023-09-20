public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter filename: ");
        string fileName = Console.ReadLine()!;

        DriveInfo[] drives = DriveInfo.GetDrives();
        List<string> foundFilePaths = new List<string>();

        foreach (DriveInfo drive in drives)
        {
            Console.WriteLine($"Searching in drive {drive.Name}...");
            SearchFileInDrive(drive.RootDirectory, fileName, foundFilePaths);
        }

        Console.WriteLine("Search complete.");

        if (foundFilePaths.Count > 0)
        {
            Console.WriteLine("Found files:");
            foreach (string filePath in foundFilePaths)
            {
                Console.WriteLine(filePath);
            }

            Console.WriteLine($"Count found files: {foundFilePaths.Count}");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    public static void SearchFileInDrive(DirectoryInfo directory, string fileName, List<string> foundFilePaths)
    {
        try
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                if (file.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                {
                    foundFilePaths.Add(file.FullName);
                }
            }

            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                SearchFileInDrive(subDirectory, fileName, foundFilePaths);
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine(directory.FullName);
        }
    }
}
