using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Um416.BLL.Tools
{
    public static class StringExtensions
    {
        public static string ToHashtag(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            var textNormalize =  stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            return Regex.Replace(textNormalize, @"[^\d\w]+", "");
        }
    }
}
