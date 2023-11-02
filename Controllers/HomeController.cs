using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("process")]
    public IActionResult ProcessSession(User user)
    {
        if (ModelState.IsValid)
        {

            HttpContext.Session.SetString("UserName", user.Name);
            // Console.WriteLine(HttpContext.Session.GetString("UserName"));
            return RedirectToAction("Dashboard");
        }
        else
        {
            return View("Index");
        }
    }

    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
    [HttpGet("/dashboard")]
    public IActionResult Dashboard()
    {
        string? UserName = HttpContext.Session.GetString("UserName");
        if (UserName == null)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View();
        }
    }

    [HttpGet("/operation")]
    public IActionResult Operation(string operation)
    {
        Console.WriteLine(operation);

        
        int? count = HttpContext.Session.GetInt32("Count");
        count ??= 22;

        if (operation == "sum")
        {
            count++;
        }
        if (operation == "minus")
        {
            count--;
        }
        if (operation == "double")
        {
            count *= 2;
        }
        if (operation == "random")
        {
            Random rand = new Random();
            count += rand.Next(1, 11);
        }
        

        
        HttpContext.Session.SetInt32("Count", (int)count);

        
        return RedirectToAction("Dashboard");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
