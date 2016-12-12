using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using _2_zadatak.Interfaces;
using _2_zadatak.Models;

namespace _2_zadatak.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController(ITodoRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUser();
        
            return View(_repository.GetActive(Guid.Parse(user.Id)));
            
        }

        public IActionResult Add()
        {
            return View();

        }

        public async Task<IActionResult> Completed()
        {
            var user = await GetCurrentUser();
            var userId = Guid.Parse(user.Id);
            var completedTodos = _repository.GetCompleted(userId);
            return View(completedTodos);
        }

        public async Task<IActionResult> MarkCompleted(Guid id)
        {
            var user = await GetCurrentUser();
            var userId = Guid.Parse(user.Id);
            _repository.MarkAsCompleted(id, userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTodoViewModel m)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUser();
                var item = new TodoItem(m.Text, Guid.Parse(user.Id));
                _repository.Add(item);
                return RedirectToAction("Index");
            }
            return View(m);
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return user;
        }

        
    }

}   
