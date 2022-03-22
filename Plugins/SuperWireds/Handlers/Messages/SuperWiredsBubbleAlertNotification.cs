using Sirius.Api.Messaging;

namespace SuperWireds.Handlers.Messages;

public class SuperWiredsBubbleAlertNotification : IGSPMessage
{
    public uint MessageId => 5555559;

    public uint UserId;
    public string Title = string.Empty;
    public string Type = string.Empty;
    public string Message = string.Empty;

    public void Serialize(IGSPStream stream)
    {
        stream.Serialize(ref UserId);
        stream.Serialize(ref Title);
        stream.Serialize(ref Type);
        stream.Serialize(ref Message);
    }
}