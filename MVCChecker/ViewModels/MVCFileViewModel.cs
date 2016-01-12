using MVCChecker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MVCChecker.ViewModels
{
    internal class MVCFileViewModel : INotifyPropertyChanged
    {
        // Model
        MVCModel mvcModel = new MVCModel();

        public string Path
        {
            get { return mvcModel.InfoData.DirectoryPath; }
            set
            {
                mvcModel.InfoData.DirectoryPath = value;
                NotifyPropertyChanged(nameof(Path));
            }
        }

        public string FolderPath
        {
            get { return mvcModel.InfoData.FolderPath; }
            set
            {
                mvcModel.InfoData.FolderPath = value;
                NotifyPropertyChanged(nameof(FolderPath));
            }
        }
        public string FileText
        {
            get { return mvcModel.InfoData.FileText; }
            set
            {
                mvcModel.InfoData.FileText = value;
                NotifyPropertyChanged(nameof(FileText));
            }
        }


        public ObservableCollection<MVCDirectoryModel> MVCDirectory => mvcModel.MVCDirectory;

        public ObservableCollection<MVCFileModel> MVCFiles => mvcModel.MVCFiles;



        private MVCFileModel _selectedItem = null;

        public MVCFileModel SelectMVCFiles
        {
            get { return _selectedItem; }
            set
            {
                if (SelectMVCFiles == value)
                    return;

                _selectedItem = value;
                NotifyPropertyChanged(nameof(SelectMVCFiles));

                //やりたいことを実行
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Path))
            {
                mvcModel.SetDirectory();
            }
            if (propertyName == nameof(FolderPath))
            {
                mvcModel.SetDirectoryFile();
            }
            if (propertyName == nameof(SelectMVCFiles))
            {
                mvcModel.SetFileText(SelectMVCFiles);
                NotifyPropertyChanged(nameof(FileText));
            }
        }

        private ICommand getRSS;
        public ICommand GetRSS => getRSS ?? (getRSS = new GetRSSCommand(this));

        // RSSフィードを取得するコマンドです。
        private class GetRSSCommand : ICommand
        {

            // ViewModel
            private MVCFileViewModel rssViewModel;


            //　コンストラクタ
            public GetRSSCommand(MVCFileViewModel viewModel)
            {
                rssViewModel = viewModel;
                // コマンド実行の可否の変更を通知します。
                rssViewModel.PropertyChanged += (sender, e) =>
                    CanExecuteChanged?.Invoke(sender, e);
            }


            // コマンドを実行できるかどうかを取得します。
            public bool CanExecute(object parameter) => true;


            // コマンド実行の可否の変更した時のイベントハンドラです。
            public event EventHandler CanExecuteChanged;

            // コマンドを実行し、RSSフィードを取得します。
            public void Execute(object parameter)
            {
            }

        }

    }
}