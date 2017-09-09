
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace RFO.Common.Utilities.StringHelper
{
    /// <summary>
    /// Provides methods to do operations related to a string like customization or matching checking, etc.
    /// </summary>
    public static class StringHelper
    {
        #region Implementation of IStringHelper

        /// <summary>
        /// TrimStart input string with specified characters
        /// </summary>
        /// <param name="s">Input string</param>
        /// <param name="trimChars">Specified characters</param>
        /// <returns>String has been trimed</returns>
        public static string TrimStart(string s, params char[] trimChars)
        {
            return s.TrimStart(trimChars);
        }

        /// <summary>
        /// TrimEnd input string with specified characters
        /// </summary>
        /// <param name="s">Input string</param>
        /// <param name="trimChars">Specified characters</param>
        /// <returns>String has been trimed</returns>
        public static string TrimEnd(string s, params char[] trimChars)
        {
            return s.TrimEnd(trimChars);
        }

        /// <summary>
        /// Trim input string with specified characters
        /// </summary>
        /// <param name="s">Input string</param>
        /// <param name="trimChars">Specified characters</param>
        /// <returns>String has been trimed</returns>
        public static string Trim(string s, params char[] trimChars)
        {
            return s.Trim(trimChars);
        }

        /// <summary>
        /// Truncate a string with specified length
        /// </summary>
        /// <param name="s">Input string</param>
        /// <param name="length">Specified length</param>
        /// <returns>String has been truncated</returns>
        public static string Truncate(string s, int length)
        {
            var result = s;
            if (s.Length > length)
            {
                result = "...";
                s = s.Substring(0, length);
                var lastPos = s.LastIndexOf(" ", StringComparison.Ordinal);
                if (lastPos != -1)
                {
                    result = s.Substring(0, lastPos) + "...";
                }
            }
            return result;
        }

        /// <summary>
        /// To upper first character
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>String has been made upper</returns>
        public static string ToUpperFirstCharacter(string s)
        {
            return string.IsNullOrEmpty(s) ? s : (s[0].ToString().ToUpper() + s.Substring(1));
        }

        /// <summary>
        /// To lower first character
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>String has been made lower</returns>
        public static string ToLowerFirstCharacter(string s)
        {
            return string.IsNullOrEmpty(s) ? s : (s[0].ToString().ToLower() + s.Substring(1));
        }

        /// <summary>
        /// Remove specified characters
        /// </summary>
        /// <param name="s">Input string</param>
        /// <param name="chars">Invalid characters</param>
        /// <returns>String has been removed invalid characters</returns>
        public static string RemoveInvalidChars(string s, params char[] chars)
        {
            var newString = new StringBuilder();

            if (!string.IsNullOrEmpty(s))
            {
                foreach (char ch in s)
                {
                    if (chars.Contains(ch))
                    {
                        newString.Append(ch);
                    }
                }
            }

            return newString.ToString();
        }

        /// <summary>
        /// Remove unicode characters
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>String has been removed unicode characters</returns>
        public static string RemoveUnicodeChars(string s)
        {
            char[] arrChar = {'a', 'A', 'd', 'D', 'e', 'E', 'i', 'I', 'o', 'O', 'u', 'U', 'y', 'Y'};
            char[][] uniChar =
            {
                new char[] {'á', 'à', 'ả', 'ã', 'ạ', 'â', 'ấ', 'ầ', 'ẩ', 'ẫ', 'ậ', 'ă', 'ắ', 'ằ', 'ẳ', 'ẵ', 'ặ'},
                new char[] {'Á', 'À', 'Ả', 'Ã', 'Ạ', 'Â', 'Ấ', 'Ầ', 'Ẩ', 'Ẫ', 'Ă', 'Ặ', 'Ắ', 'Ằ', 'Ẳ', 'Ẵ', 'Ặ'},
                new char[] {'đ'},
                new char[] {'Đ'},
                new char[] {'é', 'è', 'ẻ', 'ẽ', 'ẹ', 'ê', 'ế', 'ề', 'ể', 'ễ', 'ệ'},
                new char[] {'É', 'È', 'Ẻ', 'Ẽ', 'Ẹ', 'Ê', 'Ế', 'Ề', 'Ể', 'Ễ', 'Ệ'},
                new char[] {'í', 'ì', 'ỉ', 'ĩ', 'ị'},
                new char[] {'Í', 'Ì', 'Ỉ', 'Ĩ', 'Ị'},
                new char[] {'ó', 'ò', 'ỏ', 'õ', 'ọ', 'ô', 'ố', 'ồ', 'ổ', 'ỗ', 'ộ', 'ơ', 'ớ', 'ờ', 'ỡ', 'ợ'},
                new char[] {'Ó', 'Ò', 'Ỏ', 'Õ', 'Ọ', 'Ô', 'Ố', 'Ồ', 'Ổ', 'Ỗ', 'Ộ', 'Ơ', 'Ớ', 'Ờ', 'Ỡ', 'Ợ'},
                new char[] {'ú', 'ù', 'ủ', 'ũ', 'ụ', 'ư', 'ứ', 'ừ', 'ử', 'ữ', 'ự'},
                new char[] {'Ú', 'Ù', 'Ủ', 'Ũ', 'Ụ', 'Ư', 'Ứ', 'Ừ', 'Ử', 'Ữ', 'Ự'},
                new char[] {'ý', 'ỳ', 'ỷ', 'ỹ', 'ỵ'},
                new char[] {'Ý', 'Ỳ', 'Ỷ', 'Ỹ', 'Ỵ'}
            };

            for (var i = 0; i < uniChar.Length; i++)
            {
                for (var j = 0; j < uniChar[i].Length; j++)
                {
                    s = s.Replace(uniChar[i][j], arrChar[i]);
                }
            }

            if (s[s.Length - 1].Equals('_'))
            {
                s = s.Substring(0, s.Length - 1);
            }

            return s;
        }

        /// <summary>
        /// Creates the URL friendly.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string CreateUrlFriendly(string s)
        {
            s = s.ToLower().Trim();
            s = RemoveUnicodeChars(s);

            s = s.Replace(" - ", " ");

            s = s.Replace("– ", " ");
            s = s.Replace("+ ", " ");
            s = s.Replace(". ", " ");
            s = s.Replace(", ", " ");
            s = s.Replace("; ", " ");
            s = s.Replace(": ", " ");
            s = s.Replace("? ", " ");
            s = s.Replace("! ", " ");
            s = s.Replace("“ ", " ");
            s = s.Replace("” ", " ");

            s = s.Replace("–", "-");
            s = s.Replace("+", string.Empty);
            s = s.Replace(".", string.Empty);
            s = s.Replace(",", "-");
            s = s.Replace(";", "-");
            s = s.Replace(":", "-");
            s = s.Replace("?", "-");
            s = s.Replace("!", "-");
            s = s.Replace("“", "-");
            s = s.Replace("”", "-");
            s = s.Replace("&", "-");

            s = s.Replace(" ", "-");
            s = s.Replace("\t", "-");

            while (s.Contains("__"))
            {
                s = s.Replace("__", "-");
            }

            if (s[s.Length - 1].Equals('_'))
            {
                s = s.Substring(0, s.Length - 1);
            }

            return s;
        }

        /// <summary>
        /// Remove control characters
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>String has been removed control characters</returns>
        public static string RemoveControlChars(string s)
        {
            var newString = new StringBuilder();

            if (!string.IsNullOrEmpty(s))
            {
                foreach (char ch in s)
                {
                    if (!char.IsControl(ch))
                    {
                        newString.Append(ch);
                    }
                }
            }

            return newString.ToString();
        }

        /// <summary>
        /// Dump the current collection to string.
        /// </summary>
        /// <typeparam name="T">
        /// collection parameter.
        /// </typeparam>
        /// <param name="collection">
        /// The collection parameter.
        /// </param>
        /// <returns>
        /// The to strings.
        /// </returns>
        public static string ToStrings<T>(ICollection<T> collection)
        {
            var strItems = Array.ConvertAll(collection.ToArray(), item => item.ToString());
            return string.Format("{0}: [{1}]", collection.GetType().FullName, string.Join(",", strItems));
        }

        /// <summary>
        /// Dump the given assembly collection to string.
        /// </summary>
        /// <param name="assemblies">Collection of Assembly</param>
        /// <returns>string type .</returns>
        public static string AssembliesToString(ICollection<Assembly> assemblies)
        {
            var assNames = Array.ConvertAll(assemblies.ToArray(), ass => ass.GetName().Name);
            return string.Format("{0}: [{1}]", assemblies.GetType().Name, string.Join(",", assNames));
        }

        /// <summary>
        /// Dump the given type collection to string.
        /// </summary>
        /// <param name="types">Collection of Type .</param>
        /// <returns>string type.</returns>
        public static string TypesToString(ICollection<Type> types)
        {
            var typeNames = Array.ConvertAll(types.ToArray(), t => t.FullName);
            return string.Format("{0}: [{1}]", types.GetType().Name, string.Join(",", typeNames));
        }

        /// <summary>
        /// The wildcard to regex.
        /// </summary>
        /// <param name="pattern">
        /// The pattern.
        /// </param>
        /// <returns>
        /// The System.String.
        /// </returns>
        public static string WildcardToPattern(string pattern)
        {
            return "^" + Regex.Escape(pattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$";
        }

        /// <summary>
        /// The wildcard to regex.
        /// </summary>
        /// <param name="filterText">
        /// The filter text.
        /// </param>
        /// <returns>
        /// The System.Text.RegularExpressions.Regex.
        /// </returns>
        public static Regex WildcardToRegex(string filterText)
        {
            var ret = new Regex(WildcardToPattern(filterText), RegexOptions.IgnoreCase);
            return ret;
        }

        /// <summary>
        /// Splits the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="maxCharacterInLine">The maximum character in line.</param>
        /// <param name="minCharacterInLine">The minimum character in line.</param>
        /// <returns></returns>
        public static string[] SplitString(string text, int maxCharacterInLine, int minCharacterInLine)
        {
            var result = new List<string>();
            var random = new Random(Environment.TickCount);
            text = text.Trim();

            while (!string.IsNullOrEmpty(text))
            {
                var numWordInLineRand = random.Next(minCharacterInLine, maxCharacterInLine + 1);
                var subPreText = string.Empty;
                var nextPos = FindCharacterPosition(text, " ", numWordInLineRand);
                if (nextPos != -1)
                {
                    subPreText = text.Substring(0, nextPos);
                }
                else
                {
                    if (!string.IsNullOrEmpty(text))
                    {
                        subPreText = text;
                    }
                }

                result.Add(subPreText);
                text = text.Remove(0, subPreText.Length).Trim();
            }

            return result.ToArray();
        }

        /// <summary>
        /// Finds the character position.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        private static int FindCharacterPosition(string text, string pattern, int size)
        {
            var result = -1;
            var count = 0;

            while (!string.IsNullOrEmpty(text))
            {
                var nextPos = text.IndexOf(pattern, 0);
                if (nextPos != -1)
                {
                    text = text.Substring(nextPos + 1);
                    // The rest of string does not have string matching with specified pattern
                    if (text.IndexOf(pattern) == -1) 
                    {
                        result = -1;
                        break;
                    }
                    result += (nextPos + 1);
                    count++;
                    if (count == size)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        #endregion
    }
}