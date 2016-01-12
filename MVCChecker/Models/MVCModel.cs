using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace MVCChecker.Models
{
    internal class MVCModel : INotifyPropertyChanged
    {

        const string ModelName = "Model";
        const string ControllerName = "Controller";
        const string ViewName = "View";

        internal class MVCInfo
        {
            // タイトル
            public string DirectoryPath { get; set; }
            public string FolderPath { get; set; }

            public string FileText { get; set; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<MVCDirectoryModel> MVCDirectory = new ObservableCollection<MVCDirectoryModel>();
        public ObservableCollection<MVCFileModel> MVCFiles = new ObservableCollection<MVCFileModel>();
        internal MVCInfo InfoData { get; private set; } = new MVCInfo();



        private void AddDirectory(string dir, MVCDirectoryModel mvc)
        {
            var files = Directory.EnumerateDirectories(dir);
            foreach (var file in files)
            {
                if (!file.Contains(ModelName) && !file.Contains(ControllerName) && !file.Contains(ViewName)) continue;

                var child = new MVCDirectoryModel();
                child.DirctoryName = Path.GetFileName(file);
                child.FullName = file;
                mvc.Children.Add(child);

                AddDirectory(file, child);
            }
        }
        public void SetDirectory()
        {
            if (Directory.Exists(InfoData.DirectoryPath))
            {
                MVCDirectory.Clear();

                var mvc = new MVCDirectoryModel();
                mvc.DirctoryName = Path.GetFileName(InfoData.DirectoryPath);
                mvc.FullName = (InfoData.DirectoryPath);

                AddDirectory(InfoData.DirectoryPath, mvc);
                MVCDirectory.Add(mvc);
            }
        }

        internal void SetFileText(MVCFileModel file)
        {
            if (!string.IsNullOrEmpty(file?.FullName))
            {
                InfoData.FileText = File.ReadAllText(file.FullName);

            }
        }

        public void SetDirectoryFile()
        {
            if (!Directory.Exists(InfoData.FolderPath)) return;

            MVCFiles.Clear();
            foreach (var file in Directory.GetFiles(InfoData.FolderPath))
            {
                var mvcFile = new MVCFileModel()
                {
                    FileName = Path.GetFileName(file),
                    FullName = file
                };

                //Model
                if (file.Contains(ModelName))
                {
                    if (File.ReadAllText(file).Contains(ViewName))
                    {
                        mvcFile.Error = true;
                    }
                }
                //View
                if (file.Contains(ViewName))
                {
                    if (File.ReadAllText(file).Contains(ModelName))
                    {
                        mvcFile.Error = true;
                    }
                }

                MVCFiles.Add(mvcFile);
            }
        }

    }
}
