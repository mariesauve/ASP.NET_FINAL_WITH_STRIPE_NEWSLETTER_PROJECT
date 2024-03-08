using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using KoolStuff.Data;
using KoolStuff.Models;

namespace KoolStuff.Controllers
{
    public class EmailStoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmailStoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmail(string userName, string email)
        {
            try
            {
                // Create a new EmailStore object
                var emailStore = new EmailStore
                {
                    UserName = userName,
                    Email = email
                };

                // Add the object to the database context
                _context.EmailStore.Add(emailStore);

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Email saved successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Failed to save email: " + ex.Message });
            }
        }

        // GET: EmailStore
        public async Task<IActionResult> Index()
        {
            var emailList = await _context.EmailStore.ToListAsync();
            return View(emailList);
        }



        // GET: EmailStore/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailStore = await _context.EmailStore
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emailStore == null)
            {
                return NotFound();
            }

            return View(emailStore);
        }

        // GET: EmailStore/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmailStore/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Email")] EmailStore emailStore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emailStore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emailStore);
        }

        // GET: EmailStore/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailStore = await _context.EmailStore.FindAsync(id);
            if (emailStore == null)
            {
                return NotFound();
            }
            return View(emailStore);
        }

        // POST: EmailStore/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Email")] EmailStore emailStore)
        {
            if (id != emailStore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the current state of the entity from the database
                    var existingEmailStore = await _context.EmailStore.FindAsync(id);
                    if (existingEmailStore == null)
                    {
                        return NotFound();
                    }

                    // Update the entity's properties
                    existingEmailStore.UserName = emailStore.UserName;
                    existingEmailStore.Email = emailStore.Email;

                    // Update the entity in the database
                    _context.Update(existingEmailStore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency conflicts
                    if (!EmailStoreExists(emailStore.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Retry logic or other error handling
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(emailStore);
        }



        // GET: EmailStore/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailStore = await _context.EmailStore
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emailStore == null)
            {
                return NotFound();
            }

            return View(emailStore);
        }

        // POST: EmailStore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emailStore = await _context.EmailStore.FindAsync(id);
            if (emailStore != null)
            {
                _context.EmailStore.Remove(emailStore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailStoreExists(int id)
        {
            return _context.EmailStore.Any(e => e.Id == id);
        }
    }
}
