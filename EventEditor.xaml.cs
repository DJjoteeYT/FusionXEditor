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

            for (int i = 0; i < Editor.Events.Count; i++)
            {
                var eventGroup = Editor.Events[i];
                var newGroupControl = new EventGroupControl();
                newGroupControl.EvGroupNum.Text = (i + 1).ToString();
                foreach (var condition in eventGroup.Conditions)
                {
                    var newConditionControl = new ConditionControl();
                    newConditionControl.CondText.Text = condition.Text;
                    newGroupControl.Conditions.Children.Add(newConditionControl);
                }
                foreach (var eventObject in Editor.Objects)
                {
                    var newActionControl = new ActionControl();
                    if(eventGroup.Actions.Count>0)
                        newActionControl.Checkmark.Visibility = Visibility.Visible;

                    newGroupControl.ActionPanel.Children.Add(newActionControl);
                }

                
                EventGroups.Children.Add(newGroupControl);
            }

            var createEventGroup = new EventGroupControl();
            createEventGroup.EvGroupNum.Text=(Editor.Events.Count+1).ToString();
            var newCondition = new ConditionControl();
            newCondition.CondText.Text = "New condition";
            createEventGroup.Conditions.Children.Add(newCondition);
            EventGroups.Children.Add(createEventGroup);
            EventGroups.Children.Add(new Separator() { Background = Brushes.Transparent, Height = 200});
        }
    }
}
