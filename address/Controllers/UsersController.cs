using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using address.Data;
using address.Models;
using Microsoft.AspNetCore.Authorization;
using address.Log;

namespace address.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly AddressSystemContext db;
        Logger Log = new Logger();
        public UsersController(AddressSystemContext context)
        {
            db = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 2)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View(await db.Users.ToListAsync());
            }
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 2)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var users = await db.Users
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (users == null)
                {
                    return NotFound();
                }
                Log.Info($"[User:Detail][User:{User.Identity.Name}]", users.Email, users.Name);
                return View(users);
            }
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 2)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<SelectListItem> UGroupsItems = new List<SelectListItem>();
                UGroupsItems.AddRange(new[]{
                                new SelectListItem() { Text = "Пользователь", Value = "0" },
                                new SelectListItem() { Text = "Администратор", Value = "1" },
                                new SelectListItem() { Text = "Владелец", Value = "2" }});
                SelectList UGroups = new SelectList(UGroupsItems, "Value", "Text", 0);
                ViewBag.UGroups = UGroups;
                return View();
            }
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Password,Group,RegistrationDate,Email")] Users users)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 2)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Add(users);
                    await db.SaveChangesAsync();
                    Log.Info($"[User:Create][User:{User.Identity.Name}]", users.Id, users.Email, users.Name,users.Group, users.RegistrationDate);
                    return RedirectToAction(nameof(Index));
                }
                return View(users);
            }
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 2)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var users = await db.Users.FindAsync(id);
                List<SelectListItem> UGroupsItems = new List<SelectListItem>();
                UGroupsItems.AddRange(new[]{
                                new SelectListItem() { Text = "Пользователь", Value = "0" },
                                new SelectListItem() { Text = "Администратор", Value = "1" },
                                new SelectListItem() { Text = "Владелец", Value = "2" }});
                SelectList UGroups = new SelectList(UGroupsItems, "Value", "Text", users.Group);
                ViewBag.UGroups = UGroups;
                if (users == null)
                {
                    return NotFound();
                }
                return View(users);
            }
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Password,Group,RegistrationDate,Email")] Users users)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 2)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (id != users.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Update(users);
                        await db.SaveChangesAsync();
                        Log.Info($"[User:Edit][User:{User.Identity.Name}]", users.Id, users.Email, users.Name, users.Group, users.RegistrationDate);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UsersExists(users.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(users);
            }
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 2)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var users = await db.Users
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (users == null)
                {
                    return NotFound();
                }

                return View(users);
            }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Users Iuser = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            if (Iuser == null || Iuser.Group < 2)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var users = await db.Users.FindAsync(id);
                db.Users.Remove(users);
                await db.SaveChangesAsync();
                Log.Info($"[User:Delete][User:{User.Identity.Name}]", users.Id, users.Email, users.Name, users.Group, users.RegistrationDate);
                return RedirectToAction(nameof(Index));
            }
        }

        private bool UsersExists(int id)
        {
            return db.Users.Any(e => e.Id == id);
        }
    }
}
