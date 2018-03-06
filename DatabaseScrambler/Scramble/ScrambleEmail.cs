using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleEmail : BaseScramble
    {
        public ScrambleEmail() 
            : base(ScrambleType.Email)
        {
        }

        protected override IList<string> GetScrambleData()
        {
            var firstNames = GetResource("FirstNames.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var lastNames = GetResource("LastNames.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var end = Math.Min(firstNames.Count(), lastNames.Count());

            var returnValue = new List<string>();

            for (var i = 0; i < end; i++)
            {
                var email = $"{firstNames[i]}.{lastNames[i]}@Evil-Corp.test";
                returnValue.Add(email.Replace(" ", "_"));
            }

            return returnValue;
        }
    }
}
