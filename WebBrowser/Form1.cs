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
        private BrowserController browserController;
        private UserSettings userSettings;
        private System.Windows.Forms.WebBrowser currentBrowser;
        private List<TabPage> tabs;

        public MainForm()
        {
            InitializeComponent();

            // Set up splitter distances
            splitContainer1.SplitterDistance = 50;
            splitContainer2.SplitterDistance = 0;
            splitContainer2.IsSplitterFixed = true;

            // Set up text for tabs
            tabPage1.Text = "New Tab";
            tabPage2.Text = "+";
            tabs = new List<TabPage>();
            tabs.Add(tabPage1);
            tabs.Add(tabPage2);

            // Set up browserController and add the initial browser to it.
            browserController = new BrowserController();
            // Add a new browser window. (Along with all it's details)
            System.Windows.Forms.WebBrowser wb = browserController.Add();
            currentBrowser = wb;
            splitContainer2.Panel2.Controls.Add(currentBrowser);
            currentBrowser.Url = new Uri("http://www.google.com");
            currentBrowser.Dock = DockStyle.Fill;
            currentBrowser.DocumentCompleted +=
                new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(webBrowserCompleted);

            userSettings = new UserSettings();
            userSettings.HomePageString = "http://www.bing.com";

            UpdateProperties();
        }

        // Ensures that all of the moving parts are kept as-is. For example,
        // we don't want splitContainer1 changing it's splitterDistance.
        private void UpdateProperties()
        {
            // Cancel out any auto re-sizing of splitContainer1
            splitContainer1.SplitterDistance = 50;

            // Change location of tab system if splitContainer2.Panel1 is out
            Point p = new Point();
            p.X = splitContainer2.SplitterDistance;
            p.Y = tabControl1.Location.Y;
            closeTabButton.Location = p;
            p.X = splitContainer2.SplitterDistance + 36;
            p.Y = tabControl1.Location.Y;
            tabControl1.Location = p;

            // Enable/disable back/forward buttons on the go
            backButton.Enabled = currentBrowser.CanGoBack;
            forwardButton.Enabled = currentBrowser.CanGoForward;
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
            currentBrowser.Navigate(textBox1.Text);
        }

        // On back button press.
        private void button1_Click(object sender, EventArgs e)
        {
            if (currentBrowser.CanGoBack)
            {
                currentBrowser.GoBack();
            }
            textBox1.Text = currentBrowser.Url.ToString();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            currentBrowser.Refresh();
            
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            currentBrowser.GoForward();
        }

        private void finishedNavigating(object sender, WebBrowserNavigatedEventArgs e)
        {
            textBox1.Text = currentBrowser.Url.ToString();

            UpdateProperties();
        }

        private void resizeEnded(object sender, EventArgs e)
        {

            UpdateProperties();
        }

        private void onKeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void onURLKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                currentBrowser.Navigate(textBox1.Text);
            }
        }

        private void webBrowserCompleted(object sender, EventArgs ev)
        {
            System.Windows.Forms.WebBrowser wb = (System.Windows.Forms.WebBrowser)sender;
            int index = browserController.FindBrowserIndex(wb);
            tabs[index].Text = wb.DocumentTitle;

            UpdateProperties();
        }

        private void AddNewTabAndBrowser()
        {
            // Add a new browser window.
            System.Windows.Forms.WebBrowser wb = browserController.Add();
            currentBrowser = wb;
            splitContainer2.Panel2.Controls.Add(currentBrowser);
            currentBrowser.Url = userSettings.HomePageURI;
            currentBrowser.Dock = DockStyle.Fill;

            // Set current "+" tab to new tab name, and make a new "+" tab.
            int browserIndex = browserController.BrowserCount;
            tabControl1.SelectedTab.Text = "New Tab";
            TabPage myTabPage = new TabPage("+");
            tabControl1.TabPages.Add(myTabPage);
            tabs.Add(myTabPage);

            textBox1.Text = userSettings.HomePageString;

            wb.DocumentCompleted +=
                new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(webBrowserCompleted);
        }

        private void onChangeSelection(object sender, EventArgs e)
        {
            if ("+" == tabControl1.SelectedTab.Text)
            {
                AddNewTabAndBrowser();
            }
            else
            {
                int index = tabControl1.TabPages.IndexOf(tabControl1.SelectedTab);
                browserController.ChangeBrowser(index);
                currentBrowser = browserController.GetCurrentBrowser(index);

                try
                {
                    textBox1.Text = browserController.GetURIAtIndex(index).ToString();
                }
                catch (System.NullReferenceException err)
                {
                    textBox1.Text = userSettings.HomePageString;
                    Console.Write(err.Message);
                }
            }
        }

        private void closeTabButton_Click(object sender, EventArgs e)
        {
            int index = browserController.FindBrowserIndex(currentBrowser);
            browserController.Delete(index);
            currentBrowser = browserController.GetCurrentBrowser(0);

            tabControl1.TabPages.RemoveAt(index);
            tabs.RemoveAt(index);
        }

        private void onWindowEnterFocus(object sender, EventArgs e)
        {
            UpdateProperties();
        }

        // Home button press
        private void button1_Click_1(object sender, EventArgs e)
        {
            currentBrowser.Navigate(userSettings.HomePageString);
        }
    }
}
