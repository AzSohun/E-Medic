using E_Medic.Services.Interfaces;

namespace E_Medic.Services
{
    public class EmailService: IEmailService
    {

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task EmailSenderAsync(string emailTo, string subject, string htmlMessage)
        {

        }

    }
}
