using System;
using System.Collections.Generic;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleCity : BaseScramble
    {
        public ScrambleCity() 
            : base(ScrambleType.City)
        {
        }

        protected override IList<string> GetScrambleData()
        {
            return GetResource("City.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }
    }
}
