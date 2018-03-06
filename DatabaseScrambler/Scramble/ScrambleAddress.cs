using System;
using System.Collections.Generic;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleAddress : BaseScramble
    {
        public ScrambleAddress() 
            : base(ScrambleType.Address)
        {
        }

        protected override IList<string> GetScrambleData()
        {
            return GetResource("Address.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
