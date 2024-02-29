using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RollForward.Views
{
    public partial class MainForm : Form
    {
        public int SelectedVersion { get; set; } = -1;

        public MainForm(int[] versions)
        {
            InitializeComponent();

            this.versionCombobox.DataSource = versions; 
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SelectedVersion = (int)this.versionCombobox.SelectedItem;
            
            this.Hide();

        }
    }
}
