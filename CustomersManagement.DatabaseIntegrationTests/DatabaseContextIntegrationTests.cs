using CustomersManagement.Application.Models;
using CustomersManagement.Infrastructure.DBContext;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Reflection;

namespace CustomersManagement.DatabaseIntegrationTests;

[TestFixture]
public class DatabaseContextIntegrationTests
{
    private DataContext _dbContext;
    [SetUp]
    public void SetUp()
    {
        WebApplicationFactory<Presentation.Program> factory = new WebApplicationFactory<CustomersManagement.Presentation.Program>().WithWebHostBuilder(builder => { });

        IServiceScope scope = factory.Services.CreateScope();
        _dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    }

    [Test]
    public async Task GetCustomers_ShouldReturnAListOfCustomers()
    {
        string data = this.ReadFromFile("data.json");

        Customer[] actual = _dbContext.Customers.ToArray();
        Customer[]? expected = JsonConvert.DeserializeObject<Customer[]>(data);

        Assert.That(expected, !Is.EqualTo(null));
        Assert.That(actual, !Is.EqualTo(null));


        for (int i = 0; i < actual.Count(); i++)
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
        using (var stream = assemply.GetManifestResourceStream($"CustomersManagement.DatabaseIntegrationTests.{filename}"))
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}