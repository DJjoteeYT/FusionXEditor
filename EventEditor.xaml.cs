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
using FusionX_Editor.CustomControls;
using FusionX_Editor.Editor.EventEditor;

namespace FusionX_Editor
{
    /// <summary>
    /// Interaction logic for EventEditor.xaml
    /// </summary>
    public partial class EventEditor : Page
    {
        public FEventEditor Editor;
        public EventEditor()
        {
            InitializeComponent();
            Editor = new FEventEditor();
            Editor.Init();
        }

        public void Redraw()
        {
            EventGroups.Children.Clear();
            Header.EventObjectsPanel.Children.Clear();
            
            foreach (var eventObject in Editor.Objects)
            {
                var newObjectControl = new EventObjectControl();
                Header.EventObjectsPanel.Children.Add(newObjectControl);
            }
            
            foreach (var eventGroup in Editor.Events)
            {
                var newGroupControl = new EventGroupControl();
                foreach (var eventObject in Editor.Objects)
                {
                    var newActionControl = new ActionControl();
                    newGroupControl.ActionPanel.Children.Add(newActionControl);
                }

                EventGroups.Children.Add(newGroupControl);
            }
        }
    }
}
