using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using address.ViewModels; // пространство имен моделей RegisterModel и LoginModel
using address.Models; // пространство имен UserContext и класса User
using address.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using address.Log;

namespace address.Controllers
{
    public class AccountController : Controller
    {
        private AddressSystemContext db;
        Logger Log = new Logger();
        public AccountController(AddressSystemContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Вход пользователя
        /// </summary>
        /// <param name="model">Модель с данными пользователя</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user.Name); // аутентификация
                    Log.Info($"Пользователь {user.Name} успешно вошёл в систему!");
                    return RedirectToAction("Index", "Home");
                }
                Log.Warning($"Неудачная попытка входа пользователя {model.Email}!");
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="model">Модель с данными нового пользователя</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email || u.Name == model.Name);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.Users.Add(new Users { Name = model.Name, Email = model.Email, Password = model.Password, RegistrationDate = DateTime.Now.Date });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Name); // аутентификация
                    Log.Info($"Зарегистрирован новый пользователь: {model.Name}!");
                    return RedirectToAction("Index", "Home");
                }
                else
                    Log.Warning($"Неудачная попытка регистрации пользователя с Email: {model.Email} и Именем:{model.Name}!");
                    ModelState.AddModelError("", "Данный Email/Пользователь уже используется");
            }
            return View(model);
        }
        /// <summary>
        /// Личный кабинет GET: /Account/Cabinet
        /// </summary>
        [HttpGet]
        public IActionResult Cabinet()
        {
            if (User.Identity.IsAuthenticated)
            {
                Users user = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ViewData["UGroup"] = user.Group;
                    ViewData["UsersCount"] = db.Users.Count();
                    return View(user);
                }
            }
            return RedirectToAction("Login", "Account");
        }
        /// <summary>
        /// Аутентификация пользователя при входе/регистрации
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
