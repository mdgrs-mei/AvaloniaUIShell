using AvaloniaUIShell.Common;

namespace AvaloniaUIShell;

public sealed class AvaloniaUIShellObject : IAvaloniaUIShellObject
{
    public ObjectId AvaloniaUIShellObjectId { get; } = new();

    internal AvaloniaUIShellObject(ObjectId id)
    {
        AvaloniaUIShellObjectId = id;
    }
}
