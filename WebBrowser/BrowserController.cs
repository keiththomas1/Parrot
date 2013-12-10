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

        public System.Windows.Forms.WebBrowser GetCurrentBrowser(int index)
        {
            try
            {
                return browsers[index];
            }
            catch (Exception e)
            {
                Console.Write("No browser at that index..\n");
                return null;
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
            DisableOtherBrowsers(browserCount);
            browserCount++;
        }

        public int FindBrowserIndex(System.Windows.Forms.WebBrowser wb)
        {
            for (int i = 0; i < browserCount; i++)
            {
                // If not current browser, disable
                if (browsers[i] == wb)
                {
                    return i;
                }
            }
            return 0;
        }

        public Uri GetURIAtIndex(int index)
        {
            return browsers[index].Url;
        }

        public void Delete(int index)
        {
            System.Windows.Forms.WebBrowser wb = browsers[index];
            browsers.RemoveAt(index);
            //browsers.Remove(browsers[index]);
            wb.Dispose();
            browserCount--;
            ChangeBrowser(0);
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
