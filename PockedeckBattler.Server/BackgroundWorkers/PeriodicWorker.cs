namespace PockedeckBattler.Server.BackgroundWorkers;

public abstract class PeriodicWorker : BackgroundService
{
    readonly PeriodicTimer _timer;

    protected PeriodicWorker(TimeSpan period)
    {
        _timer = new PeriodicTimer(period);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            await Work(stoppingToken);
        }
    }

    protected abstract Task Work(CancellationToken cancellationToken);
}
