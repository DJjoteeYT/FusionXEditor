using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FusionX_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Welcome WelcomePage = new();
        public FrameEditor FrameEditor = new();
        public EventEditor EventEditor = new();

        private int NewAppCount = 0;

        public bool AnyApplicationOpen;
        private bool ApplicationIsOpen
        {
            get
            {
                return false; //i dunno
            }
            set
            {
                AnyApplicationOpen = value;

                Close.IsEnabled = value;
                Save.IsEnabled = value;
                SaveAs.IsEnabled = value;
                ProjectTools.IsEnabled = value;
                Build.IsEnabled = value;

                RunProject.IsEnabled = value;
                RunApp.IsEnabled = value;
                RunAppFromCurFrame.IsEnabled = value;
                RunAppFrame.IsEnabled = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Editor.Source = new Uri("Welcome.xaml", UriKind.RelativeOrAbsolute);
            EventEditor.Editor.Init();
        }

        #region Menubar
        #region File
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            NewAppCount++;
            NewApplication("Application " + NewAppCount);
        }
        #endregion
        #region View
        private void WelcomePage_Click(object sender, RoutedEventArgs e)
        {
            Editor.Content = Welcome;
        }
        private void FrameEditor_Click(object sender, RoutedEventArgs e)
        {
            Editor.Content = FrameEditor;
        }
        private void EventEditor_Click(object sender, RoutedEventArgs e)
        {
            Editor.Content = EventEditor;
            EventEditor.Redraw();

        }
        #endregion
        #endregion

        private void NewApplication(string name)
        {
            TreeViewItem app = new TreeViewItem();
            app.Name = name.Trim(' ');
            //WorkspaceToolbar_Tree.
        }
        private void NewFrame(string appName)
        {

        }
    }
}
