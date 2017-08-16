using System;
using System.IO;

namespace QuickHTML
{
    class QuickHTMLService
    {
        /// <summary>
        /// Location of the project
        /// </summary>
        public string ProjectLocation { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        /// <summary>
        /// Name of the project/file
        /// </summary>
        public string ProjectDirectoryName { get; set; } = "QuickHTML";
        /// <summary>
        /// Inside the project directory
        /// </summary>
        public string ProjectDirectoryRoot { get; set; }

        public QuickHTMLService() { }

        public QuickHTMLService(string projectLocation, string projectName)
        {
            ProjectLocation = projectLocation;
            ProjectLocation = projectName;
        }

        public QuickHTMLService CreateProject()
        {
            ProjectDirectoryRoot = Path.Combine(ProjectLocation, ProjectDirectoryName);

            //Only delete the dir if you're making a QuickHTML om the desktop
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

