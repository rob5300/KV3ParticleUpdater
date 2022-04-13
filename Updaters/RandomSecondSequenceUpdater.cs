namespace KeyValue3Updater.Updaters
{
    internal class RandomSecondSequenceUpdater : InitFloatUpdaterBase
    {
        protected override string BlockClassName => "C_INIT_RandomSecondSequence";
        protected override int outputField => 13;
        protected override string randomMinKey => "m_nSequenceMin";
        protected override string randomMaxKey => "m_nSequenceMax";
    }
}
