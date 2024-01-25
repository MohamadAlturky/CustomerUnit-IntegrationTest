using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriverManager;

namespace CustomersManagement.FrontEndTest;

public class SomeSiteTest
{
    private IWebDriver _webDriver;
    [SetUp]
    public void Setup()
    {

        FirefoxDriverService service =
            FirefoxDriverService
            .CreateDefaultService(@"C:\Users\mohamad\Downloads\geckodriver-v0.34.0-win64", "geckodriver.exe");


        service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";

        _webDriver = new FirefoxDriver(service);


        _webDriver.Navigate().GoToUrl("http://altoro.testfire.net/login.jsp");
    }

    [Test]
    public void InValidCredenials_ShouldReturnFalse()
    {
        IWebElement searchBox1 = _webDriver.FindElement(By.Name("uid"));
        searchBox1.SendKeys("admin");
        IWebElement searchBox2 = _webDriver.FindElement(By.Name("passw"));
        searchBox2.SendKeys("admin2");

        IWebElement logIn = _webDriver.FindElement(By.Name("btnSubmit"));
        logIn.Click();

        IWebElement Error = _webDriver.FindElement(By.Id("_ctl0__ctl0_Content_Main_message"));
        Assert.That(Error.Displayed);
        Assert.That(Error.Text.Contains("Login Failed: We're sorry, but this username or password was not found in our system. Please try again."));
    }

    [Test]
    public void ValidCredenials_ShouldReturnTrue()
    {
        TearDown();
        Setup();
        IWebElement searchBox1 = _webDriver.FindElement(By.Name("uid"));
        searchBox1.SendKeys("admin");
        IWebElement searchBox2 = _webDriver.FindElement(By.Name("passw"));
        searchBox2.SendKeys("admin");
        searchBox2.Submit();
        IWebElement logIn = _webDriver.FindElement(By.Name("btnSubmit"));
        logIn.Click();

        var elements = _webDriver.FindElements(By.TagName("h1"));
        
        bool thereIsHelloAdmin = false;
        
        foreach(var element in elements)
        {
            if (element.Text.Contains("Hello Admin User"))
            {
                thereIsHelloAdmin = true;
            }
        }

        
        Assert.That(thereIsHelloAdmin,Is.EqualTo(true));
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }
}
