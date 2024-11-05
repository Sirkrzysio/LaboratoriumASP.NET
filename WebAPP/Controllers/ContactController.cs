using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.Services;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            return View(_contactService.GetAll());
        }

        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Add(ContactModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _contactService.Add(model);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id, ContactModel model)
        {
            _contactService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            return View(_contactService.GetById(id));
        }

        public IActionResult Details(int id)
        {
                return View(_contactService.GetById(id));
        }
    }
}