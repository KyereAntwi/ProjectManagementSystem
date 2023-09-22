using PMS.Messaging.Models;

namespace PMS.Messaging.Contracts;

public interface IEmailService
{
    Task SendEmail(EmailMessage email);
}