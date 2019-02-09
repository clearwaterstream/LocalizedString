using System;
using System.Collections.Generic;
using System.Text;

namespace clearwaterstream
{
    public static class LocalizedStringExtensions
    {
        public static LocalizedString Localize(this string invariantValue)
        {
            return new LocalizedString(invariantValue);
        }

        public static T In<T>(this T localizedString, string cultureName, string localizedValue) where T : LocalizedString
        {
            if (localizedString == null)
                return localizedString;

            localizedString[cultureName] = localizedValue;

            return localizedString;
        }

        public static T Invariant<T>(this T localizedString, string invariantValue) where T : LocalizedString
        {
            return In(localizedString, string.Empty, invariantValue);
        }

        public static T InEnglish<T>(this T localizedString, string localizedValue) where T : LocalizedString
        {
            return In(localizedString, "en-US", localizedValue);
        }

        public static T InQueensEnglish<T>(this T localizedString, string localizedValue) where T : LocalizedString
        {
            return In(localizedString, "en-GB", localizedValue);
        }

        public static T InCanadianEnglish<T>(this T localizedString, string localizedValue) where T : LocalizedString
        {
            return In(localizedString, "en-CA", localizedValue);
        }

        public static T InCanadianFrench<T>(this T localizedString, string localizedValue) where T : LocalizedString
        {
            return In(localizedString, "fr-CA", localizedValue);
        }

        public static T InFrench<T>(this T localizedString, string localizedValue) where T : LocalizedString
        {
            return In(localizedString, "fr-FR", localizedValue);
        }
    }
}
