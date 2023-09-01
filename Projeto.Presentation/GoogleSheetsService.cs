using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Projeto.Domain.GoogleSheets;
using Projeto.Presentation.Auth;

namespace Projeto.Presentation
{
    public class GoogleSheetsService
    {
        public string Integrate(BookingRegistration bookingRegistration)
        {
            var sheetsId = "1EJOw6Ivp-unNVq8vA823JqRyYrPFvgzvUvnd4nrCc24";

            var range = "sheet1!A1:E1";

            var register = new List<IList<object>>
            {
                new List<object>
                {
                    bookingRegistration.Name,
                    bookingRegistration.Document,
                    bookingRegistration.EventType,
                    bookingRegistration.Description,
                    bookingRegistration.EventDateTime.ToString()
                }
            };

            var valueRange = new ValueRange()
            {
                Values = register
            };

            var sheetsService = new GoogleAuthService();

            var service = sheetsService.AuthenticateGoogleApiUse();

            SpreadsheetsResource.ValuesResource.AppendRequest request = service.Spreadsheets.Values.Append(valueRange, sheetsId, range);

            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;

            AppendValuesResponse response = request.Execute();

            if (response.Updates.UpdatedCells > 0)
            {
                return "Dados cadastrados com sucesso!";
            }

            return "Dados não cadastrados!";
        }
    }
}