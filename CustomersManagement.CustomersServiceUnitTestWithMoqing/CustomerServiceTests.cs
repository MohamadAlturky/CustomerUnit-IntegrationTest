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

    [Test]
    public void GetCustomers_ShouldReturnAnEmptyListOfCustomers_WhenAllIsDeleted()
    {
        Mock<IEntityRepository<Customer>> mock = new Mock<IEntityRepository<Customer>>();

        _customers = new List<Customer>
        {
            new Customer()
            {
                CustomerId = "Hi",
                FirstName = "Mohamad",
                LastName = "Alturky",
                Id = 4,
                IsDeleted = true
            }
        };
        IQueryable<Customer> queryable = _customers.AsQueryable();
        mock.Setup(repo => repo.GetAllQueryable()).Returns(queryable);


        CustomerService service = new CustomerService(mock.Object);

        List<Customer> customers = service.GetCustomers();

        Assert.That(customers, !Is.EqualTo(null));
        Assert.That(customers.Count, Is.EqualTo(0));
    }

    [Test]
    public void InsertCustomer_SouldReturnTrue_WhenCallingItWithCustomerObject()
    {
        Mock<IEntityRepository<Customer>> mock = new Mock<IEntityRepository<Customer>>();
        Customer customer = new Customer()
        {

        };

        mock.Setup(r => r.Insert(customer));
        CustomerService service = new CustomerService(mock.Object);

        bool status = service.insertCustomer(customer);

        Assert.That(status, Is.EqualTo(true));
        mock.Verify(r => r.Insert(customer), Times.Once);

    }

    [Test]
    public void InsertCustomer_SouldReturnFalse_WhenExceptionOccured()
    {
        Mock<IEntityRepository<Customer>> mock = new Mock<IEntityRepository<Customer>>();
        Customer customer = new Customer()
        {

        };

        mock.Setup(r => r.Insert(customer)).Throws(new InvalidOperationException("Insert failed")); ;
        CustomerService service = new CustomerService(mock.Object);

        bool status = service.insertCustomer(customer);

        Assert.That(status, Is.EqualTo(false));
    }
}