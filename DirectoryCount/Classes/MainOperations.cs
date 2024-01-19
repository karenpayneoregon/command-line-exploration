namespace DirectoryCount.Classes;
internal class MainOperations
{
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
