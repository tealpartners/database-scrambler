using System;
using System.Collections.Generic;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleContractNumber : BaseScramble
    {
        public ScrambleContractNumber()
            : base(ScrambleType.ContractNumber)
        {
        }

        protected override IList<string> GetScrambleData()
        {
            return GetResource("ContractNumbers.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }
    }
}
