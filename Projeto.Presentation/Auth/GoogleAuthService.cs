using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace Projeto.Presentation.Auth
{
    public class GoogleAuthService
    {
        public SheetsService AuthenticateGoogleApiUse()
        {
            var credencialPath = "";
            var credentialFile = GoogleCredential.FromFile(credencialPath);

            var sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentialFile,
                ApplicationName = "My First Project",
            });

            var alteracao = "";

            return sheetsService;
        }
    }
}
