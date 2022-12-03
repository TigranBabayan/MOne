using Application.User;
using Application.User.Commands.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator mediatr;
        public UserController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }

        [HttpGet]
        public IActionResult Registration()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(User user)
        {
            if (ModelState.IsValid)
            {
                await mediatr.Send(new UserRegistrationCommand(user));
                return RedirectToAction("Login", "User");
            }

            return RedirectToAction("Registration", "User");
        }


        [HttpGet]
        public IActionResult Login(string modelError)
        {
            if (!string.IsNullOrEmpty(modelError))
            {
                ModelState.AddModelError("", modelError);
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var result = await this.mediatr.Send(new GetUserByEmailAndPassword(user));
            if (result == null)
            {
                var errorMessage = "Email or Password is not Valid";
                return RedirectToAction("Login","User", new { modelError = errorMessage });
            }
            HttpContext.Session.SetString("fullName", result.FullName);
            return View("UserPage", result);
        }


        [HttpGet]
        public IActionResult UserPage(User model)
        {
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("fullName");
            return RedirectToAction("Login","User");
        }
    }
}
