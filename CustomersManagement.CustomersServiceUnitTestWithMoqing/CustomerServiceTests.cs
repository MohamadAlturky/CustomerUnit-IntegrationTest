using CustomersManagement.Application.Models;
using CustomersManagement.Application.Repositories;
using CustomersManagement.Application.Services;
using Moq;
using NUnit.Framework;

namespace CustomersManagement.CustomersServiceUnitTestWithMoqing;

[TestFixture]
public class CustomerServiceTests
{
    private Mock<IEntityRepository<Customer>> _mock;
    private List<Customer> _customers;
    [SetUp]
    public void SetUp()
    {
        _mock = new Mock<IEntityRepository<Customer>>();
        _customers = new List<Customer>
        {
            new Customer()
            {
                CustomerId = "Hi",
                FirstName = "Mohamad",
                LastName = "Alturky",
                Id = 4,
                IsDeleted = false
            }
        };
        IQueryable<Customer> queryable = _customers.AsQueryable();
        _mock.Setup(repo => repo.GetAllQueryable()).Returns(queryable);
    }

    [Test]
    public void GetCustomers_ShouldReturnAListOfCustomers_WhenCallingIt()
    {
        CustomerService service = new CustomerService(_mock.Object);

        List<Customer> customers = service.GetCustomers();

        Assert.That(customers, !Is.EqualTo(null));
        Assert.That(customers.Count, !Is.EqualTo(0));
        
        for (int i = 0; i < customers.Count;i++)
        {
            Assert.That(customers[i].Id, Is.EqualTo(_customers[i].Id));
            Assert.That(customers[i].CustomerId, Is.EqualTo(_customers[i].CustomerId));
            Assert.That(customers[i].FirstName, Is.EqualTo(_customers[i].FirstName));
            Assert.That(customers[i].LastName, Is.EqualTo(_customers[i].LastName));
            Assert.That(customers[i].IsDeleted, Is.EqualTo(_customers[i].IsDeleted));
        }

    }
}