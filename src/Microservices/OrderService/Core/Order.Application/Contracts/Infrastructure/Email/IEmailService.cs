namespace Order.Application.Contracts.Infrastructure.Email;

public interface IEmailService
{
    public Task<bool> SendEmail(Models.Email email);
}