using System;
using System.Collections.Generic;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScramblePhoneNumber : BaseScramble
    {
        public ScramblePhoneNumber()
            : base(ScrambleType.PhoneNumber)
        {
        }

        protected override IList<string> GetScrambleData(string culture)
        {
            return GetResource("PhoneNumbers.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
