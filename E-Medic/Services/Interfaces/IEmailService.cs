namespace E_Medic.Services.Interfaces
{
    public interface IEmailService
    {
        Task EmailSenderAsync(string toEmail, string subject, string htmlMessage);
    }
}
