using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser
{
    class UserSettings
    {
        public UserSettings()
        { }

        public string HomePageString
        {
            get { return homePage; }
            set { homePage = value; }
        }

        public Uri HomePageURI
        {
            get { return new Uri(homePage); }
            set { homePage = value.ToString(); }
        }

        private string homePage;
    }
}
