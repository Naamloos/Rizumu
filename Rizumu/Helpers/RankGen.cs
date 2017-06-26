/*
 * Logic for generationg ranks. This code is not optimal. feel free to improve
 */


namespace Rizumu
{
    class RankGen
    {
        public static string generate(int hitnotes, int maxnotes)
        {
            float percentage = (hitnotes / maxnotes) * 100;

            if (percentage == 100)
                return "ss";
            if (percentage > 99)
                return "s";
            if (percentage > 95)
                return "a";
            if (percentage > 85)
                return "b";
            if (percentage > 70)
                return "c";
            else
                return "d";

            /*
            // ss, s, a, b, c, d
            string rank = "ss";
            return rank;
            */    
        }
    }
}
