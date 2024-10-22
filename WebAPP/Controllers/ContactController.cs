using Microsoft.AspNetCore.Mvc;
using WebAPP.Models;

namespace WebAPP.Controllers;

public class ContactController : Controller
{


    private static Dictionary<int, ContactModel> _contacts = new()
    {

        {
            1,
            new ContactModel()
            {
                Id = 1,
                FirstName = "Adam",
                LastName = "Małysz",
                Email = "adam@malysz.pl",
                PhoneNumber = " 999 222 333",
                BirthDate = new DateOnly(2003, 10, 10)
            }
        },
        {
            2,
            new ContactModel()
            {
                Id = 2,
                FirstName = "Zbigniew",
                LastName = "Ziobro",
                Email = "adam@malysz.pl",
                PhoneNumber = " 999 222 333",
                BirthDate = new DateOnly(2003, 10, 10)
            }
        },
        {
            3,
            
            new ContactModel()
            {
            Id = 3,
            FirstName = "Adam",
            LastName = "Małysz",
            Email = "adam@malysz.pl",
            PhoneNumber = " 999 222 333",
            BirthDate = new DateOnly(2003, 10, 10)
        }

    }
    };

    private static int currentId = 3;
    // Lista kontaktów. przycisk dodawania kontaktu
    public IActionResult Index()
    {
        return View(_contacts);
    }
    // formularz dodawania kontaktu
    public IActionResult Add()
    {
        return View();
    }
    // Odebrania danych z formularza walidacja i dodawanie kontaktu do kolekcji
    
    [HttpPost]
    public IActionResult Add(ContactModel contactModel)
    {
        if (!ModelState.IsValid)
        {
            return View(contactModel);
        }

        currentId = ++currentId;
        _contacts.Add(currentId, contactModel);

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        _contacts.Remove(id);
        return View("Index", _contacts);
    }

    public IActionResult Edit()
    {
        throw new NotImplementedException();
    }

    public IActionResult Details()
    {
        throw new NotImplementedException();
    }
}