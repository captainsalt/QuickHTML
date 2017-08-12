using System;
using System.IO;

namespace QuickHTML
{
    class QuickHTMLService
    {
        string _projectDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string _projectDirName = "QuickHTML";
        string _projectDirRoot;

        public QuickHTMLService() { }

        public QuickHTMLService(string projectDirectory, string projectName)
        {
            _projectDirectory = projectDirectory;
            _projectDirName = projectName;
        }

        public void CreateProject()
        {
            _projectDirRoot = Path.Combine(_projectDirectory, _projectDirName);

            //Only delete the dir if you're making a QuickHTML om the desktop
            if (Directory.Exists(_projectDirRoot) && _projectDirName == "QuickHTML" &&
                _projectDirectory == Environment.GetFolderPath(Environment.SpecialFolder.Desktop))
                new DirectoryInfo(_projectDirRoot).Delete(true);

            var targetDirectory = new DirectoryInfo(_projectDirRoot);
            targetDirectory.Create();

            CreateDirectories(@"assets", @"assets\css", @"assets\scripts", @"assets\images");
            AddTemplateFiles();
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

                var filePath = Path.Combine(_projectDirRoot, folderPath, tFile.Name);
                File.Copy(tFile.FullName, filePath);
            }
        }

        void CreateDirectories(params string[] directoryPaths)
        {
            foreach (var dir in directoryPaths)
            {
                var dirPath = Path.Combine(_projectDirRoot, dir);
                Directory.CreateDirectory(dirPath);
            }
        }

        void CreateFiles(params string[] filePaths)
        {
            foreach (var file in filePaths)
            {
                var filePath = Path.Combine(_projectDirRoot, file);
                File.Create(filePath);
            }
        }
    }
}

