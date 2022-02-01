using McbaExample.Data;
using McbaExample.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace McbaExampleWithLogin.HostedService
{
    public class BillPayService : BackgroundService
    {
        private readonly IServiceProvider _services; 
        private readonly ILogger<BillPayService>_logger; 

        public BillPayService(IServiceProvider services, ILogger<BillPayService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background service is running"); 

            while (!stoppingToken.IsCancellationRequested)
            {
                await DoWork(stoppingToken);
                _logger.LogInformation("Background service is waiting a minute");
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

            }
            throw new NotImplementedException();
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background service is working");

            using var scope = _services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<McbaContext>();

            var bills = await context.BillPay.Where(x => x.ScheduleDate <= DateTime.UtcNow).ToListAsync(stoppingToken);
      


            foreach (var bill in bills)
            {
                var account = await context.Accounts.FindAsync(bill.AccountNumber);

                if (account.Balance < bill.Amount
               || (account.AccountType.Equals(1) && account.Balance - bill.Amount < 300))
                {
                    context.BillPay.Remove(bill);
                }
                else
                {
                    account.Balance -= bill.Amount;
                    account.Transactions.Add(new Transaction
                    {
                        TransactionType = (TransactionType)2,
                        AccountNumber = bill.AccountNumber,
                        Amount = bill.Amount,
                        TransactionTimeUtc = DateTime.UtcNow
                    });

                    if (bill.Period.Equals("Once off")) 
                    {
                        // Remove billpay
                        context.BillPay.Remove(bill);
                    }
                    else
                    {
                        // Reschedule billpay
                        bill.ScheduleDate = bill.ModifyDate.AddMonths(1);
                    }
                }


            }

            await context.SaveChangesAsync(stoppingToken);
            _logger.LogInformation("Background service work is done!"); 


        }
    }
}
