using System;
using System.Collections.Generic;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleZipcode : BaseScramble
    {
        public ScrambleZipcode() 
            : base(ScrambleType.Zipcode)
        {
        }

        protected override IList<string> GetScrambleData(string culture)
        {
            return GetResource("Zipcode.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
