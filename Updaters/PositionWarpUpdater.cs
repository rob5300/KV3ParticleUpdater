using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValue3Updater.Updaters
{
    internal class PositionWarpUpdater : ReplacementUpdater
    {
        protected override string BlockClassName => "C_INIT_PositionWarp";

        protected override string GetReplacement(ref string input)
        {
            return input.Replace(BlockClassName, "C_INIT_PositionWarpScalar");
        }
    }
}
