using BreakInfinity;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ProgressApocalypse
{
    public static class PaFunctions
    {
        /// <summary>
        /// Formats currency into K M B T format and into 9.99e75 format
        /// Use only F0 or F2 format (F2 for money F0 for materials, items etc)
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatCurrencyKMB(BigDouble currency, string format)
        {
            string currency_text = "$0";

            var prefixes = new Dictionary<BigDouble, string>
        {
            {3, "k" },
            {6, "m" },
            {9, "b" },
            {12, "t" },
            {15, "q" },
            {18, "Q" },
            {21, "s" },
            {24, "S" },
            {27, "o" },
            {30, "n" },
            {33, "d" },
            {36, "u" },
            {39, "D" },
            {42, "T" },
            {45, "qu" },
            {48, "Qu" },
            {51, "se" },
            {54, "Se" },
            {57, "O" },
            {60, "N" },
            {63, "V" },
            {66, "U" },
            {69, "du" },
            {72, "Tr" }
        };

            var exponent = BigDouble.Floor(BigDouble.Log10(currency));
            var thirdExponent = 3 * BigDouble.Floor(exponent / 3);
            var mantissa = currency / BigDouble.Pow(10, thirdExponent);

            if (currency < 1000) currency_text = currency.ToString(format);
            if (currency >= 1000)
            {
                if (currency >= 1e75)
                {
                    currency_text = mantissa.ToString(format) + "e" + thirdExponent;
                }
                else
                {
                    currency_text = mantissa.ToString(format) + prefixes[thirdExponent];
                }
            }

            return currency_text;
        }

        // formats resources to be 1kp 25g 23s / 25g 23s 30c and colors it
        public static string FormatResources(BigDouble resources)
        {
            StringBuilder resourcesText = new();

            var colors = new Dictionary<char, string>
            {
                { 'p', "#7EB1EC" },
                { 'g', "#F6CB6E" },
                { 's', "#D9D9D9" },
                { 'c', "#AE611B" }
            };
            var typeValue = new Dictionary<char, BigDouble>
            {
                {'p', 1000000 },
                {'g', 10000 },
                {'s', 100 }
            };

            var p = BigDouble.Truncate(resources / typeValue['p']);
            var g = BigDouble.Truncate((resources - (p * typeValue['p'])) / typeValue['g']);
            var s = BigDouble.Truncate((resources - (p * typeValue['p']) - (g * typeValue['g'])) / typeValue['s']);
            var c = BigDouble.Truncate(resources - (p * typeValue['p']) - (g * typeValue['g']) - (s * typeValue['s']));

            if(p > 0)
            {
                resourcesText.Append($"<color={colors['p']}>{FormatCurrencyKMB(p,"F0")}p</color> ");
            }

            resourcesText.Append($"<color={colors['g']}>{g}g</color> ");
            resourcesText.Append($"<color={colors['s']}>{s}s</color> ");

            if(p == 0)
            {
                resourcesText.Append($"<color={colors['c']}>{c}c</color>");
            }

            return resourcesText.ToString();
        }
    }
}
