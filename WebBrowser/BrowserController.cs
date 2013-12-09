using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser
{
    class BrowserController
    {
        public BrowserController()
        {
            browsers = new List<System.Windows.Forms.WebBrowser>();
            browserCount = 0;
        }

        // Make all browsers invisible except current.
        private void DisableOtherBrowsers(int index)
        {
            for (int i = 0; i < browserCount; i++)
            {
                // If not current browser, disable
                if( index != i )
                {
                    browsers[i].Visible = false;
                }
            }
        }

        public void ChangeBrowser(int index)
        {
            browsers[index].Visible = true;
            DisableOtherBrowsers(index);
        }

        public System.Windows.Forms.WebBrowser Add()
        {
            System.Windows.Forms.WebBrowser wb = new System.Windows.Forms.WebBrowser();
            wb.Url = new Uri("http://www.bing.com");
            browsers.Add(wb);

            DisableOtherBrowsers(browserCount);
            browserCount++;

            return wb;
        }

        public void Add(System.Windows.Forms.WebBrowser wb)
        {
            browsers.Add(wb);
            browserCount++;
        }

        public int BrowserCount 
        { 
            get { return browserCount; }
            set { browserCount =  value; } 
        }

        private List<System.Windows.Forms.WebBrowser> browsers;
        private int browserCount;
    }
}
