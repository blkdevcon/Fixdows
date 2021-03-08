using System;
using System.Windows;

namespace Fixdows
{
    /// <summary>
    /// Interaction logic for UpdateUI.xaml
    /// </summary>
    public partial class UpdateUI : Window
    {
        public UpdateUI()
        {
            InitializeComponent();
            updateui_this = this;
        }

        internal UpdateUI updateui_this;
        internal string Status
        {
            get { return CurrentVersion_UpdUI.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { CurrentVersion_UpdUI.Content = value; })); }
        }

        internal string StatusLabelUpdate
        {
            get { return CurrentVersion_UpdUI.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { UPDATE_STATUS_LABEL.Content = value; })); }
        }

        internal string UpstreamLabelUpdate
        {
            get { return CurrentVersion_UpdUI.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { UPSTREAMVERSION_LABEL.Content = value; })); }
        }
    }
}
