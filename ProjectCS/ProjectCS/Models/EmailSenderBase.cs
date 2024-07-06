
public abstract class EmailSenderBase
{
    public abstract Task SendEmailAsync(string email, string subject, string htmlMessage, string fromEmail, string fromPassword);
}