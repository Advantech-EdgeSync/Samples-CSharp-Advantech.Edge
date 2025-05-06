using Xunit.Abstractions;

namespace Advantech.Edge.Test;

public class WatchdogTest(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    private void WriteLine(string message)
    {
        _output.WriteLine(message);
        Console.WriteLine(message);
    }

    [Fact]
    public async Task BasicTest()
    {
        WriteLine("---------------------------------------------------------");

        // Create device instance.
        var device = new Device();

        // Check if feature is supported.
        if (device.Watchdog.IsSupported)
        {
            // Get capabilities.
            var maxDelay = device.Watchdog.MaxDelay;
            var maxEventTimeout = device.Watchdog.MaxEventTimeout;
            var maxResetTimeout = device.Watchdog.MaxResetTimeout;

            // Start timer.
            device.Watchdog.StartTimer(maxDelay, maxEventTimeout, maxResetTimeout);

            // Trigger watchdog timer periodically until process is canceled.
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, e) => { cts.Cancel(); };
            while (!cts.Token.IsCancellationRequested)
            {
                // Trigger timer.
                device.Watchdog.TriggerTimer();
                
                // Print messages.
                WriteLine($"Trigger watchdog timer.");

                // Wait 1 second.
                await Task.Delay(1000);
            }

            // Stop watchdog timer.
            device.Watchdog.StopTimer();
        }
        else
        {
            WriteLine($"Feature not supported : Watchdog");
        }

        WriteLine("---------------------------------------------------------");
    }
}
