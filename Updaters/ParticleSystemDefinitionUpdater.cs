using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace KeyValue3Updater.Updaters
{
    internal class ParticleSystemDefinitionUpdater : Updater
    {
        private const string NewBehaviourVersionLine = "m_nBehaviorVersion = 10";

        public ParticleSystemDefinitionUpdater()
        {
            findRegex = GetBlockRegex("CParticleSystemDefinition");
        }

        public override string Process(ref string input)
        {
            string name = GetType().Name;
            var match = findRegex.Match(input);
            if(match.Success)
            {
                string block = match.Value;
                var behaviourVersionLine = GetLine(ref block, "m_nBehaviorVersion");
                if (string.IsNullOrEmpty(behaviourVersionLine))
                {
                    //Insert the version line to the block
                    var blockStartMatch = Regex.Match(block, @"_class = ""CParticleSystemDefinition""\n?");
                    if (blockStartMatch.Success)
                    {
                        block = block.Insert(blockStartMatch.Index + blockStartMatch.Length, NewBehaviourVersionLine + "\n");
                        Log($"[{name}] Inserted behaviour version.");
                    }
                    else
                    {
                        Log($"[{name}] !ERROR!, failed to find start of block to insert version");
                    }
                }
                else
                {
                    var behaviourVersion = GetLineValueInt(behaviourVersionLine);
                    if (behaviourVersion < 10)
                    {
                        block = block.Replace(behaviourVersionLine, NewBehaviourVersionLine);
                        Log($"[{name}] Updated behaviour version to: '{NewBehaviourVersionLine}'");
                    }
                    else
                    {
                        Log($"[{name}] Did not behaviour version as it was high enough already ({behaviourVersion}).");
                    }
                }

                return input.Replace(match.Value, block);
            }
            else
            {
                Log($"[{name}] ParticleSystenDefinition block was not found?");
            }

            return input;
        }
    }
}
