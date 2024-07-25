using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Controllers;

[Authorize]
public class TodoController : Controller
{
    private readonly ITodoItemService _todoItemService;
    private readonly UserManager<IdentityUser> _userManager;

    public TodoController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager)
    {
        _todoItemService = todoItemService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null) return Challenge();


        var items = await _todoItemService.GetIncompleteItemsAsync(currentUser);

        //Get to-do items from database
        var model = new TodoViewModel()
        {
            Items = items
        };
        return View(model);
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(TodoItem newItem)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        var successful = await _todoItemService.AddItemAsync(newItem);
        if (!successful)
        {
            return BadRequest("Could not add item.");
        }
        return RedirectToAction("Index");
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkDone(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToAction("Index");
        }
        var successful = await _todoItemService.MarkDoneAsync(id);
        if (!successful)
        {
            return BadRequest("Could not mark item as done.");
        }
        return RedirectToAction("Index");
    }
}