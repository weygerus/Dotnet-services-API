using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace Projeto.Presentation.Auth
{
    public class GoogleAuthService
    {
        public SheetsService AuthenticateGoogleApiUse()
        {
            var credencialPath = "C:\\dev\\Starter\\Projeto.Presentation\\Auth\\starterproject-397714-8d3a0b6cd71b.json";

            var credentialFile = GoogleCredential.FromFile(credencialPath);

            var sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentialFile,
                ApplicationName = "",
            });

            return sheetsService;
        }
    }
}
