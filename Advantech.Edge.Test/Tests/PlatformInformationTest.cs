
using Xunit.Abstractions;

namespace Advantech.Edge.Test;

public class PlatformInformationTest(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

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

        // Get fields of platform information.
        var boardName = device.PlatformInformation.MotherboardName;
        var manufacturer = device.PlatformInformation.Manufacturer;
        var biosRevision = device.PlatformInformation.BiosRevision;
        var driverVersion = device.PlatformInformation.DriverVersion;
        var libraryVersion = device.PlatformInformation.LibraryVersion;
        var dmiInfo = device.PlatformInformation.DmiInfo;

        // Print fields of platform information.
        WriteLine($"Feature supported : Platform Information.");
        WriteLine($"- Board name : {boardName}");
        WriteLine($"- Manufacturer : {manufacturer}");
        WriteLine($"- BIOS Revision : {biosRevision}");
        if (driverVersion is not null)
            WriteLine($"- Driver Version : {driverVersion}");
        if (libraryVersion is not null)
            WriteLine($"- Library Version : {libraryVersion}");
        WriteLine($"- DMI information existence : {dmiInfo is not null}");
        if (dmiInfo is not null)
        {
            if (dmiInfo!.BiosVendor is not null) WriteLine($"- \tBIOS vendor ({dmiInfo!.BiosVendor?.DisplayName}) : {dmiInfo.BiosVendor?.Value}");
            if (dmiInfo!.BiosVersion is not null) WriteLine($"- \tBIOS version ({dmiInfo.BiosVersion?.DisplayName}) : {dmiInfo.BiosVersion?.Value}");
            if (dmiInfo!.BiosReleaseDate is not null) WriteLine($"- \tBIOS date ({dmiInfo.BiosReleaseDate?.DisplayName}) : {dmiInfo.BiosReleaseDate?.Value}");
            if (dmiInfo!.SysUuid is not null) WriteLine($"- \tSystem UUID ({dmiInfo.SysUuid?.DisplayName}) : {dmiInfo.SysUuid?.Value}");
            if (dmiInfo!.SysVendor is not null) WriteLine($"- \tSystem vendor ({dmiInfo.SysVendor?.DisplayName}) : {dmiInfo.SysVendor?.Value}");
            if (dmiInfo!.SysProduct is not null) WriteLine($"- \tSystem product ({dmiInfo.SysProduct?.DisplayName}) : {dmiInfo.SysProduct?.Value}");
            if (dmiInfo!.SysVersion is not null) WriteLine($"- \tSystem version ({dmiInfo.SysVersion?.DisplayName}) : {dmiInfo.SysVersion?.Value}");
            if (dmiInfo!.SysSerial is not null) WriteLine($"- \tSystem serial ({dmiInfo.SysSerial?.DisplayName}) : {dmiInfo.SysSerial?.Value}");
            if (dmiInfo!.BoardVendor is not null) WriteLine($"- \tBoard vendor ({dmiInfo.BoardVendor?.DisplayName}) : {dmiInfo.BoardVendor?.Value}");
            if (dmiInfo!.BoardName is not null) WriteLine($"- \tBoard name ({dmiInfo.BoardName?.DisplayName}) : {dmiInfo.BoardName?.Value}");
            if (dmiInfo!.BoardVersion is not null) WriteLine($"- \tBoard version ({dmiInfo.BoardVersion?.DisplayName}) : {dmiInfo.BoardVersion?.Value}");
            if (dmiInfo!.BoardSerial is not null) WriteLine($"- \tBoard serial ({dmiInfo.BoardSerial?.DisplayName}) : {dmiInfo.BoardSerial?.Value}");
            if (dmiInfo!.BoardAssetTag is not null) WriteLine($"- \tBoard asset tag ({dmiInfo.BoardAssetTag?.DisplayName}) : {dmiInfo.BoardAssetTag?.Value}");
        }

        WriteLine("---------------------------------------------------------");
    }
}
