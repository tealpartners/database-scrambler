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

        protected override IList<string> GetScrambleData()
        {
            return GetResource("PhoneNumbers.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }
    }
}
