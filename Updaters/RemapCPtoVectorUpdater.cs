using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValue3Updater.Updaters
{
    internal class RemapCPtoVectorUpdater : InsertUpdater
    {
        protected override string BlockClassName => "C_INIT_RemapCPtoVector";
        protected override string InsertContainerBlockName => "m_Operators";

        protected override string GetBlockToInsert(string foundBlock)
        {
            var newString = foundBlock.Replace("C_INIT_RemapCPtoVector", "C_OP_RemapCPtoVector");
            return newString;
        }
    }
}
