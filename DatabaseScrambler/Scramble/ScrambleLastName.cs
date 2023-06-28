using System;
using System.Collections.Generic;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleLastName : BaseScramble
    {
        public ScrambleLastName() 
            : base(ScrambleType.LastName)
        {
        }

        protected override IList<string> GetScrambleData(string culture)
        {
            return GetResource("LastNames.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
