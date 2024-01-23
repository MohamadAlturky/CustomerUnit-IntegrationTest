using CustomersManagement.Application.Models;
using CustomersManagement.Application.Repositories;
using CustomersManagement.Application.Services;
using CustomersManagement.Infrastructure.DBContext;
using CustomersManagement.Infrastructure.RepositoriesImplemetation;
using CustomersManagement.Presentation.ApiModels;

namespace CustomersManagement.Presentation;
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<DataContext>();
        builder.Services.AddScoped<IEntityRepository<Customer>, EntityRepository<Customer>>();
        builder.Services.AddScoped(typeof(CustomerService));

        var app = builder.Build();



        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();


        app.MapGet("/customers", (CustomerService customerService) =>
        {
            return customerService.GetCustomers();
        })
        .WithName("GetCustomers")
        .WithOpenApi();


        app.MapPost("/createCustomer", (CreateCustomerRequest createCustomerRequest, CustomerService customerService) =>
        {
            return customerService.insertCustomer(new Customer()
            {
                CustomerId = createCustomerRequest.CustomerId,
                FirstName = createCustomerRequest.FirstName,
                LastName = createCustomerRequest.LastName,
                Id = 0,
                IsDeleted = false
            });
        })
        .WithName("CreateCustomer")
        .WithOpenApi();


        app.Run();
    }
}