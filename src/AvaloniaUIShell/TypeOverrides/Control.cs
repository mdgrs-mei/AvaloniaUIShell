using AvaloniaUIShell.Common;

namespace AvaloniaUIShell.Avalonia.Controls;

public partial class Control
{
    private const string _accessorClassName = "AvaloniaUIShell.Server.ControlAccessor, AvaloniaUIShell.Server";

    public object? FindControl(string name)
    {
        return CommandClient.Get().InvokeStaticMethodAndGetResult<object>(
            _accessorClassName,
            "FindControl",
            AvaloniaUIShellObjectId,
            name);
    }
}
