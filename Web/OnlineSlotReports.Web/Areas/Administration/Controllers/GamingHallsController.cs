namespace OnlineSlotReports.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using OnlineSlotReports.Data;
    using OnlineSlotReports.Data.Models;

    public class GamingHallsController : AdministrationController
    {
        private readonly ApplicationDbContext context;

        public GamingHallsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/GamingHalls
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.context.GamingHalls.Include(g => g.User);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/GamingHalls/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var gamingHall = await this.context.GamingHalls
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamingHall == null)
            {
                return this.NotFound();
            }

            return this.View(gamingHall);
        }

        // GET: Administration/GamingHalls/Create
        public IActionResult Create()
        {
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/GamingHalls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HallName,ImageUrl,Description,PhoneNumber,Adress,Town,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] GamingHall gamingHall)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(gamingHall);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", gamingHall.UserId);
            return this.View(gamingHall);
        }

        // GET: Administration/GamingHalls/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var gamingHall = await this.context.GamingHalls.FindAsync(id);
            if (gamingHall == null)
            {
                return this.NotFound();
            }

            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", gamingHall.UserId);
            return this.View(gamingHall);
        }

        // POST: Administration/GamingHalls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("HallName,ImageUrl,Description,PhoneNumber,Adress,Town,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] GamingHall gamingHall)
        {
            if (id != gamingHall.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(gamingHall);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.GamingHallExists(gamingHall.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", gamingHall.UserId);
            return this.View(gamingHall);
        }

        // GET: Administration/GamingHalls/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var gamingHall = await this.context.GamingHalls
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamingHall == null)
            {
                return this.NotFound();
            }

            return this.View(gamingHall);
        }

        // POST: Administration/GamingHalls/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var gamingHall = await this.context.GamingHalls.FindAsync(id);
            this.context.GamingHalls.Remove(gamingHall);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool GamingHallExists(string id)
        {
            return this.context.GamingHalls.Any(e => e.Id == id);
        }
    }
}
