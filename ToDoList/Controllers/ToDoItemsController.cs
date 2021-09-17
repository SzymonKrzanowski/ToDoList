using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly ToDoItemDBContext _context;

        public ToDoItemsController(ToDoItemDBContext context)
        {
            _context = context;
        }

        // GET: ToDoItems
        public async Task<IActionResult> Index()
        {
            var a = await _context.ToDoItems.ToListAsync();
            return View(a);
        }

        // GET: ToDoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItems
                .FirstOrDefaultAsync(m => m.IdTask == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // GET: ToDoItems/AddOrEditTask

        public async Task<IActionResult> AddOrEditTask(int id)
        {
            if (id == 0)
            {
                return View(new ToDoItem());
            }
            else
                return View(await _context.ToDoItems.FindAsync(id));
        }

        // POST: ToDoItems/AddOrEditTask/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditTask(int idTask,
            [Bind("IdTask,Title,Details,IsDone,Date")] ToDoItem toDoItem)
        {
           

            if (ModelState.IsValid)
            {
                if (toDoItem.IdTask==0)
                {
                    _context.Add(toDoItem);
                }
                else
                {
                    _context.Update(toDoItem);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItem);
        }

        [Route("")]
        [Route("ToDoItems/Index/{stringSearch}")]
        public async Task<IActionResult> Index(string stringSearch)
        {
            var toDo = from m in _context.ToDoItems
                       select m;
            if (!String.IsNullOrEmpty(stringSearch))
            {
                toDo = toDo.Where(s => s.Title.Contains(stringSearch));
            }
            return View(await toDo.ToListAsync());
        }



        // GET: ToDoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItems
                .FirstOrDefaultAsync(m => m.IdTask == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // POST: ToDoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoItemExists(int IdTask)
        {
            return _context.ToDoItems.Any(e => e.IdTask == IdTask);
        }
    }
}
