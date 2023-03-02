using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace StackOverflowTags.Web.Controllers.Abstractions;

public class MvcController : Controller
{
    private IMediator _mediator;

    protected IMediator Mediator
    {
        get
        {
            if (_mediator is null)
                _mediator = HttpContext.RequestServices.GetService<IMediator>();

            return _mediator!;
        }
    }
}