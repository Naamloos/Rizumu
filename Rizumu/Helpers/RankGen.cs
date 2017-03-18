/*
 * Logic for generationg ranks. This code is not optimal. feel free to improve
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu
{
    class RankGen
    {
        public static string generate(int hitnotes, int maxnotes)
        {
            string rank = "d";
            float percent = ((float)hitnotes / maxnotes) * 100;
            if (percent == 100)
            {
                rank = "ss";
            }
            if (percent < 100)
            {
                rank = "s";
            }
            if (percent < 90)
            {
                rank = "a";
            }
            if (percent < 80)
            {
                rank = "b";
            }
            if (percent < 70)
            {
                rank = "c";
            }
            return rank;
        }
    }
}
