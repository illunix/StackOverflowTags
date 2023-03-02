using Microsoft.AspNetCore.Mvc;
using StackOverflowTags.Core.Queries;
using StackOverflowTags.Web.Controllers.Abstractions;
using StackOverflowTags.Web.Models;
using System.Diagnostics;

namespace StackOverflowTags.Web.Controllers;

public sealed class HomeController : MvcController
{
    public async Task<IActionResult> Index(GetStackOverflowTagsQuery qry)
        => View(await Mediator.Send(qry));

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}