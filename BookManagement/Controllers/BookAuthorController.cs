using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookManagement.Data;
using BookManagement.Models;
using BookManagement.Extensions;
using System.Net;
using BookManagement.ViewModel;

namespace BookManagement.Controllers
{
    public class BookAuthorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookAuthorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? bookId)
        {
            if (bookId == null)
            {
                return NotFound();
            }

            List<BookAuthorViewModel> list = await (from bk in _context.BookAuthors
                                                   join a in _context.Authors on bk.AuthorId equals a.Id
                                                   where bk.BookId == bookId
                                                   select new BookAuthorViewModel
                                                   {
                                                       BookAuthorId = bk.Id,
                                                       AuthorName = a.Name
                                                   }).ToListAsync();

            ViewBag.BookId = bookId;

            return View(list);
        }

        public async Task<IActionResult> Details(int? id, int? bookId)
        {
            if (id == null || bookId == null || _context.BookAuthors == null)
            {
                return NotFound();
            }

            BookAuthorViewModel? ba = await (from bk in _context.BookAuthors
                                            join a in _context.Authors on bk.AuthorId equals a.Id
                                            where bk.Id == id
                                            select new BookAuthorViewModel
                                            {
                                                BookAuthorId = bk.Id,
                                                AuthorName = a.Name
                                            }).FirstOrDefaultAsync();

            if (ba == null)
            {
                return NotFound();
            }

            ViewBag.BookId = bookId;

            return View(ba);
        }

        public async Task<IActionResult> Create(int bookId)
        {
            List<Author> list = await _context.Authors.ToListAsync();

            BookAuthor bookAuthor = new()
            {
                BookId = bookId,
                Authors = list.ConvertToSelectList(0)
            };

            return View(bookAuthor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,AuthorId")] BookAuthor bookAuthor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookAuthor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { bookId = bookAuthor.BookId });
            }

            return View(bookAuthor);
        }

        public async Task<IActionResult> Edit(int? id, int? bookId)
        {
            if (id == null || bookId == null || _context.BookAuthors == null)
            {
                return NotFound();
            }

            var bookAuthor = await _context.BookAuthors.FindAsync(id);

            List<Author> list = await _context.Authors.ToListAsync();

            if (bookAuthor == null)
            {
                return NotFound();
            }

            bookAuthor.Authors = list.ConvertToSelectList(0);

            return View(bookAuthor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,AuthorId")] BookAuthor bookAuthor)
        {
            if (id != bookAuthor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookAuthor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookAuthorExists(bookAuthor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { bookId = bookAuthor.BookId });
            }

            return View(bookAuthor);
        }

        public async Task<IActionResult> Delete(int? id, int? bookId)
        {
            if (id == null || bookId == null || _context.BookAuthors == null)
            {
                return NotFound();
            }

            BookAuthorViewModel? ba = await (from bk in _context.BookAuthors
                                            join a in _context.Authors on bk.AuthorId equals a.Id
                                            where bk.Id == id
                                            select new BookAuthorViewModel
                                            {
                                                BookAuthorId = bk.Id,
                                                AuthorName = a.Name
                                            }).FirstOrDefaultAsync();

            if (ba == null)
            {
                return NotFound();
            }

            ViewBag.BookId = bookId;

            return View(ba);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookAuthor = await _context.BookAuthors.FindAsync(id);

            if (bookAuthor == null)
            {
                return NotFound();
            }

            _context.BookAuthors.Remove(bookAuthor);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { bookId = bookAuthor.BookId });
        }

        private bool BookAuthorExists(int id)
        {
            return (_context.BookAuthors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
