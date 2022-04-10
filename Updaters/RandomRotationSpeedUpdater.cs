namespace KeyValue3Updater.Updaters
{
    internal class RandomRotationSpeedUpdater : InitFloatUpdaterBase
    {
        protected override string BlockClassName => "C_INIT_RandomRotationSpeed";
        protected override int outputField => 5;

        protected override string GetReplacement(ref string input)
        {
            var degreesMin = GetLineValueFloat(GetLine(ref input, "m_flDegreesMin")) / RadToDeg;
            var degreesMax = GetLineValueFloat(GetLine(ref input, "m_flDegreesMax")) / RadToDeg;

            return GetInitFloatString(ref input, degreesMin, degreesMax, outputField);
        }
    }
}
