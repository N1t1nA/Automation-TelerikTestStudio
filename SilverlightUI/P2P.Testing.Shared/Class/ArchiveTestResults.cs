using System;
using System.Linq;

namespace P2P.Testing.Shared.Class
{
    class ArchiveTestResults
    {
        public void ArchiveP2PTestResults(string testResultsArchivePath, string testResultFileName, string testResultSourcePath)
        {
            // Specify a "currently active folder"
            string activeDir = @testResultsArchivePath;

            string filename = String.Format("TestResults_Archive_{0:ddMMyyyyhhmmss}", DateTime.Now);

            //Create a new subfolder under the current active folder
            string newPath = System.IO.Path.Combine(activeDir, filename);

            // Create the subfolder
            System.IO.Directory.CreateDirectory(newPath);

            //copy SQL test results to archive folder
            string fileName = testResultFileName;
            string sourcePath = @testResultSourcePath;

            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(newPath, fileName);

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(sourceFile, destFile, true);

        }
    
    
    }
}
