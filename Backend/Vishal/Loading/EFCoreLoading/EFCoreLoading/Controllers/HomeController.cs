using EFCoreLoading.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;



namespace EFCoreLoading.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            //-----------explicit loading -------------

            //Villa? villaTemp = _db.Villas.FirstOrDefault(a => a.Id == 1);
            ////explicit loading - collections
            //_db.Entry(villaTemp).Collection(u => u.VillaAmenity).Load();

            //VillaAmenity? villaAmenityTemp = _db.VillaAmenities.FirstOrDefault(a => a.Id == 1);
            ////explicit loading
            //_db.Entry(villaAmenityTemp).Reference(u => u.villa).Load();

            //-----------explicit loading -------------


            //load villa
            //List<Villa> villas = _db.Villas.ToList();

            // Villa? villaTemp = _db.Villas.FirstOrDefault(a => a.Id == 1);
            //explicit loading
            //_db.Entry(villaTemp).Collection(u => u.VillaAmenity).Load();



            ////-------- eager loading -----------------
            ////Load villa
            //List<Villa> villas = _db.Villas.ToList();
            ////Include will automaticaly access child entities based on PK & FK relation
            //List<Villa> eager_villas = _db.Villas.Include(u => u.VillaAmenity).ToList();
            //foreach (var villa in villas)
            //{
            //    villa.VillaAmenity = _db.VillaAmenities.Where(u => u.VillaId == villa.Id).ToList();
            //}
            ////------- eager loading --------------------




            ////----------- Lazy loading only main entities -----------------
            ////Load all the villa & stores in villas variable
            //IEnumerable<Villa> villas = _db.Villas;    //to reterive data but it will not go to Db until accessed 

            //var totalvillas = villas.Count();    //at this stage it will go to Db 
            ////----------- Lazy loading only main entities -----------------


            ////--------------------lazy loading--------------
            ////load villa
            //List<Villa> villas = _db.Villas.ToList();
            //foreach (var villa in villas)  //for child entities we have to access it individualy
            //{
            //    villa.VillaAmenity = _db.VillaAmenities.Where(u => u.VillaId == villa.Id).ToList();
            //}
            ////--------------------lazy loading--------------

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
    }
}

