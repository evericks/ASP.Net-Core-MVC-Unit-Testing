using Data;
using Data.Access;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Supper.Controllers
{
    public class UsersController : Controller
    {
        private readonly SupperContext _context;

        private readonly UserAccess dao = new UserAccess();

        User result = null;

        public UsersController(SupperContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login([Bind] User user)
        {
            result = dao.CheckLogin(user);
            if (result != null)
            {
                HttpContext.Session.SetString("User", result.Name);
                HttpContext.Session.SetString("Username", result.Username);
                HttpContext.Session.SetString("Role", result.Role);
                HttpContext.Session.SetString("Email", result.Email);
                return Json("success");
            }
            else
            {
                return Json("failed");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // GET: Users/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return NotFound();
        }

        // POST: Users/Details
        [HttpPost]
        public async Task<IActionResult> Details([Bind("Username")] User usera)
        {
            if (usera.Username == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Username == usera.Username);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Index()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Index([Bind] User user)
        {
            int result = dao.CreateUser(user);
            if (result == 1)
            {
                return Json("success");
            }
            else
            {
                return Json("failed");
            }
        }

        [HttpPost]
        public IActionResult ChangePass([Bind] User user)
        {
            bool result = dao.ChangePass(user);
            if (result == false)
            {
                return Json("failed");
            }
            return Json("success");
        }
    }
}
