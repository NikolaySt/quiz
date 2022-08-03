using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuizGame.Client.Models;

namespace QuizGame.Client;

public abstract class SuperClient
{
    public readonly Uri _baseUri;
    public readonly HttpClient _httpClient;

    protected SuperClient(Uri baseUri, HttpClient httpClient)
    {
        _baseUri = baseUri;
        _httpClient = httpClient;
    }

    public static async Task<Response<T>> GetResponse<T>(HttpResponseMessage httpResponse, HttpStatusCode expectedStatus = HttpStatusCode.OK)
    {
        return httpResponse.StatusCode == expectedStatus ?
            new Response<T>(httpResponse.StatusCode, await ReadAndDeserializeAsync<T>(httpResponse)) :
            new Response<T>(httpResponse.StatusCode, default, await ReadErrorAsync(httpResponse));
    }

    public static async Task<Response<T>> GetResponse<T>(HttpResponseMessage httpResponse, T value, HttpStatusCode expectedStatus = HttpStatusCode.OK)
    {
        return httpResponse.StatusCode == expectedStatus ?
            new Response<T>(httpResponse.StatusCode, value) :
            new Response<T>(httpResponse.StatusCode, default, await ReadErrorAsync(httpResponse));
    }

    protected async Task<HttpResponseMessage> ExecuteAsync(
        HttpMethod method,
        string relativeUri,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync(method, relativeUri, null, cancellationToken);
    }

    protected async Task<HttpResponseMessage> ExecuteAsync(
        HttpMethod method,
        string relativeUri,
        object? content = null,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(method, new Uri(_baseUri, relativeUri));
        if (content != null)
        {
            request.Content = new StringContent(JsonConvert.SerializeObject(content));
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
        }
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response = await _httpClient.SendAsync(request, cancellationToken);
        return response;
    }

    public static async Task<string> ReadErrorAsync(HttpResponseMessage response)
    {
        return await response.Content.ReadAsStringAsync();
    }

    private static async Task<T> ReadAndDeserializeAsync<T>(HttpResponseMessage response)
    {
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }
}