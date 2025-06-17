using BBTT.DBModels;
using BBTT.DBModels.Email;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTT.DBPostgres;
public class EmailDataAcess : IEmailDataAcess
{
    DbContextPostgres _dbContext;

    public EmailDataAcess (DbContextPostgres dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveEmailCodeAsync (string email, string code)
    {
       _dbContext.Emails.Add(new EmailVerficationDTO
       {
           Email = email,
           Code = code,
           CreatedAt = DateTime.UtcNow
       });
        return _dbContext.SaveChangesAsync();
    }

    public Task<bool> VerifyEmailCodeAsync (string email, string code)
    {
        var emailVerification = _dbContext.Emails
            .Where(e => e.Email == email && e.Code == code)
            .FirstOrDefault();
        if (emailVerification == null)
        {
            return Task.FromResult(false);
        }
        // Check if the code is expired (e.g., older than 5 minutes)
        if (DateTime.UtcNow > emailVerification.ExpirationDate)
        {
            return Task.FromResult(false);
        }
        // Code is valid and not expired
        return Task.FromResult(true);
    }
}
