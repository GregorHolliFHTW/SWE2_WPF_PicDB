using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace WpfPicDB.ViewModels
{
    public class MainWindowVM
    {
        private readonly string _title;
        private ImageTabVM _imageTabVM;
        private ImageScrollerVM _imageScrollerVM;
        private ImagePreviewVM _imagePreviewVM;
        private TaskbarVM _taskbarVM;

        public MainWindowVM(ImageTabVM imageTabVM, ImageScrollerVM imageScrollerVM, ImagePreviewVM imagePreviewVM, TaskbarVM taskbarVM)
        {
            _imageTabVM = imageTabVM;
            _imageScrollerVM = imageScrollerVM;
            _imagePreviewVM = imagePreviewVM;
            _taskbarVM = taskbarVM;
            _title = ConfigurationManager.AppSettings["MainWindowTitle"];
        }

        public string Title => _title;

        public ImageTabVM ImageTabVM { get => _imageTabVM; set => _imageTabVM = value; }
        public ImageScrollerVM ImageScrollerVM { get => _imageScrollerVM; set => _imageScrollerVM = value; }
        public ImagePreviewVM ImagePreviewVM { get => _imagePreviewVM; set => _imagePreviewVM = value; }
        public TaskbarVM TaskbarVM { get => _taskbarVM; set => _taskbarVM = value; }
    }
}
