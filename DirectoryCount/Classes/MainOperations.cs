namespace DirectoryCount.Classes;
internal class MainOperations
{
    /// <summary>
    /// Executes the main operations for counting directories and files within the specified folder.
    /// </summary>
    /// <param name="folderName">The name of the folder to count directories and files in.</param>
    /// <remarks>
    /// If the folder does not exist, a message is printed to the console. If an exception occurs during the counting process,
    /// it is handled and displayed using a colorful exception message.
    /// </remarks>
    public static void Run(string folderName)
    {
        if (DirectoryHelpers.FolderExists(folderName))
        {

            try
            {
                var (directoryCount, fileCount) = DirectoryHelpers.DirectoryFileCount(folderName, SearchOption.AllDirectories);

                Console.WriteLine($"Dir count {directoryCount:N0}");
                Console.WriteLine($"File count {fileCount:N0}");
            }
            catch (Exception exception)
            {
                ExceptionHelpers.ColorStandard(exception);
            }
        }
        else
        {
            Console.WriteLine($"Folder {folderName} does not exists");
        }
    }
}
