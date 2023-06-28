using System;
using System.Collections.Generic;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble.DE
{
    public class ScrambleCityDE : BaseScramble
    {
        public ScrambleCityDE() 
            : base(ScrambleType.City_DE)
        {
        }

        protected override IList<string> GetScrambleData()
        {
            return GetResource("City.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
