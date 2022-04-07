namespace KeyValue3Updater.Updaters
{
    internal class RandomRadiusUpdater : InitFloatUpdaterBase
    {
        protected override string BlockClassName => "C_INIT_RandomRadius";
        protected override int outputField => 0;
        protected override string randomMinKey => "m_flRadiusMin";
        protected override string randomMaxKey => "m_flRadiusMax";

        protected override string GetReplacement(ref string input)
        {
            string replacement = base.GetReplacement(ref input);
            return replacement.Replace("m_nOutputField = \"0\"\r\n", "");
        }
    }
}
