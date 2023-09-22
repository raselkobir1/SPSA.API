namespace SPSA.API.Helper.BackgroundJob
{
    public class NotificationsBackgroundJob : BackgroundService
    {
        private readonly ILogger<BackgroundService> _logger;
        public NotificationsBackgroundJob(ILogger<BackgroundService> logger)
        {
                _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(NotificationsBackgroundJob)} >>>>>>>>>> started at <<<<<<<<<<<<<<<< {DateTimeOffset.Now}");
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int count = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                count++;
                _logger.LogInformation($">>>>>>>>>>>>> Notification <<<<<<<<<<<<<<< {count} processing");
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(NotificationsBackgroundJob)} >>>>>>>>>>>> Stopped at <<<<<<<<<<<<<<<< {DateTimeOffset.Now}");
            return base.StopAsync(cancellationToken);   
        }
    }
}
