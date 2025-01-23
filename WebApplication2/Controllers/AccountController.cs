using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers;

public class AccountController : Controller
{
    // Lista użytkowników w pamięci (na potrzeby nauki)
    private static readonly List<User> Users = new List<User>
    {
        new User { Username = "admin", Password = "admin" }, // Domyślny admin
        new User { Username = "basia", Password = "123" },
        new User { Username = "asia", Password = "321" },
        new User { Username = "krzys", Password = "111" },
        new User { Username = "lukas", Password = "222" },
        new User { Username = "miachal", Password = "333" },
        new User { Username = "mati", Password = "111222333" },
    };

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult ManageUsers()
    {
        return View(Users);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult EditUser(string username)
    {
        var user = Users.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult EditUser(string username, string newPassword)
    {
        var user = Users.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            user.Password = newPassword;
            ViewBag.Message = "Password updated successfully!";
        }
        else
        {
            ViewBag.Error = "User not found!";
        }
        return RedirectToAction("ManageUsers");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult DeleteUser(string username)
    {
        var user = Users.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            Users.Remove(user);
            ViewBag.Message = "User deleted successfully!";
        }
        else
        {
            ViewBag.Error = "User not found!";
        }
        return RedirectToAction("ManageUsers");
    }


    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, username == "admin" ? "Admin" : "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Invalid username or password";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string username, string password)
    {
        // Sprawdzenie, czy użytkownik już istnieje
        if (Users.Any(u => u.Username == username))
        {
            ViewBag.Error = "Username already exists.";
            return View();
        }

        // Dodanie nowego użytkownika do listy
        Users.Add(new User { Username = username, Password = password });

        ViewBag.Success = "User registered successfully!";
        return RedirectToAction("Login");
    }

    // Klasa reprezentująca użytkownika
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}