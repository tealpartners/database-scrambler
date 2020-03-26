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
            var firstNames = GetResource("FirstNames.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var lastNames = GetResource("LastNames.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            var end = Math.Min(firstNames.Count(), lastNames.Count());

            var returnValue = new List<string>();

            for (var i = 0; i < end; i++)
            {
                var email = $"{firstNames[i]}.{lastNames[i]}@Acme-Corp.test";
                returnValue.Add(email.Replace(" ", "_"));
            }

            return returnValue;
        }
    }
}
