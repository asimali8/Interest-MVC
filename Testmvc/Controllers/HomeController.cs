using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Testmvc.Models;
using Newtonsoft.Json;
using Testmvc.Services;
//using RApplicationDbContext;

namespace Testmvc.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    //private readonly ApplicationDbContext _dbContext;

    //public EditFileModel(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
    //{
    //    _dbContext = applicationDbContext;
    //    _hostenvironment = webHostEnvironment;

    //}


    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Compound_Interest_Calculator(string ticker, double principal, double apy, double years)
    {
        // Compound Interest operations & printing them on the view
        var myCalulator = new MyCalculator();
        //double result = principal * Math.Pow(1.0 + (apy / 100.0), years);
        var result = myCalulator.compute(ticker, principal, apy, years);
        SaveToJson(ticker, principal, apy, years, result);
        return View("Index");
    }

    // Method to serialize data to a json file
    private void writeToFile(List<StockTicker> storedList)
    {
        using (StreamWriter file = System.IO.File.CreateText(@"/Users/asimali/Projects/Testmvc/Testmvc/Data/testing.json"))
        {
            JsonSerializer serializer = new JsonSerializer();

            // Serilization directly into file stream
            serializer.Serialize(file, storedList);
        }
    }

    [HttpPost]
    public IActionResult deleteJson(string ticker)
    { 
        List<StockTicker> jsonVals = ReadJsonFile();

        jsonVals.RemoveAll(a => a.ticker == ticker);

        writeToFile(jsonVals);

        return View("Index");

    }


    // Serialize TO Json file..
    private void SaveToJson(string ticker, double principal, double apy, double years, double result)
    {

        // Bringing in whatever information is stored in the Json file..
        List<StockTicker> jsonVals = ReadJsonFile();

        // If jsonVals has data then update..
        if (jsonVals.Any())
        {
            // Matching inputted ticker with Json ticker
            var existingTicker = jsonVals.FirstOrDefault(a => a.ticker == ticker);

            // If the inputed ticker matches the ticker already stored in the json file...
            if (existingTicker != null)
            {
                // Updating the fields with the inputed values
                jsonVals.FirstOrDefault(a => a.ticker == ticker).principal = principal;
                jsonVals.FirstOrDefault(a => a.ticker == ticker).apy = apy;
                jsonVals.FirstOrDefault(a => a.ticker == ticker).years = years;
                jsonVals.FirstOrDefault(a => a.ticker == ticker).result = result;

                writeToFile(jsonVals);

            }
            else
            {
                // add it
                jsonVals.Add(new StockTicker()
                {
                    // Updating the saved json data with the user entered data
                    ticker = ticker,
                    principal = principal,
                    apy = apy,
                    years = years,
                    result = result
                });
                writeToFile(jsonVals);
            }
           
        }
        // Otherwise save ALL the user entered info into a Json file..
        else
        {
            List<StockTicker> data = new List<StockTicker>();

            data.Add(new StockTicker()
            {
                ticker = ticker,
                principal = principal,
                apy = apy,
                years = years,
                result = result
            });

            writeToFile(data);

        }
    }
    
    // Sending deserialized json to the front end
    [HttpGet]
    public IActionResult GetFromJson()
    {
        List<StockTicker> jsonVals = ReadJsonFile();

        ViewBag.results = jsonVals;

        return View("Index");
    }


    // Deserialize FROM Json file..
    private List<StockTicker> ReadJsonFile()
    {  
        if (System.IO.File.Exists(@"/Users/asimali/Projects/Testmvc/Testmvc/Data/testing.json"))
        {
            try
            {
                return JsonConvert.DeserializeObject<List<StockTicker>>(System.IO.File.ReadAllText(@"/Users/asimali/Projects/Testmvc/Testmvc/Data/testing.json"));
            }
            catch (IOException e)
            {
                throw new IOException("No file found", e);
            }
        }
        else
        {
            return new List<StockTicker>();
        }
    }

    //public FileResult Export(string fileName)
    //{
    //    var bytes = _dbContext.File.Where(c => c.FileName == fileName).FirstOrDefault().Content;

    //    //Send the File to Download.
    //    return File(bytes, @"/Users/asimali/Projects/Testmvc/Testmvc/Data/testing.json", fileName);
    //}






    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private class ApplicationDbContext
    {
        public IEnumerable<object> File { get; internal set; }
    }
}

