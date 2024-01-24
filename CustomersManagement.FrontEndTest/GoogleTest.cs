using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriverManager;

namespace CustomersManagement.FrontEndTest;

public class Tests
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
        

        _webDriver.Navigate().GoToUrl("https://www.google.com");
    }

    [Test]
    public void TestSearchInput()
    {
        IWebElement searchBox = _webDriver.FindElement(By.Name("q"));
        searchBox.SendKeys("go");
        searchBox.Submit();
        Thread.Sleep(2000);
        Assert.That(_webDriver.Title.Contains("go"));
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }
}
