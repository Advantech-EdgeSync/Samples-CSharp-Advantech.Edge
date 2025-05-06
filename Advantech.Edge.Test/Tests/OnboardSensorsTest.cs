using Xunit.Abstractions;

namespace Advantech.Edge.Test;

public class OnboardSensorsTest
{
    private readonly ITestOutputHelper _output;
    
    public OnboardSensorsTest(ITestOutputHelper output)
    {
        _output = output;
    }

    private void WriteLine(string message)
    {
        _output.WriteLine(message);
        Console.WriteLine(message);
    }

    [Fact]
    public void BasicTest()
    {
        WriteLine("---------------------------------------------------------");

        // Create device instance.
        var device = new Device();

        // Check if feature is supported.
        if (device.OnboardSensors.IsSupported)
        {
            // Get temperature values from all sources.
            var temperatureSrcs = Enum.GetValues<TemperatureSources>();
            var temperatureValueList = new List<(TemperatureSources, double)>();
            for (int i = 0; i < temperatureSrcs.Length; i++)
            {
                try
                {
                    var src = temperatureSrcs[i];
                    var temperature = device.OnboardSensors.GetTemperature(src);
                    temperatureValueList.Add((src, temperature));
                }
                catch (Exception) { }
            }

            // Get voltage values from all sources.
            var voltageSrcs = Enum.GetValues<VoltageSources>();
            var voltageValueList = new List<(VoltageSources, double)>();
            for (int i = 0; i < voltageSrcs.Length; i++)
            {
                try
                {
                    var src = voltageSrcs[i];
                    var voltage = device.OnboardSensors.GetVoltage(src);
                    voltageValueList.Add((src, voltage));
                }
                catch (Exception) { }
            }

            // Get fan speed values from all sources.
            var fanSpeedSrcs = Enum.GetValues<FanSources>();
            var fanSpeedValueList = new List<(FanSources, double)>();
            for (int i = 0; i < fanSpeedSrcs.Length; i++)
            {
                try
                {
                    var src = fanSpeedSrcs[i];
                    var fanSpeed = device.OnboardSensors.GetFanSpeed(src);
                    fanSpeedValueList.Add((src, fanSpeed));
                }
                catch (Exception) { }
            }

            // Show values
            WriteLine($"Feature supported : Onboard sensors.");
            WriteLine($"- Temperature soruce count : {temperatureValueList.Count}");
            foreach (var (src, temperature) in temperatureValueList)
            {
                WriteLine($"- \t{src} : {temperature} C");
            }
            WriteLine($"- Voltage soruce count : {voltageValueList.Count}");
            foreach (var (src, voltage) in voltageValueList)
            {
                WriteLine($"- \t{src} : {voltage} A");
            }
            WriteLine($"- Fan speed soruce count : {fanSpeedValueList.Count}");
            foreach (var (src, fanSpeed) in fanSpeedValueList)
            {
                WriteLine($"- \t{src} : {fanSpeed} RPM");
            }
        }
        else
        {
            WriteLine($"Feature not supported : Onboard sensors.");
        }

        WriteLine("---------------------------------------------------------");
    }
}
