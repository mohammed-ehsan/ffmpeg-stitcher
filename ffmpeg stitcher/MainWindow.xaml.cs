using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.ComponentModel;
using ME.ffmpeg.wrapper;
using System.IO;

namespace ffmpeg_stitcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {


        private string _ImagesSourcePath;
        public string ImagesSourcePath
        {
            get { return _ImagesSourcePath; }
            set
            {
                _ImagesSourcePath = value;
                OnPropertyChanged("ImagesSourcePath");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFolder_Click(object sender, RoutedEventArgs e)
        {
            using (var ofd = new FolderBrowserDialog())
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtblk_imagesPath.Text = ofd.SelectedPath;
                    ImagesSourcePath = ofd.SelectedPath;
                }
            }
        }

        private void RenderVideo_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(ImagesSourcePath))
            {
                if (!string.IsNullOrEmpty(txtbx_nameingConv.Text))
                {
                    QueryCreator cq = new QueryCreator(ImagesSourcePath, txtbx_nameingConv.Text);
                    try
                    {
                        cq.FrameRate = 30;
                        cq.Height = 1080;
                        cq.Width = 1920;
                        Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog() { Filter = "mp4 files | *.mp4" };
                        if (sfd.ShowDialog() == true)
                        {
                            cq.RenderVideo(sfd.FileName);
                            
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
    }
}
