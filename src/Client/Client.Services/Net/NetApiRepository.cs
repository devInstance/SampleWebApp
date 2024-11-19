﻿using DevInstance.DevCoreApp.Shared.Model;
using DevInstance.BlazorUtils.Http;
using Microsoft.AspNetCore.Components;
using DevInstance.DevCoreApp.Client.Services.Net.Api;
using System.Net.Http;

namespace DevInstance.DevCoreApp.Client.Services.Net;

public class NetApiRepository : INetApiRepository
{
    private readonly IHttpClientFactory httpFactory;

    public NetApiRepository(IHttpClientFactory factory, NavigationManager navigationManager)
    {
        httpFactory = factory;
        //httpFactory.BaseAddress = new Uri(navigationManager.BaseUri);
    }

    HttpClient HttpClient => httpFactory.CreateClient("DevInstance.DevCoreApp.ServerAPI");
    public IApiContext<WeatherForecastItem> GetWeatherForecastApi()
    {
        return HttpApiContextFactory.Create<WeatherForecastItem>(HttpClient, "api/forecast");
    }
}
