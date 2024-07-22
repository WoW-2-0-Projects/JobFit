using JobFit.Domain.Entities;
using JobFit.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JobFit.Persistence.Data;

public static class SeedDataExtensions
{
    public static async ValueTask InitializeSeedAsync(this IServiceProvider serviceProvider)
    {
        using var cancellationSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));

        // Seed data for database storage
        var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();

        if (!await appDbContext.Users.AnyAsync(cancellationToken: cancellationSource.Token))
        {
            await appDbContext.SeedUsersAsync();
            await appDbContext.SeedEmployeeAsync();
        }

        // if (!await appDbContext.RideRequests.AnyAsync(cancellationToken: cancellationSource.Token))
        // await appDbContext.SeedRideRequestsAsync();

        // if (!await appDbContext.SmsTemplates.AnyAsync(cancellationToken: cancellationSource.Token))
        //     await appDbContext.SeedSmsTemplatesAsync();

        if (appDbContext.ChangeTracker.HasChanges())
            await appDbContext.SaveChangesAsync(cancellationSource.Token);
    }
    
    /// <summary>
    /// Seeds initial users data
    /// </summary>
    private static async ValueTask SeedUsersAsync(this AppDbContext dbContext)
    {
        var users = new List<User>
        {
            new()
            {
                Id = Guid.Parse("399EF78D-D0B5-4E01-9AAD-D9C6AF1EB23D"),
                FirstName = "System",
                LastName = "System",
                PhoneNumber = "+998936054827",
                EmailAddress = "isxoqovxasanboy1@gmail.com",
            }
        };

        await dbContext.Users.AddRangeAsync(users);
    }


    private static async ValueTask SeedEmployeeAsync(this AppDbContext dbContext)
    {
        var employees = new List<Employee>()
        {
            new()
            {
                Id = Guid.Parse("A038D4C8-190A-4EE2-A3AE-5B76D4AE90CD"),
                FirstName = "Husanboy",
                LastName = "Isxoqov",
                PhoneNumber = "+998930792526",
                EmailAddress = "isxoqovxasanboy1@gmail.com",
            }

        };
        
        await dbContext.Employees.AddRangeAsync(employees);

    }
    
    
    /*private static async ValueTask SeedSmsTemplatesAsync(this AppDbContext appDbContext)
    {
        var smsTemplates = new List<SmsTemplate>
        {
            new()
            {
                TemplateType = NotificationTemplateType.SystemWelcomeNotification,
                Content = """
                          Hey! Welcome to our system. We are glad to have you with us.

                          Assalomu aleykum! Tizimga muvaffaqiyatli ro'yxatdan o'tdingiz. Servisimizni tanlaganizdan mamnunmiz.
                          """
            },
            new()
            {
                TemplateType = NotificationTemplateType.SignUpVerificationNotification,
                Content = """
                          DO NOT SHARE THE CODE WITH ANYONE. Your OTP code for sign up verification : {{Otp}}.

                          KODNI HECH KIMGA BERMANG. Tizimga ro'yxatdan o'tish uchun tasdiqlash uchun kod : {{Otp}}.
                          """,
            },
            new()
            {
                TemplateType = NotificationTemplateType.SignInConfirmationNotification,
                Content = """
                          DO NOT SHARE THE CODE WITH ANYONE. Your OTP code for sign in confirmation : {{Otp}}.

                          KODNI HECH KIMGA BERMANG. Tizimga kirishni tasdiqlash uchun kod : {{Otp}}.
                          """,
            }
        };

        await appDbContext.SmsTemplates.AddRangeAsync(smsTemplates);
    }*/
    
}