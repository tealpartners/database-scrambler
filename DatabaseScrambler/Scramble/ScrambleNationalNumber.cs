using System;
using System.Collections.Generic;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleNationalNumber : BaseScramble
    {
        public ScrambleNationalNumber() 
            : base(ScrambleType.NationalNumber)
        {
        }

        protected override IList<string> GetScrambleData(string culture)
        {
            return GetResource("NationalNumbers.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
