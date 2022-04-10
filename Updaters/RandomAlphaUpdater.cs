namespace KeyValue3Updater.Updaters
{
    internal class RandomAlphaUpdater : InitFloatUpdaterBase
    {
        protected override string BlockClassName => "C_INIT_RandomAlpha";
        protected override int outputField => 7;

        protected override string GetReplacement(ref string input)
        {
            var alphaMin = GetLineValueFloat(GetLine(ref input, "m_nAlphaMin")) / 255f;
            var alphaMax = GetLineValueFloat(GetLine(ref input, "m_nAlphaMax")) / 255f;

            return GetInitFloatString(ref input, alphaMin, alphaMax, outputField);
        }
    }
}
