using System;
using System.IO;

namespace QuickHTML
{
    class QuickHTMLService
    {
        /// <summary>
        /// Location of the project
        /// </summary>
        public string ProjectLocation { get; }
        /// <summary>
        /// Name of the project/file
        /// </summary>
        public string ProjectDirectoryName { get; }
        /// <summary>
        /// Inside the project directory
        /// </summary>
        public string ProjectDirectoryRoot { get; private set; }

        public QuickHTMLService()
        {
            ProjectLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ProjectDirectoryName = "QuickHTML";
        }

        public QuickHTMLService(string projectLocation, string projectName)
        {
            ProjectLocation = projectLocation;
            ProjectDirectoryName = projectName;
        }

        public QuickHTMLService CreateProject()
        {
            ProjectDirectoryRoot = Path.Combine(ProjectLocation, ProjectDirectoryName);

            //Only delete the dir if you're making a QuickHTML on the desktop
            if (Directory.Exists(ProjectDirectoryRoot) && ProjectDirectoryName == "QuickHTML" &&
                ProjectLocation == Environment.GetFolderPath(Environment.SpecialFolder.Desktop))
                new DirectoryInfo(ProjectDirectoryRoot).Delete(true);

            var targetDirectory = new DirectoryInfo(ProjectDirectoryRoot);
            targetDirectory.Create();

            CreateDirectories(@"assets", @"assets\css", @"assets\scripts", @"assets\images");
            AddTemplateFiles();

            return this;
        }

        void AddTemplateFiles()
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates");
            var templateFiles = new DirectoryInfo(templatePath).EnumerateFiles();

            foreach (var tFile in templateFiles)
            {
                string folderPath = string.Empty;

                switch (tFile.Extension)
                {
                    case ".js": folderPath = @"assets\scripts"; break;
                    case ".css": folderPath = @"assets\css"; break;
                }

                var filePath = Path.Combine(ProjectDirectoryRoot, folderPath, tFile.Name);
                File.Copy(tFile.FullName, filePath);
            }
        }

        void CreateDirectories(params string[] directoryPaths)
        {
            foreach (var dir in directoryPaths)
            {
                var dirPath = Path.Combine(ProjectDirectoryRoot, dir);
                Directory.CreateDirectory(dirPath);
            }
        }

        void CreateFiles(params string[] filePaths)
        {
            foreach (var file in filePaths)
            {
                var filePath = Path.Combine(ProjectDirectoryRoot, file);
                File.Create(filePath);
            }
        }
    }
}

