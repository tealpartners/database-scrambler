using System;
using System.Collections.Generic;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleBirthdate : BaseScramble
    {
        public ScrambleBirthdate()
            : base(ScrambleType.Birthdate)
        {
        }

        protected override IList<string> GetScrambleData(string culture)
        {
            return GetResource("Birthdates.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
