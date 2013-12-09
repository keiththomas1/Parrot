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

            
            webBrowser.Url = new Uri("http://www.google.com");
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
            splitContainer1.SplitterDistance = 30;
        }
    }
}
