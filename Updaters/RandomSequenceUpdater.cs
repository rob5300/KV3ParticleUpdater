namespace KeyValue3Updater.Updaters
{
    internal class RandomSequenceUpdater : InitFloatUpdaterBase
    {
        protected override string BlockClassName => "C_INIT_RandomSequence";
        protected override int outputField => 9;
        protected override string randomMinKey => "m_nSequenceMin";
        protected override string randomMaxKey => "m_nSequenceMax";
    }
}
