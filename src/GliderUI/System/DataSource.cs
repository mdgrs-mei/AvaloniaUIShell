using System.Dynamic;
using GliderUI.Common;

namespace GliderUI;

public class DataSource : DynamicObject, IGliderUIObject
{
    private readonly HashSet<string> _memberNames = [];
    private readonly HashSet<string> _originalMemberNames = [];

    public ObjectId GliderUIObjectId { get; protected set; } = new();

    public DataSource()
    {
        GliderUIObjectId = CommandClient.Get().CreateObject(
            "GliderUI.Server.DataSource, GliderUI.Server",
            this);
    }

    public override IEnumerable<string> GetDynamicMemberNames()
    {
        return _originalMemberNames.AsEnumerable();
    }

    public override bool TryGetMember(
        GetMemberBinder binder, out object? result)
    {
        ArgumentNullException.ThrowIfNull(binder);

        // case-insensitive.
        string memberName = binder.Name.ToUpperInvariant();
        if (_memberNames.Contains(memberName))
        {
            result = GetMember(memberName);
            return true;
        }
        else
        {
            result = null;
            return false;
        }
    }

    public override bool TrySetMember(
        SetMemberBinder binder, object? value)
    {
        ArgumentNullException.ThrowIfNull(binder);

        string memberName = binder.Name.ToUpperInvariant();
        _ = _memberNames.Add(memberName);
        _ = _originalMemberNames.Add(binder.Name);

        SetMember(memberName, value);
        return true;
    }

    private object? GetMember(string memberName)
    {
        object? result = CommandClient.Get().InvokeMethodAndGetResult<object?>(
            GliderUIObjectId,
            "GliderUI.Server.DataSource, GliderUI.Server",
            "GetMember",
            memberName);

        return result;
    }

    private void SetMember(string memberName, object? value)
    {
        CommandClient.Get().InvokeMethod(
            GliderUIObjectId,
            "GliderUI.Server.DataSource, GliderUI.Server",
            "SetMember",
            memberName,
            value);
    }
}
