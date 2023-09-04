using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Projeto.Domain.GoogleSheets;
using Projeto.Presentation.Auth;
using Projeto.Infrastructure.Data.Validations;

namespace Projeto.Presentation
{
    public class GoogleSheetsService
    {
        private readonly BookingRegistrationDataValidation _ValidationRepository;

        public GoogleSheetsService(BookingRegistrationDataValidation validationsRepository)
        {
            _ValidationRepository = validationsRepository;
        }

        public async Task<string> Integrate(BookingRegistration bookingRegistration)
        {
            var sheetInfo = GetSheetInfo();

            var formattedEventDatetime = GetFormattedEventDatetimeString(bookingRegistration.EventDateTime);

            var isRegisterAvailable = _ValidationRepository.ValidateBookingRegistration(bookingRegistration, formattedEventDatetime);

            if (!isRegisterAvailable)
                return "Evento não pode ser cadastrado!";

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

            var valueRange = new ValueRange() { Values = register };

            var service = GetGoogleApiAuthentication();

            var response = GetSheetPostRequest(service, valueRange, sheetInfo);

            if (response.Updates.UpdatedCells > 0)
                return "Evento cadastrado com sucesso!";

            return "Evento não cadastrado!";
        }

        public SheetInfo GetSheetInfo()
        {
            var sheetInfo = new SheetInfo()
            {
                SheetId = "1dxRUYdMk3yqhZLlYWzdHpHN8L7JFyHlSwrSe8y6mfSE",
                Range = "sheet1!A1:E1"
            };

            return sheetInfo;
        }

        public string GetFormattedEventDatetimeString(DateTime eventDatetime)
        {
            var FormattedEventDatetime = eventDatetime.ToString("dd/MM/yyyy HH:mm:ss");

            return FormattedEventDatetime;
        }

        public SheetsService GetGoogleApiAuthentication()
        {
            var serviceAuth = new GoogleAuthService();

            var service = serviceAuth.AuthenticateGoogleApiUse();

            return service;
        }

        public AppendValuesResponse GetSheetPostRequest(SheetsService service, ValueRange valueRange, SheetInfo sheetInfo)
        {
            SpreadsheetsResource.ValuesResource.AppendRequest request =
                service.Spreadsheets.Values.Append(valueRange, sheetInfo.SheetId, sheetInfo.Range);

            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;

            AppendValuesResponse response = request.Execute();

            return response;
        }

        public struct SheetInfo
        {
            public string SheetId { get; set; }

            public string Range { get; set; }
        }

    }
}