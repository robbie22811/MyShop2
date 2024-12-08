using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovies.Models;

namespace RazorPagesMovies.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovies.Data.RazorPagesMoviesContext _context;

        public IndexModel(RazorPagesMovies.Data.RazorPagesMoviesContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; } = default!;

        private readonly ILogger<IndexModel> _logger;

    }
}