using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Projekat_PR38_2019
{
    public class SwitchEntity : PowerEntity
    {
        private string status;

        public string Status
        {
            get { return status; }

            set { status = value; }
        }
    }
}
