using Testmvc;
using Moq;
using Testmvc.Services;
using Testmvc.Controllers;
using Microsoft.Extensions.Logging;

namespace TestingTestMVC;


[TestClass]
public class UnitTest1
{
    private readonly ILogger<HomeController> _logger;

    [TestMethod]
    public void Compound_Interest_Calculator_HappyPath()
    {

        var sut = new MyCalculator();
        var resultant = sut.compute("blah", 10000, 10, 5);
        Assert.AreEqual(resultant, 16105.1);
    }

    [TestMethod]
    public void Compound_Interest_Calculator_SadPath()
    {
     
        var sut = new MyCalculator();
        var resultant = sut.compute("blah", 0, 10, 5);
        Assert.AreEqual(resultant, 0);
    }

    [TestMethod]
    public void Compound_Interest_Calculator_SadPath_NegativePrincipal()
    {

        var sut = new MyCalculator();
        var resultant = sut.compute("blah", -500, 10, 5);
        Assert.AreEqual(resultant, -.005);
    }

    [TestMethod]
    public void Compound_Interest_Calculator_SadPath_ZeroInterestRate()
    {

        var sut = new MyCalculator();
        var resultant = sut.compute("blah", -500, 0, 10);
        Assert.AreEqual(resultant, -500);
    }

    [TestMethod]
    public void Compound_Interest_Calculator_SadPath_NegativeYears()
    {

        var sut = new MyCalculator();
        var resultant = sut.compute("blah", 500, 10, -10);
        Assert.AreEqual(resultant, 192.77);
    }
}
