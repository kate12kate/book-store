using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using EBook.Service.Interface;
using Microsoft.Extensions.Logging;
using EBook.Domain.DomainModels;
using EBook.Domain.DTO;
using ClosedXML.Excel;
using EBook.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace BookShop.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        // private readonly ILogger<BooksController> _logger;

        public BooksController( IBookService bookService)
        {
            //_logger = logger;
            _bookService = bookService;
        }

        // GET: Books
        [HttpGet]
        public IActionResult Index()
        {
            //_logger.LogInformation("User Request -> Get All bookss!");
            return View(this._bookService.GetAllBooks());
        }

        [HttpPost]
        public IActionResult Index(string title)
        {
            if (title == null)
            {
                return RedirectToAction("Index");
            }
            var allTickets = _bookService.GetAllBooksByTitle(title);
            return View(allTickets);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = this._bookService.GetDetailsForBook(id);
               
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookName,BookImage,BookDescription,Price,Rating,Genre")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = Guid.NewGuid();
                this._bookService.CreateNewBook(book);
                return RedirectToAction(nameof(Index));
                
            }
            return View(book);
        }

       

      


        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            //_logger.LogInformation("User Request -> Get edit form for Book!");
            if (id == null)
            {
                return NotFound();
            }

            var book = this._bookService.GetDetailsForBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,BookName,BookImage,BookDescription,Price,Rating,Genre")] Book book)
        {

           // _logger.LogInformation("User Request -> Update Book in DataBase!");

            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._bookService.UpdeteExistingBook(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
           // _logger.LogInformation("User Request -> Get delete form for Book!");

            if (id == null)
            {
                return NotFound();
            }

            var book = this._bookService.GetDetailsForBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
              _bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
            
        }

        public IActionResult AddBookToCard(Guid id)
        {
            var result = this._bookService.GetShoppingCartInfo(id);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBookToCard(AddToShoppingCartDto model)
        {

           // _logger.LogInformation("User Request -> Add book in ShoppingCart and save changes in database!");


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._bookService.AddToShoppingCart(model, userId);

            if (result)
            {
                return RedirectToAction("Index", "Books");
            }
            return View(model);
        }
        private bool BookExists(Guid id)
        {
            return _bookService.GetDetailsForBook(id) != null;
        }


        [HttpGet]
        [Authorize(Roles = RoleName.Admin)]
        public IActionResult ExportAllBooks()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public FileContentResult ExportAllBooks([Bind("Genre")] Book book)
        {
            string fileName = "Books-" + book.Genre + ".xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("All books - " + book.Genre);
                worksheet.ColumnWidth = 50;
                worksheet.Cell(1, 1).Value = "Book Id";
                worksheet.Cell(1, 2).Value = "Book Name";
                worksheet.Cell(1, 3).Value = "Book Price";
                worksheet.Cell(1, 4).Value = "Genre";
                worksheet.Cell(1, 5).Value = "Book Description";
                worksheet.Cell(1, 6).Value = "Book Image";
                

                var result = this._bookService.GetAllBooksGenre(book.Genre);
                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];
                    worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.BookName;
                    worksheet.Cell(i + 1, 3).Value = item.Price.ToString() + "$";
                    worksheet.Cell(i + 1, 4).Value = item.Genre;
                    worksheet.Cell(i + 1, 5).Value = item.BookDescription;
                    worksheet.Cell(i + 1, 6).Value = item.BookImage;
                  
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }
            }
        }


    }
}
