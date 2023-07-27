using Library.BL;
using Library.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Library.MVC.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientsManager _clientsManager;

        public ClientsController(IClientsManager clientsManager)
        {
            _clientsManager = clientsManager;
        }


        #region 1-Select

        public IActionResult Index()
        {
            var AllBooks = _clientsManager.GetAll();
            return View(AllBooks);
        }

        public IActionResult Details(int id)
        {
            var Client = _clientsManager.GetByIDWithBooks(id);
            return View(Client);
        }

        #endregion

        #region 2-Add

        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Add(ClientAddVM client)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            RequestStatus requestStatus = _clientsManager.Add(client); 
            TempData[Constants.Status] = requestStatus.Status;
            TempData[Constants.Message] = requestStatus.Message;
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region 3-Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _clientsManager.GetByIdToEdit(id);

            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(ClientEditVM client)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            RequestStatus requestStatus = _clientsManager.Edit(client);
            TempData[Constants.Status] = requestStatus.Status;
            TempData[Constants.Message] = requestStatus.Message;
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region 4-Delete
        public IActionResult Delete(int id)
        {
            
            RequestStatus requestStatus = _clientsManager.Delete(id);
            TempData[Constants.Status] = requestStatus.Status;
            TempData[Constants.Message] = requestStatus.Message;
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
