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

        protected override IList<string> GetScrambleData()
        {
            return GetResource("Birthdates.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }
    }
}
