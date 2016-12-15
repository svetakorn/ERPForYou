using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ERPForYou.Data
{
    public  class Repository
    {
        public  List<Material> Materials;
        public  List<Trademark> Trademark;
        public  List<Agent> Agents;
        public  List<Zakaz> Receipts;
        public  List<Sklad> Sklad;

        public void UpdateUe()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://pestova.styleru.net/proga/get_ue");

            }

        }
    }
}
