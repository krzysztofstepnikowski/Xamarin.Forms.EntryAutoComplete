# Xamarin.Forms.EntryAutoComplete ![Build status](https://img.shields.io/badge/nuget-1.0-blue.svg)

[Xamarin.Forms.EntryAutoComplete](https://github.com/krzysztofstepnikowski/Xamarin.Forms.EntryAutoComplete) is a custom control which functionality provides you with suggestions while typing. There are several modes of suggestions. The suggested text can be displayed in a drop-down list so that you can choose from different options.

## Features

* Custom search
* Filtering
* MaximumVisibleElements
* MinimumPrefixCharacter
* Watermark
* Clear button
* Style support for Android
* Style support for iOS

## Visuals

#### Android

![demo](https://github.com/krzysztofstepnikowski/Xamarin.Forms.EntryAutoComplete/blob/master/screenshots/Android.gif?raw=true)

#### iOS

![demo](https://github.com/krzysztofstepnikowski/Xamarin.Forms.EntryAutoComplete/blob/master/screenshots/iOS.gif?raw=true)

## Requirements


* Xamarin.Forms >= 3.1.0.697729

## Installation
Available as a **[NuGet package](https://www.nuget.org/packages/Xamarin.Forms.EntryAutoComplete/)**. 
```
Install-Package Xamarin.Forms.EntryAutoComplete -Version 1.0.0
```

## Usage

#### XAML

Reference the assembly namespace
```C#
xmlns:customControl="clr-namespace:EntryAutoComplete;assembly=EntryAutoComplete"
```



```C#
<customControl:EntryAutoComplete
            VerticalOptions="CenterAndExpand"
            Placeholder="Enter country..." 
            ItemsSource="{Binding Countries}"
            SearchText="{Binding SearchCountry}"
            SearchMode="{Binding SearchMode}"
            MaximumVisibleElements="5"/>
```

#### Code behind

```C#
var entryAutoComplete = new EntryAutoComplete
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Placeholder = "Enter country...",
                PlaceholderColor = Color.LightGray,
                MaximumVisibleElements = 5
            };
```


## Contributions

* [Krzysztof Stępnikowski](https://github.com/krzysztofstepnikowski)
* [Bartłomiej Rogowski](https://github.com/brogowski)

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.
