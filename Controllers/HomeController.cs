using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using CalculatorLibrary;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        //ApplicationDBContext db;
        public HomeController(ILogger<HomeController> logger) //ApplicationDBContext context)
        {
            _logger = logger;
            //this.db = context;
        }

        public IActionResult Index(string r1_color, string r2_color, string r3_color, string r4_color, string r5_color, string r6_color)
        {
            ViewData["r1_color"] = r1_color;
            ViewData["r2_color"] = r2_color;
            ViewData["r3_color"] = r3_color;
            ViewData["r4_color"] = r4_color;
            ViewData["r5_color"] = r5_color;
            ViewData["r6_color"] = r6_color;

            ViewData["resistanceRings"] = $"Кольори: Кільце 1: {r1_color} Кільце 2: {r2_color} Кільце 3: {r3_color} Кільце 4: {r4_color} Кільце 5: {r5_color} Кільце 6: {r6_color}";

            CalculateResistance(r1_color, r2_color, r3_color, r4_color, r5_color, r6_color);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public IActionResult AddToDatabase(string r1_color, string r2_color, string r3_color, string r4_color, string r5_color, string r6_color)
        //{
            
        //    string[] colorRings = { r1_color, r2_color, r3_color, r4_color, r5_color, r6_color };
        //    List<string> rings = new();


        //    Resistor resistorToSave = new(rings.Count, rings.ToArray());


        //    db.Add(resistorToSave);
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}


        public void CalculateResistance(string r1_color, string r2_color, string r3_color, string r4_color, string r5_color, string r6_color)
        {
            string[] colorRings = { r1_color, r2_color, r3_color, r4_color, r5_color, r6_color };
            List<string> rings = new();


            for (int i = 0; i < colorRings.Length; i++)
            {
                if (colorRings[i] != null && colorRings[i] != "transparent")
                {
                    rings.Add(colorRings[i]);
                }
            }

            try
            {
                Resistor resistor = new(rings.Count, rings.ToArray());
                resistor.CalculateResistance();
                ViewData["calcAnswR"] = Convert.ToString(resistor.GetResistance());
                ViewData["calcAnswT"] = Convert.ToString(resistor.GetTolerance());
                ViewData["calcAnswTempK"] = Convert.ToString(resistor.GetTempK());
            }
            catch (KeyNotFoundException)
            {
                ViewData["resistanceRings"] = "Помилка! Оберіть кольори резистора вірно.";
            }
        }
    }
}