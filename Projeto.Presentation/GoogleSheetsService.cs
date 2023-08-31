using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json.Linq;
using Projeto.Presentation.Auth;

namespace Projeto.Presentation
{
    public class GoogleSheetsService
    {
        public string Integrate()
        {
            var sheetsId = "PlanilhaTeste";

            var range = "sheet1!A1:E1";

            var register = new List<IList<object>> { new List<object> {"coluna1", "coluna2", "coluna3", "coluna4", "coluna5" } };

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
