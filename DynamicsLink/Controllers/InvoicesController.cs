using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DynamicsLink.Data;
using DynamicsLink.Models;

namespace DynamicsLink.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Invoices.Include(i => i.Item).Include(i => i.Unit);
            return View(await applicationDbContext.ToListAsync());
        }
        public  IActionResult NewInvoice()
        {
            ViewData["ItemID"] = new SelectList(_context.Items, "ID", "Name");
            ViewData["UnitID"] = new SelectList(_context.Units, "ID", "Name");
            ViewData["StoreID"] = new SelectList(_context.Stores, "ID", "Name");
            var InvoiceID = _context.InvoicesContainer.OrderByDescending(u => u.No).FirstOrDefault();
            ViewBag.InvoiceID = InvoiceID.No+1;
            return View();
        }
        public ICollection<Item> StoreChanged(int? id)
        {
            return _context.Items.Where(i=>i.StoreID==id).ToList();
        }
        public ICollection<Unit> ItemChanged(int? id)
        {
            return _context.Units.Where(i => i.ItemID == id).ToList();
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Item)
                .Include(i => i.Unit)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["ItemID"] = new SelectList(_context.Items, "ID", "Name");
            ViewData["UnitID"] = new SelectList(_context.Units, "ID", "Name");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Invoices.Add(invoice);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemID"] = new SelectList(_context.Items, "ID", "Name", invoice.ItemID);
            ViewData["UnitID"] = new SelectList(_context.Units, "ID", "Name", invoice.UnitID);
            return View(invoice);
        }
        [HttpPost]
        public IActionResult CreateContainer(InvoiceContainer invoiceContainer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceContainer);
                _context.SaveChanges();
                var id = _context.InvoicesContainer.FirstOrDefault(c => c.No == invoiceContainer.No).ID;
                var invoices = _context.Invoices.Where(i => i.InoviceContainerNo == invoiceContainer.No).ToList();
                foreach (var item in invoices)
                {
                    item.InvoicesContainerID = id;
                }
                _context.SaveChanges();
                return RedirectToAction("NewInvoice");
            }
            return View();
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["ItemID"] = new SelectList(_context.Items, "ID", "ID", invoice.ItemID);
            ViewData["UnitID"] = new SelectList(_context.Units, "ID", "ID", invoice.UnitID);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ItemID,UnitID,Price,Qty,Total,Discount,Net")] Invoice invoice)
        {
            if (id != invoice.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.ID))
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
            ViewData["ItemID"] = new SelectList(_context.Items, "ID", "ID", invoice.ItemID);
            ViewData["UnitID"] = new SelectList(_context.Units, "ID", "ID", invoice.UnitID);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Item)
                .Include(i => i.Unit)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.ID == id);
        }
    }
}
