using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookManagement.Data;
using BookManagement.Models;
using BookManagement.ViewModel;
using BookManagement.Extensions;
using System.Net;

namespace BookManagement.Controllers
{
    public class BookCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? bookId)
        {
            if (bookId == null)
            {
                return NotFound();
            }

            List<BookCategoryViewModel> list = await (from bc in _context.BookCategories
                                                     join c in _context.Categories on bc.CategoryId equals c.Id
                                                     where bc.BookId == bookId
                                                     select new BookCategoryViewModel
                                                     {
                                                         BookCategoryId = bc.Id,
                                                         Category = c.Name
                                                     }).ToListAsync();

            ViewBag.BookId = bookId;

            return View(list);
        }

        public async Task<IActionResult> Details(int? id, int? bookId)
        {
            if (id == null || bookId == null || _context.BookCategories == null)
            {
                return NotFound();
            }

            BookCategoryViewModel? bc = await (from bk in _context.BookCategories
                                              join c in _context.Categories on bk.CategoryId equals c.Id
                                              where bk.Id == id
                                              select new BookCategoryViewModel
                                              {
                                                  BookCategoryId = bk.Id,
                                                  Category = c.Name
                                              }).FirstOrDefaultAsync();

            if (bc == null)
            {
                return NotFound();
            }

            ViewBag.BookId = bookId;

            return View(bc);
        }

        public async Task<IActionResult> Create(int bookId)
        {
            List<Category> list = await _context.Categories.ToListAsync();

            BookCategory bookCategory = new()
            {
                BookId = bookId,
                Categories = list.ConvertToSelectList(0)
            };

            return View(bookCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,CategoryId")] BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { bookId = bookCategory.BookId });
            }

            return View(bookCategory);
        }

        public async Task<IActionResult> Edit(int? id, int? bookId)
        {
            if (id == null || bookId == null || _context.BookCategories == null)
            {
                return NotFound();
            }

            var bookCategory = await _context.BookCategories.FindAsync(id);

            List<Category> list = await _context.Categories.ToListAsync();

            if (bookCategory == null)
            {
                return NotFound();
            }

            bookCategory.Categories = list.ConvertToSelectList(0);

            return View(bookCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,CategoryId")] BookCategory bookCategory)
        {
            if (id != bookCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookCategoryExists(bookCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { bookId = bookCategory.BookId });
            }

            return View(bookCategory);
        }

        public async Task<IActionResult> Delete(int? id, int? bookId)
        {
            if (id == null || bookId == null || _context.BookCategories == null)
            {
                return NotFound();
            }

            BookCategoryViewModel? bc = await (from bk in _context.BookCategories
                                             join c in _context.Categories on bk.CategoryId equals c.Id
                                             where bk.Id == id
                                             select new BookCategoryViewModel
                                             {
                                                 BookCategoryId = bk.Id,
                                                 Category = c.Name
                                             }).FirstOrDefaultAsync();

            if (bc == null)
            {
                return NotFound();
            }

            ViewBag.BookId = bookId;

            return View(bc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookCategory = await _context.BookCategories.FindAsync(id);

            if (bookCategory == null)
            {
                return NotFound();
            }
            
            _context.BookCategories.Remove(bookCategory);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { bookId = bookCategory.BookId });
        }

        private bool BookCategoryExists(int id)
        {
          return (_context.BookCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
