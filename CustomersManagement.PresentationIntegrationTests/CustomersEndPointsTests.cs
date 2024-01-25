using System.Net.Http;
using System.Text.Json;
using CustomersManagement.Application.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Reflection;
using CustomersManagement.Presentation.ApiModels;

namespace CustomersManagement.PresentationIntegrationTests;

[TestFixture]
public class CustomersEndPointsTests
{
    [Test]
    public async Task GetCustomersEndPoint_ShouldBeAsTheExpectedResponse_WhenCallIt()
    {
        WebApplicationFactory<Presentation.Program> factory = new WebApplicationFactory<CustomersManagement.Presentation.Program>().WithWebHostBuilder(builder => { });
        HttpClient client = factory.CreateClient();

        string data = this.ReadFromFile("data.json");
        Customer[]? expected = JsonConvert.DeserializeObject<Customer[]>(data);

        HttpResponseMessage response = await client.GetAsync("/customers");
        Customer[]? actual = JsonConvert.DeserializeObject<Customer[]>(await response.Content.ReadAsStringAsync());

        Assert.That(actual, !Is.EqualTo(null));
        Assert.That(expected, !Is.EqualTo(null));

        for(int i=  0; i < actual.Count(); i++)
        {
            Assert.That(actual[i].Id, Is.EqualTo(expected[i].Id));
            Assert.That(actual[i].CustomerId, Is.EqualTo(expected[i].CustomerId));
            Assert.That(actual[i].FirstName, Is.EqualTo(expected[i].FirstName));
            Assert.That(actual[i].LastName, Is.EqualTo(expected[i].LastName));
            Assert.That(actual[i].IsDeleted, Is.EqualTo(expected[i].IsDeleted));
        }
    }



    public string ReadFromFile(string filename)
    {
        var assemply = Assembly.GetExecutingAssembly();
        using (var stream = assemply.GetManifestResourceStream($"CustomersManagement.PresentationIntegrationTests.{filename}"))
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    [Test]
    public async Task InsertCustomerEndPoint_ShouldReturnTrue_WhenCallItWithACustomer()
    {
        WebApplicationFactory<Presentation.Program> factory = new WebApplicationFactory<CustomersManagement.Presentation.Program>().WithWebHostBuilder(builder => { });
        HttpClient client = factory.CreateClient();


        var data = new CreateCustomerRequest()
        {
            CustomerId = "98",
            FirstName = "Mohamad",
            LastName = "Alturky"
        };

        var json = System.Text.Json.JsonSerializer.Serialize(data);

        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/createCustomer", content);

        var responseString = await response.Content.ReadAsStringAsync();

        bool status = Boolean.Parse(responseString);
        
        Assert.That(status, Is.EqualTo(true));
    }
}