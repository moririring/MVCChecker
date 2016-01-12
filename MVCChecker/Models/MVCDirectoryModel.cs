using System.Collections.ObjectModel;

namespace MVCChecker.Models
{
    internal class MVCDirectoryModel
    {
        public string DirctoryName { get; set; }
        public string FullName { get; set; }

        public ObservableCollection<MVCDirectoryModel> Children { get; set; } = new ObservableCollection<MVCDirectoryModel>();

    }
}