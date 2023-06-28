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

        /// <summary>
        /// Returns City scramble data, supports DE-culture
        /// </summary>
        protected override IList<string> GetScrambleData(string culture)
        {
            var culturePrefix = ParseCulture(culture);
            return GetResource($"{culturePrefix}City.txt").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
