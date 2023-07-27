using Library.BL;
using Library.DAL;
using Library.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksManager _booksManager;

        public BooksController(IBooksManager booksManager)
        {
            _booksManager = booksManager;
        }


        #region 1-Select

        public IActionResult Index()
        {
            var AllBooks = _booksManager.GetAll();
            return View(AllBooks);
        }

        public IActionResult Details(int id)
        {
            var book = _booksManager.GetByIDWithClients(id);
            return View(book);
        }

        #endregion

        #region 2-Add

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookAddVM book)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            RequestStatus requestStatus = _booksManager.Add(book);
            TempData[Constants.Status] = requestStatus.Status;
            TempData[Constants.Message] = requestStatus.Message;

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region 3-Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _booksManager.GetByIdToEdit(id);

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(BookEditVM book)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            RequestStatus requestStatus = _booksManager.Edit(book);
            TempData[Constants.Status] = requestStatus.Status;
            TempData[Constants.Message] = requestStatus.Message;

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region 4-Delete
        public IActionResult Delete(int id)
        {
            RequestStatus requestStatus = _booksManager.Delete(id);
            TempData[Constants.Status] = requestStatus.Status;
            TempData[Constants.Message] = requestStatus.Message;
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region 5-Borrow Book

        [HttpGet]
        public IActionResult Borrow()
        {
            return View();
        }

        [HttpPost]
        public IActionResult borrow(BookBorrowVM book)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            RequestStatus requestStatus = _booksManager.Borrow(book);
            TempData[Constants.Status] = requestStatus.Status;
            TempData[Constants.Message] = requestStatus.Message;

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region 6-Return Book

        [HttpGet]
        public IActionResult Return()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Return(BookReturnVM book)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            RequestStatus requestStatus = _booksManager.Return(book);
            TempData[Constants.Status] = requestStatus.Status;
            TempData[Constants.Message] = requestStatus.Message;

            return RedirectToAction(nameof(Index));
        }

        #endregion




    }
}
