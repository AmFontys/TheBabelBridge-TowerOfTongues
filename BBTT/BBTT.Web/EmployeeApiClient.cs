using BBTT.CrosswordModel;
using BBTT.Web.Components.Pages;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace BBTT.Web;

/// <summary>
/// The Api client for the Employee functionality
/// </summary>
public class EmployeeApiClient
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="CrossWordApiClient"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client derived from the settings set in <see cref="Program"/> class.</param>
    public EmployeeApiClient (HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = TimeSpan.FromMinutes(5); // Increase timeout to 5 minutes
    }

        
    public async Task<object> Post (object something)
    {
        var response = await _httpClient.PostAsJsonAsync("/Employee", something);
        response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful        
        object? result = await response.Content.ReadFromJsonAsync<object>();
        //TODO: Check on null failure
        return result;

    }
    

}
