using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaTask.Data;

namespace ProniaTask.ViewComponents;

public class FooterViewComponent : ViewComponent
{
    readonly ProniaDbContext _context;
    public FooterViewComponent(ProniaDbContext context)
    {
        _context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(await _context.Settings.ToDictionaryAsync(s => s.Key, s => s.Value));
    }
}
