using System;
using System.Collections.Generic;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleFirstName : BaseScramble
    {
        public ScrambleFirstName() 
            : base(ScrambleType.FirstName)
        {
        }

        protected override IList<string> GetScrambleData()
        {
            return GetResource("FirstNames.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
