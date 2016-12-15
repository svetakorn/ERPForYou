using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ERPForYou.Data
{
    public static class Repository
    {
        public static List<Material> Materials; //
        public static List<Trademark> Trademarks;
        public static List<Agent> Agents; //
        public static List<UE> Ues; //
        public static List<Type> Types; //
        public static List<Zakaz> Zakazs;
        public static List<Sklad> Sklads;

        public static void UpdateUe()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://pestova.styleru.net/proga/get_ue");
                Ues = JsonConvert.DeserializeObject<List<UE>>(json);
            }
        }

        public static void UpdateType()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://pestova.styleru.net/proga/get_type");
                Types = JsonConvert.DeserializeObject<List<Type>>(json);
            }
        }

        public static void UpdateMaterial()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://pestova.styleru.net/proga/get_material");
                Materials = JsonConvert.DeserializeObject<List<Material>>(json);
            }
        }

        public static void UpdateAgent()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://pestova.styleru.net/proga/get_agent");
                Agents = JsonConvert.DeserializeObject<List<Agent>>(json);
            }
        }

        public static void UpdateZakaz()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://pestova.styleru.net/proga/get_zakaz");
                Zakazs = JsonConvert.DeserializeObject<List<Zakaz>>(json);
            }
        }

        public static void UpdateTrademark()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://pestova.styleru.net/proga/get_agent");
                Trademarks = JsonConvert.DeserializeObject<List<Trademark>>(json);
            }
        }
        public static void UpdateSklad()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://pestova.styleru.net/proga/get_sklad");
                Sklads = JsonConvert.DeserializeObject<List<Sklad>>(json);
            }
        }
    }
}
