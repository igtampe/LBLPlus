using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Switchboard.Server;

namespace Igtapme.LBL.Server {
    /// <summary>Holds the LBL+ Extension</summary>
    public class LBLExtension:SwitchboardExtension {

        

        /// <summary>Creates and initializes an LBL</summary>
        public LBLExtension():base("LBL+","1.0") { 
            


        }

        public override string Help() {
            throw new NotImplementedException();
        }

        public override string Parse(ref SwitchboardUser User,string Command) {
            throw new NotImplementedException();
        }
    }
}
