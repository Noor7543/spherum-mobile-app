using System.Globalization;

namespace Spherum.Mobile.Converters;

public sealed class GenderToImageSourceConverter : IValueConverter
{
    const string DefaultImageSource = "icon_male";

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string gender)
        {
            return DefaultImageSource;
        }

        return gender switch
        {
            "Male" => DefaultImageSource,
            "Female" => "icon_female",
            "Non-binary" => "icon_binary_gender",
            _ => DefaultImageSource
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}