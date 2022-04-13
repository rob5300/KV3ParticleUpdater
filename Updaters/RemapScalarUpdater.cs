using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValue3Updater.Updaters
{
    internal class RemapScalarUpdater : InsertUpdater
    {
        protected override string BlockClassName => "C_INIT_RemapScalar";
        protected override string InsertContainerBlockName => "m_Operators";

        protected override string GetBlockToInsert(string foundBlock)
        {
            var newString = foundBlock.Replace("C_INIT_RemapScalar", "C_OP_RemapScalar");
            return newString;
        }
    }
}
