using Coravel;
using CoravelScheduledWorker;

var builder = Host.CreateApplicationBuilder(args);
// builder.Services.AddHostedService<Worker>(); -- Not needed
builder.Services.AddScheduler();
builder.Services.AddSingleton<WorkerInvocable>();

var host = builder.Build();
ILogger logger = host.Services.GetRequiredService<ILogger<Program>>();
host.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<WorkerInvocable>()
    .EveryFiveSeconds()
    .RunOnceAtStart()
    .PreventOverlapping(nameof(WorkerInvocable));
}).OnError((exception) =>
    logger.LogCritical(exception, "Error with scheduled task")
);
host.Run();
