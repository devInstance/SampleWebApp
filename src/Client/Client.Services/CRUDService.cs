﻿using DevInstance.DevCoreApp.Shared.Model;
using DevInstance.BlazorUtils.Http;
using DevInstance.BlazorUtils.Services;
using DevInstance.DevCoreApp.Shared.Services;
using DevInstance.BlazorUtils.Services.Wasm;
using DevInstance.LogScope;

namespace DevInstance.DevCoreApp.Client.Services;

public class CRUDService<T> : BaseService, ICRUDService<T> where T : ModelItem
{
    IApiContext<T> Api { get; set; }

    public CRUDService(IApiContext<T> api)
    {
        Api = api;
    }

    public event DataUpdate<T> OnDataUpdate;

    public async Task<ServiceActionResult<T?>> GetAsync(string id)
    {
        using (var log = Log.TraceScope())
        {
            return await ServiceUtils.HandleWebApiCallAsync(log,
                async (l) =>
                {
                    var api = Api.Get(id);

                    return await api.ExecuteAsync();
                }
            );
        }
    }

    public void SetDataUpdate(T item)
    {
        OnDataUpdate?.Invoke(item);
    }

    public async Task<ServiceActionResult<T?>> AddNewAsync(T item)
    {
        using (var log = Log.TraceScope())
        {
            return await ServiceUtils.HandleWebApiCallAsync(log,
                async (l) =>
                {
                    var response = await Api.Post(item).ExecuteAsync();
                    if (response != null)
                    {
                        OnDataUpdate?.Invoke(response);
                    }
                    return response;
                }
            );
        }
    }

    public async Task<ServiceActionResult<T?>> UpdateAsync(T item)
    {
        using (var log = Log.TraceScope())
        {
            return await ServiceUtils.HandleWebApiCallAsync(log,
                async (l) =>
                {
                    var response = await Api.Put(item.Id, item).ExecuteAsync();
                    if (response != null)
                    {
                        OnDataUpdate?.Invoke(response);
                    }
                    return response;
                }
            );
        }
    }

    public async Task<ServiceActionResult<bool>> RemoveAsync(T item)
    {
        using (var log = Log.TraceScope())
        {
            return await ServiceUtils.HandleWebApiCallAsync(log,
                async (l) =>
                {
                    var response = await Api.Delete(item.Id).ExecuteAsync();
                    if (response != null)
                    {
                        OnDataUpdate?.Invoke(default(T));
                    }
                    return true;
                }
            );
        }
    }
}
