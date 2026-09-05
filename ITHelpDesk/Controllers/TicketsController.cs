using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITHelpDesk.Models;
using ITHelpDesk.Data;

public class TicketsController : Controller
{
    private readonly HelpDeskDbContext _context;

    public TicketsController(HelpDeskDbContext context)
    {
        _context = context;
    }

    // GET: HDTICKETS
    public async Task<IActionResult> Index()    
    {
        var tickets = _context.HdTickets
        .Include(ticket => ticket.Employee);

        return View(await tickets.ToListAsync());
    }

    // GET: HDTICKETS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hdticket = await _context.HdTickets
            .Include(t => t.Employee)
            .FirstOrDefaultAsync(t => t.TicketId == id);

        if (hdticket == null)
        {
            return NotFound();
        }

        return View(hdticket);
    }

    // GET: Tickets/Create
    public IActionResult Create()
    {
        ViewData["EmployeeId"] = new SelectList(
            _context.HdEmployees,
            "EmployeeId",
            "EmployeeName");

        return View();
    }


    // POST: Tickets/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Title,Description,Category,Priority,EmployeeId")]
    HdTicket hdticket)
    {
        // These values are controlled by the application.
        hdticket.Status = "Open";
        hdticket.CreatedDate = DateTime.Now;
        hdticket.ResolvedDate = null;

        // Employee is a navigation property.
        ModelState.Remove(nameof(HdTicket.Employee));
        ModelState.Remove(nameof(HdTicket.Status));
        ModelState.Remove(nameof(HdTicket.CreatedDate));

        if (ModelState.IsValid)
        {
            _context.HdTickets.Add(hdticket);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] =
                "Ticket created successfully.";

            return RedirectToAction(nameof(Index));
        }

        ViewData["EmployeeId"] = new SelectList(
            _context.HdEmployees,
            "EmployeeId",
            "EmployeeName",
            hdticket.EmployeeId);

        return View(hdticket);
    }

    // GET: Tickets/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await _context.HdTickets.FindAsync(id);

        if (ticket == null)
        {
            return NotFound();
        }

        ViewData["EmployeeId"] = new SelectList(
            _context.HdEmployees,
            "EmployeeId",
            "EmployeeName",
            ticket.EmployeeId);

        return View(ticket);
    }


    // POST: Tickets/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("TicketId,Title,Description,Category,Priority,Status,EmployeeId")]
    HdTicket editedTicket)
    {
        if (id != editedTicket.TicketId)
        {
            return NotFound();
        }

        ModelState.Remove(nameof(HdTicket.Employee));

        if (ModelState.IsValid)
        {
            var existingTicket =
                await _context.HdTickets.FindAsync(id);

            if (existingTicket == null)
            {
                return NotFound();
            }

            existingTicket.Title = editedTicket.Title;
            existingTicket.Description = editedTicket.Description;
            existingTicket.Category = editedTicket.Category;
            existingTicket.Priority = editedTicket.Priority;
            existingTicket.Status = editedTicket.Status;
            existingTicket.EmployeeId = editedTicket.EmployeeId;

            // Automatically manage the resolved date.
            if (editedTicket.Status == "Resolved" ||
                editedTicket.Status == "Closed")
            {
                existingTicket.ResolvedDate ??= DateTime.Now;
            }
            else
            {
                existingTicket.ResolvedDate = null;
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] =
                "Ticket updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        ViewData["EmployeeId"] = new SelectList(
            _context.HdEmployees,
            "EmployeeId",
            "EmployeeName",
            editedTicket.EmployeeId);

        return View(editedTicket);
    }
    // GET: HDTICKETS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hdticket = await _context.HdTickets
            .Include(t => t.Employee)
            .FirstOrDefaultAsync(t => t.TicketId == id);

        if (hdticket == null)
        {
            return NotFound();
        }

        return View(hdticket);
    }

    // POST: HDTICKETS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var hdticket = await _context.HdTickets.FindAsync(id);

        if (hdticket != null)
        {
            _context.HdTickets.Remove(hdticket);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool HdTicketExists(int id)
    {
        return _context.HdTickets.Any(
            ticket => ticket.TicketId == id);
    }
}
