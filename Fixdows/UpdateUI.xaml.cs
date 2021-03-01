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
using System.Windows.Shapes;

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
    }
}
