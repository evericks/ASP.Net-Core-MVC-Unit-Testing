using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ThienThai.Controllers
{
    public class AdminController : Controller
    {
        private readonly SupperContext _context;

        public AdminController(SupperContext context)
        {
            _context = context;
        }

        // GET: Player
        public async Task<IActionResult> Index()
        {
            dynamic model = new ExpandoObject();
            model.Player = await _context.Player.ToListAsync();
            model.Comment = await _context.Comment.ToListAsync();
            return View(model);
        }

        // GET: Player/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (!IdolExists(id))
            {
                return NotFound();
            }
            var player = await _context.Player
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // GET: Player/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idol = await _context.Player.FindAsync(id);
            if (idol == null)
            {
                return NotFound();
            }
            return View(idol);
        }

        // GET: Player/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idol = await _context.Player
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idol == null)
            {
                return NotFound();
            }

            return View(idol);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var idol = await _context.Player.FindAsync(id);
            _context.Player.Remove(idol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IdolExists(int id)
        {
            return _context.Player.Any(e => e.Id == id);
        }
        private bool IdolEmailExists(string email)
        {
            return _context.Player.Any(e => e.Email == email);
        }
    }
}
