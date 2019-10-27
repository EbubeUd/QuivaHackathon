using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class PlayerDetails
    {
        public string Address { get; set; }
        public List<int> Arrows { get; set; }
        public int ActiveArrow { get; set; }
        public string PrivateKey { get; set; }
    }
}
