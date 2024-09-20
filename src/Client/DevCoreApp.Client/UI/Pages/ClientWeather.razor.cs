﻿using DevInstance.DevCoreApp.Client.Services.Api;
using DevInstance.DevCoreApp.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace DevInstance.DevCoreApp.Client.UI.Pages;

public partial class ClientWeather
{
    [Inject]
    IWeatherForecastService Service { get; set; }

    private const int PageSize = 15;

    private ModelList<WeatherForecastItem> forecasts;

    private WeatherForecastItem selectedForecast;

    protected override async Task OnInitializedAsync()
    {
        await RequestDataAsync(0);

        Service.OnDataUpdate += Service_OnDataUpdate;
    }

    private async Task Service_OnDataUpdate(WeatherForecastItem item)
    {
        await RequestDataAsync(0);
    }

    protected async Task RequestDataAsync(int page)
    {
        await ServiceCallAsync(() => Service.GetItemsAsync(PageSize, page, null, null, null), (a) => { forecasts = a; });
    }

    private async Task Remove(WeatherForecastItem item)
    {
        await ServiceCallAsync(() => Service.RemoveAsync(item), null, async (a) => { await RequestDataAsync(forecasts.Page); });
    }

    private async Task SortBy(string sortBy)
    {
        await ServiceCallAsync(() => Service.GetItemsAsync(PageSize, forecasts?.Page ?? 0, null, null, null), (a) => { forecasts = a; });
    }
}
