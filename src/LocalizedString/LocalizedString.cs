/* Igor Krupin
 * https://github.com/clearwaterstream/LocalizedString
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace clearwaterstream
{
    public class LocalizedString : IEquatable<LocalizedString>, IEquatable<string>, IComparable, IComparable<LocalizedString>, IComparable<string>, ICloneable
    {
        static readonly string _defaultCultureName = string.Empty; // invariant
        readonly Dictionary<string, string> valueByCulture = new Dictionary<string, string>();

        public LocalizedString(string localizedValue)
        {
            this[_defaultCultureName] = localizedValue;
        }

        public LocalizedString(string cultureName, string localizedValue)
        {
            if (string.IsNullOrEmpty(cultureName))
                cultureName = _defaultCultureName;

            this[cultureName] = localizedValue;
        }

        private LocalizedString() { }

        public string this[string cultureName]
        {
            get
            {
                if (cultureName == null)
                    cultureName = _defaultCultureName;

                if (valueByCulture.TryGetValue(cultureName, out string localizedValue))
                    return localizedValue;
                // if we cannot find the value based on the requested culture, try to fallback on the default culture ...
                else if (valueByCulture.TryGetValue(_defaultCultureName, out string fallback))
                    return fallback;
                else
                    return null;
            }
            set
            {
                if (cultureName == null)
                    cultureName = _defaultCultureName;

                valueByCulture[cultureName] = value;
            }
        }

        public override string ToString()
        {
            return ToString(Thread.CurrentThread.CurrentCulture);
        }

        public string ToString(CultureInfo culture)
        {
            var cultureName = culture?.Name ?? null;

            return this[cultureName];
        }

        public string ToString(string cultureName)
        {
            return this[cultureName];
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return Equals(this, null);

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

        public bool Equals(LocalizedString other)
        {
            return Equals(this, other?.ToString());
        }

        public bool Equals(LocalizedString other, StringComparison comparisonType)
        {
            return Equals(this, other?.ToString(), comparisonType);
        }

        public int CompareTo(string other)
        {
            return string.Compare(ToString(), other);
        }

        public int CompareTo(LocalizedString other)
        {
            return string.Compare(ToString(), other?.ToString());
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return string.Compare(ToString(), null);

            if (obj is string)
                return string.Compare(ToString(), (string)obj);

            if(obj is LocalizedString)
                return string.Compare(ToString(), ((LocalizedString)obj)?.ToString());

            return 1;
        }

        public override int GetHashCode()
        {
            var str = ToString();

            if (str == null)
                return string.Empty.GetHashCode();

            return str.GetHashCode();
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
