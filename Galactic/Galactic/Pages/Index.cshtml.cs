using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Galactic.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHostEnvironment host;

        public IndexModel(IHostEnvironment host)
        {
            this.host = host;
        }

        public IActionResult OnGet() => File(Path.Combine(Debugger.IsAttached ? host.ContentRootPath : AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "auth", "index.html"), "text/html; charset=utf-8");
    }
}