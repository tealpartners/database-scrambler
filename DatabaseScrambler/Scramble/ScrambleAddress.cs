using System;
using System.Collections.Generic;
using System.Diagnostics;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleAddress : BaseScramble
    {
        public ScrambleAddress() 
            : base(ScrambleType.Address)
        {
        }

        /// <summary>
        /// Returns Address scramble data, supports DE-culture
        /// </summary>
        protected override IList<string> GetScrambleData(string culture)
        {
            var culturePrefix = ParseCulture(culture);
            return GetResource($"{culturePrefix}Address.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
