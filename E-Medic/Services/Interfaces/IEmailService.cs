namespace E_Medic.Services.Interfaces
{
    public interface IEmailService
    {
        Task EmailSenderAsync(string toEmai, string subject, string htmlMessage);
    }
}
