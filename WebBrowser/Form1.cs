using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowser
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            splitContainer2.SplitterDistance = 0;
            splitContainer2.IsSplitterFixed = true;
            // Set up text for tabs
            tabPage1.Text = "Tab 1";
            tabPage2.Text = "+";

            MaintainBoundaries();

            webBrowser.Url = new Uri("http://www.google.com");
        }

        // Ensures that all of the moving parts are kept as-is. For example,
        // we don't want splitContainer1 changing it's splitterDistance.
        private void MaintainBoundaries()
        {
            splitContainer1.SplitterDistance = 50;

            Point p = new Point();
            p.X = splitContainer2.SplitterDistance;
            p.Y = tabControl1.Location.Y;
            tabControl1.Location = p;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        // On go button press.
        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(textBox1.Text);
        }

        // On back button press.
        private void button1_Click(object sender, EventArgs e)
        {
            if (webBrowser.CanGoBack)
            {
                webBrowser.GoBack();
            }
            textBox1.Text = webBrowser.Url.ToString();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            webBrowser.Refresh();
            
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            webBrowser.GoForward();
        }

        private void finishedNavigating(object sender, WebBrowserNavigatedEventArgs e)
        {
            textBox1.Text = webBrowser.Url.ToString();
        }

        private void resizeEnded(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 50;

            Point p = new Point();
            p.X = splitContainer2.SplitterDistance;
            p.Y = tabControl1.Location.Y;
            tabControl1.Location = p;
            tabControl1.SelectedTab.Text = "Hello";
            
        }

        private void onKeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void onURLKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                webBrowser.Navigate(textBox1.Text);
                
            }
        }

        private void onChangeSelection(object sender, EventArgs e)
        {
            //if (tabControl1.SelectedTab == )
            {
                
            }
        }
    }
}
