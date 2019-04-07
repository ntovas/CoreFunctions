using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreFunctions.Data.Data;

namespace CoreFunctions.Admin.Controllers
{
    public class FunctionController : Controller
    {
        private readonly CoreFunctionsDbContext _context;

        public FunctionController(CoreFunctionsDbContext context)
        {
            _context = context;
        }

        // GET: Function
        public async Task<IActionResult> Index()
        {
            return View(await _context.Functions.ToListAsync());
        }

        // GET: Function/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionModel = await _context.Functions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (functionModel == null)
            {
                return NotFound();
            }

            return View(functionModel);
        }

        // GET: Function/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Function/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ShouldExecuteDelegate,Script,Order,IsActive,Created,Imports,References")] FunctionModel functionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(functionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(functionModel);
        }

        // GET: Function/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionModel = await _context.Functions.FindAsync(id);
            if (functionModel == null)
            {
                return NotFound();
            }
            return View(functionModel);
        }

        // POST: Function/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShouldExecuteDelegate,Script,Order,IsActive,Created,Imports,References")] FunctionModel functionModel)
        {
            if (id != functionModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(functionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunctionModelExists(functionModel.Id))
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
            return View(functionModel);
        }

        // GET: Function/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionModel = await _context.Functions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (functionModel == null)
            {
                return NotFound();
            }

            return View(functionModel);
        }

        // POST: Function/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var functionModel = await _context.Functions.FindAsync(id);
            _context.Functions.Remove(functionModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FunctionModelExists(int id)
        {
            return _context.Functions.Any(e => e.Id == id);
        }
    }
}
