using Xunit.Abstractions;
using Advantech.Edge.IFeatures;

namespace Advantech.Edge.Test;

public class GpioTest(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    private void WriteLine(string message)
    {
        _output.WriteLine(message);
        Console.WriteLine(message);
    }

    [Fact]
    public void ReadPinStates()
    {
        WriteLine("---------------------------------------------------------");

        // Create device instance.
        var device = new Device();

        // Check if feature is supported.
        if (device.Gpio.IsSupported)
        {
            // Get states of all GPIO pins.
            uint maxPinindex = device.Gpio.MaxPinIndex;
            var pinStateTuples = new (GpioDirectionTypes Dir, GpioLevelTypes Level)[maxPinindex + 1];
            for (var pinIndex = 0u; pinIndex < pinStateTuples.Length; pinIndex++)
            {
                var dir = device.Gpio.GetDirection(pinIndex);
                var level = device.Gpio.GetLevel(pinIndex);
                pinStateTuples[pinIndex] = (dir, level);
            }

            // Print states of all GPIO pins.
            WriteLine($"Feature supported : GPIO");
            WriteLine($"- Max pin index : {maxPinindex}");
            for (var pinIndex = 0u; pinIndex < pinStateTuples.Length; pinIndex++)
            {
                var (Dir, Level) = pinStateTuples[pinIndex];
                WriteLine($"- \tPin {pinIndex}, direction : {Dir}, level : {Level}");
            }
        }
        else
        {
            WriteLine($"Feature not supported : GPIO");
        }

        WriteLine("---------------------------------------------------------");
    }

    [Fact]
    public void SetOutputHighPin0()
    {
        WriteLine("---------------------------------------------------------");

        // Create device instance.
        var device = new Device();

        if (device.Gpio.IsSupported)
        {
            WriteLine($"Feature supported : GPIO");

            // Initialize pin index to 0.
            var pinIndex = 0u;

            // If current direction of pin is not output mode, set mode.
            var currentDir = device.Gpio.GetDirection(pinIndex);
            if (currentDir != GpioDirectionTypes.Output)
            {
                device.Gpio.SetDirection(pinIndex, GpioDirectionTypes.Output);
                WriteLine($"Set pin {pinIndex} to output mode.");
            }

            // Set level of pin to HIGH.
            try
            {
                device.Gpio.SetLevel(pinIndex, GpioLevelTypes.High);
                WriteLine($"Success : Set pin {pinIndex} to HIGH");
            }
            catch (Exception e) { WriteLine($"Fail : {e.Message}"); }
        }
        else
        {
            WriteLine($"Feature not supported : GPIO");
        }

        WriteLine("---------------------------------------------------------");
    }
}
