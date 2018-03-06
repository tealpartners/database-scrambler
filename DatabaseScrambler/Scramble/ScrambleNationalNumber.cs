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

        protected override IList<string> GetScrambleData()
        {
            return GetResource("NationalNumbers.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
