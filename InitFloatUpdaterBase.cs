namespace KeyValue3Updater
{
    internal abstract class InitFloatUpdaterBase : Updater
    {
        protected virtual int outputField => 0;
        protected virtual string randomMinKey => null;
        protected virtual string randomMaxKey => null;

        protected override string GetReplacement(ref string input)
        {
            var randomMin = GetLineValue(GetLine(ref input, randomMinKey));
            var randomMax = GetLineValue(GetLine(ref input, randomMaxKey));
            return GetInitFloatString(ref input, randomMin, randomMax, outputField);
        }
    }
}
