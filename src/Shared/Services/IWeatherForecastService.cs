﻿
using DevInstance.BlazorToolkit.Model;
using DevInstance.BlazorToolkit.Services;
using DevInstance.DevCoreApp.Shared.Model;

namespace DevInstance.DevCoreApp.Shared.Services;

public enum WeatherForecastFields
{
    Unknown,
    Date,
    Temperature,
    Summary
}

public static class WeatherForecastFieldsExtensions
{
    public static string ToFieldName(this WeatherForecastFields field)
    {
        return field switch
        {
            WeatherForecastFields.Date => "Date",
            WeatherForecastFields.Temperature => "Temperature",
            WeatherForecastFields.Summary => "Summary",
            _ => "Unknown",
        };
    }

    public static WeatherForecastFields FromFieldName(this string field)
    {
        return field switch
        {
            "Date" => WeatherForecastFields.Date,
            "Temperature" => WeatherForecastFields.Temperature,
            "Summary" => WeatherForecastFields.Summary,
            _ => WeatherForecastFields.Unknown,
        };
    }
}

public interface IWeatherForecastService : ICRUDService<WeatherForecastItem>
{
    Task<ServiceActionResult<ModelList<WeatherForecastItem>?>> GetItemsAsync(int? top, int? page, WeatherForecastFields? sortBy, bool? isAsc, string? search);

}
