using MVCChecker.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace MVCChecker
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void xamDataTree_SelectedNodesCollectionChanged(object sender, Infragistics.Controls.Menus.NodeSelectionEventArgs e)
        {
            var mvc = (MVCDirectoryModel)xamDataTree.SelectedDataItems[0];
            rssViewModel.FolderPath = mvc.FullName;
        }

        private void _ListViewFile_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(rssViewModel.MVCFiles[_ListViewFile.SelectedIndex].FullName);
        }

        private void _TextBoxPath_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            rssViewModel.Path = _TextBoxPath.Text;

        }
    }
}