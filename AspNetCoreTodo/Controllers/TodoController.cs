using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Services;
namespace AspNetCoreTodo.Controllers;

public class TodoController : Controller
{
    private readonly ITodoItemService _todoItemService;
    public TodoController(ITodoItemService todoItemService)
    {
        _todoItemService = todoItemService;
    }
    public async Task<IActionResult> Index()
    {
        var items = await _todoItemService.GetIncompleteItemsAsync();

        //Get to-do items from database
        var model = new TodoViewModel();
        // Put items into a model
        var item = new TodoItem();
        item.DueAt = new DateTimeOffset(2024, 7, 18, 0, 0, 0, TimeSpan.Zero);
        item.Title = "This is a test item";
        // Render view using the model
        model.Items = new TodoItem[1];
        model.Items[0] = item;
        return View(model);
    }

}