using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projeto.Domain.GoogleSheets;

namespace Projeto.Presentation.Pages.Services
{
    public class BookingRegistrationModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private BookingRegistration bookingRegistration { get; set; }

        public BookingRegistrationModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public string Nome { get; set; }

        [BindProperty]
        public string Documento { get; set; }

        [BindProperty]
        public string TipoEvento { get; set; }

        [BindProperty]
        public string Descricao { get; set; }

        [BindProperty]
        public DateTime DataEvento { get; set; }

        public IActionResult OnPost()
        {
            var service = new GoogleSheetsService();

            var bookingRegistration = new BookingRegistration()
            {
                Name = Nome,
                Document = Documento,
                EventType = TipoEvento,
                Description = Descricao,
                EventDateTime = DataEvento,
            };

            var response = service.Integrate(bookingRegistration);

            return Page();
        }

    }
}
