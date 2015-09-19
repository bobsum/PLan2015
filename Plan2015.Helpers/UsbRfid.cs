using System.Text.RegularExpressions;

namespace Plan2015.Helpers
{
    public static class UsbRfid
    {
        private const string FORMAT = @"^\x02(\w{12})\x03\x03$"; //todo ret

        public static string Parse(string value)
        {
            if (value == null) return null;
            var match = Regex.Match(value, FORMAT);
            if (!match.Success) return null;

            return match.Groups[1].ToString();
        }
    }
}
