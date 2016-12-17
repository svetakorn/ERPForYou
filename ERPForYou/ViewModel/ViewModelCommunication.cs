using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPForYou.ViewModel
{
    public static class ViewModelCommunication
    {
        static ViewModelCommunication()
        {
            Messaging = new Messenger();
        }

        public static Messenger Messaging { get; set; }


        // message properties
        public static string LogInChanged
        {
            get { return "LogInChanged"; }

        }
    }
}
