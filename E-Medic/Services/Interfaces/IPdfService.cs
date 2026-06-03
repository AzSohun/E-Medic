namespace E_Medic.Services.Interfaces
{
    public interface IPdfService
    {
        byte[] GeneratePdfFromHtml(string htmlContent);
    }
}
