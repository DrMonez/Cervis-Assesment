using Cervis.ClArc.Adapters.UI;

// This resolves a view type name to a matching *.resx file in the same folder
[assembly: LocalizationResolver(@"^Cervis\.MyApp\.Framework\.Views\.((?:[a-zA-Z0-9_]+\.)*)([a-zA-Z0-9_]+)$", "Cervis.MyApp.Framework.Views.$1$2")]    // does not support generic types atm

// This resolves global resources like "PageTitles" to the matching resource file in the Cervis.MyApp.Framework.Resources folder
[assembly: LocalizationResolver(@"^([a-zA-Z0-9_]+)$", "Cervis.MyApp.Framework.Resources.$1")]

// These resolve UI feature names to a matching *.resx file in the corresponding Blazor view folders
[assembly: LocalizationResolver(@"^Cervis\.MyApp\.Adapters\.UI\.((?:[a-zA-Z0-9_]+\.)*)([a-zA-Z0-9_]+)Presenter$", "Cervis.MyApp.Framework.Views.Blazor.$1$2")]         // does not support generic types atm
[assembly: LocalizationResolver(@"^Cervis\.MyApp\.Adapters\.UI\.((?:[a-zA-Z0-9_]+\.)*)([a-zA-Z0-9_]+)ViewController$", "Cervis.MyApp.Framework.Views.Blazor.$1$2")]    // does not support generic types atm
[assembly: LocalizationResolver(@"^Cervis\.MyApp\.Adapters\.UI\.((?:[a-zA-Z0-9_]+\.)*)([a-zA-Z0-9_]+)ViewModel$", "Cervis.MyApp.Framework.Views.Blazor.$1$2")]         // does not support generic types atm
