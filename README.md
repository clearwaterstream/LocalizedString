# LocalizedString
A simple library that allows for fluent definition of a string and its various translations, fluently in code. When accessing a defined localized string, `Thread.CurrentThread.CurrentCulture` will be used to provide the relavant tanslated value. You can also request a value for a particular culture.

Useful for short strings. If strings are getting longer -- consider switching to time-tested technique of using resource files.

## Here's how it's easy to define and use a LocalizedString:

```csharp
var sampleString = new LocalizedString("chicken")
    .InCanadianEnglish("chicken, eh")
    .InCanadianFrench("éh poulet")
    .InFrench("poulet")
    .InQueensEnglish("hen")
    .In("zh", "Tso's parrot");
    
// this will output the value based on Thread.CurrentThread.CurrentCulture. If no value is found, Invariant value is used.
Console.WriteLine(sampleString.ToString()); 
```

When defining a localized string, invariant value must always be supplied. This is the same as for `string` type. Further to the invariant value, you are free to add translations for various languages.
