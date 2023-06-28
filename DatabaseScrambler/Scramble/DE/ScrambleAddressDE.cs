using System;
using System.Collections.Generic;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble.DE
{
    public class ScrambleAddressDE : BaseScramble
    {
        public ScrambleAddressDE() 
            : base(ScrambleType.Address_DE)
        {
        }

        protected override IList<string> GetScrambleData()
        {
            return GetResource("Address.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
