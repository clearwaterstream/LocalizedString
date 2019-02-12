/* Igor Krupin
 * https://github.com/clearwaterstream/LocalizedString
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace clearwaterstream
{
    public class LocalizedString : IEquatable<string>, IComparable, IComparable<string>, ICloneable
    {
        static readonly string invariantCultureName = string.Empty;
        readonly Dictionary<string, string> valueByCulture = new Dictionary<string, string>();

        public static readonly LocalizedString Empty = new LocalizedString();

        /// <summary>
        /// Initializes a new instance of <see cref="LocalizedString"/> class
        /// </summary>
        /// <param name="invariantValue">Invariant value for the string. This is the "default" value, same as for a regular <see cref="String"></see> type</param>
        public LocalizedString(string invariantValue)
        {
            this[invariantCultureName] = invariantValue;
        }

        private LocalizedString() { }

        /// <summary>
        /// Returns a value in the specified culture, if such value exists. Otherwise an invariant value is returned.
        /// </summary>
        /// <param name="cultureName">Culture name, such as "en-CA"</param>
        public string this[string cultureName]
        {
            get
            {
                if (valueByCulture.Count == 0)
                    return null;

                if (cultureName == null)
                    cultureName = invariantCultureName;

                if (valueByCulture.TryGetValue(cultureName, out string localizedValue))
                    return localizedValue;

                // if there is no value for fr-CA say, see if have anything for jusr fr
                if (cultureName.Contains("-"))
                {
                    var langCode = cultureName.Split('-')[0];

                    if (valueByCulture.TryGetValue(langCode, out string byLangCode))
                        return byLangCode;
                }
                
                // if we cannot find the value based on the requested culture, fallback to invariant cultrue
                if (valueByCulture.TryGetValue(invariantCultureName, out string invariantValue))
                    return invariantValue;

                return null;
            }
            set
            {
                if (cultureName == null)
                    cultureName = invariantCultureName;

                valueByCulture[cultureName] = value;
            }
        }

        public string this[CultureInfo cultureInfo]
        {
            get
            {
                return this[cultureInfo?.Name];
            }
            set
            {
                this[cultureInfo?.Name] = value;
            }
        }

        /// <summary>
        /// Returns a value according to <see cref="Thread.CurrentThread.CurrentCulture"/> value
        /// </summary>
        public override string ToString()
        {
            return ToString(Thread.CurrentThread.CurrentCulture);
        }

        public string ToString(CultureInfo culture)
        {
            return this[culture?.Name];
        }

        public string ToString(string cultureName)
        {
            return this[cultureName];
        }

        public override bool Equals(object obj)
        {
            if (obj is string)
                return Equals(this, (string)obj);

            if (obj is LocalizedString)
                return Equals(this, ((LocalizedString)obj)?.ToString());

            return false;
        }

        public static bool Equals(LocalizedString a, string b)
        {
            if (ReferenceEquals(a, b))
                return true;

            return string.Equals(a?.ToString(), b);
        }

        public static bool Equals(LocalizedString a, string b, StringComparison comparisonType)
        {
            if (ReferenceEquals(a, b))
                return true;

            return string.Equals(a?.ToString(), b, comparisonType);
        }

        public bool Equals(string other)
        {
            return Equals(this, other);
        }

        public bool Equals(string other, StringComparison comparisonType)
        {
            return Equals(this, other, comparisonType);
        }

        public int CompareTo(string other)
        {
            return string.Compare(ToString(), other);
        }

        public int CompareTo(object obj)
        {
            if (obj is string)
                return string.Compare(ToString(), (string)obj);

            if(obj is LocalizedString)
                return string.Compare(ToString(), ((LocalizedString)obj)?.ToString());

            return 1;
        }

        public override int GetHashCode()
        {
            var invariantValue = this[invariantCultureName];

            if (invariantValue == null)
                return Empty.GetHashCode();

            return invariantValue.GetHashCode();
        }

        public object Clone()
        {
            var newInst = new LocalizedString();

            foreach(var kvp in valueByCulture)
            {
                newInst.valueByCulture.Add(kvp.Key, kvp.Value);
            }

            return newInst;
        }

        public static explicit operator string(LocalizedString localizedString)
        {
            return localizedString?.ToString();
        }

        public static explicit operator LocalizedString(string invariantValue)
        {
            return new LocalizedString(invariantValue);
        }

        public static bool operator ==(LocalizedString a, string b)
        {
            return string.Equals(a?.ToString(), b);
        }

        public static bool operator !=(LocalizedString a, string b)
        {
            return !string.Equals(a?.ToString(), b);
        }
    }
}
