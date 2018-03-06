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

        protected override IList<string> GetScrambleData()
        {
            return GetResource("Zipcode.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }
    }
}
