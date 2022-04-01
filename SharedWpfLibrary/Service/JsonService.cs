using _SharedWpfLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _SharedWpfLibrary.Service
{
    public static class JsonService
    {
        public static SyncfusionLicanceModel SyncfusionLicance()
        {
            StreamReader r = new StreamReader("../../../../SharedWpfLibrary/Keys.json");
            string jsonString = r.ReadToEnd();
            SyncfusionLicanceModel m = JsonConvert.DeserializeObject<SyncfusionLicanceModel>(jsonString);
            return m;
        }
    }
}
