using System.Text.RegularExpressions;

namespace KeyValue3Updater
{
    internal abstract class InsertUpdater : Updater
    {
        protected virtual string InsertContainerBlockName { get; }

        private Regex insertBlockRegex;

        public InsertUpdater() : base()
        {
            insertBlockRegex = new Regex(InsertContainerBlockName + @" ?= ?\n?", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }

        public override string Process(ref string input)
        {
            string classname = GetType().Name;
            Match match = findRegex.Match(input);
            if (match == null || !match.Success)
            {
                Log($"[{classname}] Found 0 matches and did not update.");
            }
            while (match != null && match.Success)
            {
                Log($"[{classname}] Found match to move and insert elsewhere.");

                //Increase match length by 1 to include a comma if found.
                var length = match.Length;
                char afterChar = input[match.Index + length];
                if (afterChar == '\n')
                {
                    length += 1;
                }

                //Remove block from old container block
                input = input.Remove(match.Index, length);
                //Insert block into new destination container block
                input = InsertBlock(ref input, match.Value);
                match = findRegex.Match(input);
            }

            return input;
        }

        protected string InsertBlock(ref string toUpdate, string foundBlock)
        {
            int matchIndex;
            int matchLength;
            var match = insertBlockRegex.Match(toUpdate);
            if(match == null || string.IsNullOrEmpty(match.Value))
            {
                //No destination block found, lets insert one ourselves to contain the moved block.
                var oldLength = toUpdate.Length;
                var newDestinationBlockContainer = GetNewInsertBlockContainer();
                toUpdate = toUpdate.Insert(oldLength - 1, newDestinationBlockContainer);

                //Set match index and length based on the block we just made
                matchIndex = oldLength - 1;
                matchLength = newDestinationBlockContainer.Length;
            }
            else
            {
                matchIndex = match.Index;
                matchLength = match.Length;
                //Search forward to find the blocks ending ] to get the correct end of this block
                int openBrackets = 0;
                for(int i = matchIndex + matchLength - 1; i < toUpdate.Length; i++)
                {
                    char c = toUpdate[i];
                    if(c == '[')
                    {
                        openBrackets++;
                    }
                    else if(c == ']')
                    {
                        openBrackets--;
                        if (openBrackets == 0)
                        {
                            matchLength = i - matchIndex + 1;
                            break;
                        }
                    }
                }
            }

            //Insert new block 1 before the end of the match to be inside the desired section
            int insertIndex = matchIndex + matchLength - 1;
            string newToInsert = GetBlockToInsert(foundBlock) + "\n";
            var newString = toUpdate.Insert(insertIndex, newToInsert);
            Log($"[{GetType().Name}] Inserted block successfully.");
            return newString;
        }

        protected abstract string GetBlockToInsert(string foundBlock);

        private string GetNewInsertBlockContainer()
        {
            return $"{InsertContainerBlockName} = \n[\n]";
        }
    }
}
