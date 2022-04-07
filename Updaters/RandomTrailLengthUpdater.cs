namespace KeyValue3Updater.Updaters
{
    internal class RandomTrailLengthUpdater : InitFloatUpdaterBase
    {
        protected override string BlockClassName => "C_INIT_RandomTrailLength";
        protected override int outputField => 10;
        protected override string randomMinKey => "m_flMinLength";
        protected override string randomMaxKey => "m_flMaxLength";
    }
}
