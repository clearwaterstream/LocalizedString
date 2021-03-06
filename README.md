# LocalizedString
A simple library that allows for fluent definition of a string and its various translations. When accessing a defined localized string, `Thread.CurrentThread.CurrentCulture` will be used to provide the relevant translated value. You can also request a value for a specific culture.

Useful for short strings. If strings are getting longer -- consider switching to time-tested technique of using resource files.

## Here's how easy it is to define and use a LocalizedString:

```csharp
var sampleString = new LocalizedString("chicken")
    .InCanadianEnglish("chicken, eh")
    .InCanadianFrench("éh poulet")
    .InFrench("poulet")
    .InQueensEnglish("hen")
    .In("de", "das bird");

// you can also do
sampleString["en-CA"] = "chicken, eh";
    
// this will output the value based on Thread.CurrentThread.CurrentCulture. If no value is found, Invariant value is used.
Console.WriteLine(sampleString.ToString()); 
```

When defining a localized string, invariant value must always be supplied. This is the same as for `string` type. Further to the invariant value, you are free to add translations for various languages.

If you would like to get a value for a particular locale, you can use

```csharp
sampleString.ToString("en-CA");

// or

sampleString["en-CA"];
```

If no suitable value is found for that locale, a value for `en` will be solicited. If no value is found, then invariant value will be returned.
