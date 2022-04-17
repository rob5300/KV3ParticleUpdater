using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValue3Updater.Updaters
{
    internal class CreationNoiseUpdater : InsertUpdater
    {
        protected override string BlockClassName => "C_INIT_CreationNoise";
        protected override string InsertContainerBlockName => "m_Operators";

        protected override string GetBlockToInsert(string foundBlock)
        {
            var newString = foundBlock.Replace("C_INIT_CreationNoise", "C_OP_Noise");
            newString = newString.Replace("m_flNoiseScale =", "m_flNoiseAnimationTimeScale =");

            var noiseScaleLine = GetLine(ref foundBlock, "m_flNoiseScaleLoc");
            if(!string.IsNullOrEmpty(noiseScaleLine))
            {
                //Replace noise scale if the line was found
                var newNoiseScaleLine = $"m_fl4NoiseScale = {GetLineValueFloat(noiseScaleLine) / 1000f}";
                newString = newString.Replace(noiseScaleLine, newNoiseScaleLine);
            }

            return newString;
        }
    }
}
