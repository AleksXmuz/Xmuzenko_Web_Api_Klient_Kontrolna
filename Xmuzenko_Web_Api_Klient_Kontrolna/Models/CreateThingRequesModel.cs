using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xmuzenko_Web_Api_Klient_Kontrolna.Models
{
    class CreateThingRequesModel
    {
        public int IdTask { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime DateOfEnd { get; set; }
    }
}
