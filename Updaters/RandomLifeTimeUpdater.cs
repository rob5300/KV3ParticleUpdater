namespace KeyValue3Updater.Updaters
{
    internal class RandomLifeTimeUpdater : InitFloatUpdaterBase
    {
        protected override string BlockClassName => "C_INIT_RandomLifeTime";
        protected override int outputField => 1;
        protected override string randomMinKey => "m_fLifetimeMin";
        protected override string randomMaxKey => "m_fLifetimeMax";
    }
}
