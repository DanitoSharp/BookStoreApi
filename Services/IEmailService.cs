using System;

namespace BookStoreApi.Services;

public interface IEmailService
{
    Task SendResetPasswordEmail(string email, string link);
}
