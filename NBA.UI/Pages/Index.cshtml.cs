using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NBA.UI.Model;
using NBA.UI.Services;

namespace NBA.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly INBAService _nBAService;

        public IndexModel(ILogger<IndexModel> logger, INBAService nBAService)
        {
            _logger = logger;
            _nBAService = nBAService;
        }
        public List<Team> Data { get; set; }
        public async Task OnGet()
        {
            Data = await _nBAService.GetTeamsDetail();
        }
    }
}