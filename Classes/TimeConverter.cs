using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public const string yellowLight = "Y";
        public const string redLight = "R";
        public const string offLight = "O";


        public const string firstLine = "RRRR";
        public const string secondLine = "RRRR";
        public const string thirdLine = "YYRYYRYYRYY";
        public const string fourthLine = "YYYY";

        StringBuilder berlinClockSb = new StringBuilder();

        public string convertTime(string aTime)
        {
            berlinClockSb.Clear();

            var hours = Int32.Parse(aTime.Substring(0, 2));
            var minutes = Int32.Parse(aTime.Substring(3, 2));
            var seconds = Int32.Parse(aTime.Substring(6, 2));

            ProcessClockLampsLines(hours, minutes, seconds);

            return berlinClockSb.ToString();
        }

        private void ProcessClockLampsLines(int hours, int minutes, int seconds)
        {
            berlinClockSb.AppendLine(seconds % 2 == 1 ? offLight : yellowLight);
            berlinClockSb.AppendLine(GetClockLampsLine(firstLine, hours / 5));
            berlinClockSb.AppendLine(GetClockLampsLine(secondLine, hours % 5));
            berlinClockSb.AppendLine(GetClockLampsLine(thirdLine, minutes / 5));
            berlinClockSb.Append(GetClockLampsLine(fourthLine, minutes % 5));
        }

        private string GetClockLampsLine(string line, int onLights)
        {
            Regex rgx = new Regex(".");
            return rgx.Replace(line, offLight, line.Length - onLights, onLights);
        }
    }
}
