using Rosier.Glucose.Phone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosier.Glucose.Phone.ViewModels
{
    public class ConnectLiveViewModel : ViewModelBase
    {
        public ConnectLiveViewModel()
        {
        }

        public string LiveClientId
        {
            get;
            private set;
        }// return "000000004C0FE529"; } }

        public string Scopes { get { return "wl.signin wl.basic"; } }

        public async override Task LoadDataAsync()
        {
            var configuration = await Configuration.Load();
            this.LiveClientId = configuration.LiveApiClientId;
        }
    }
}
