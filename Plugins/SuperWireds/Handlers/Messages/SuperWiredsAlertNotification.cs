using Sirius.Api.Messaging;

namespace SuperWireds.Handlers.Messages
{
    public class SuperWiredsAlertNotification : IGSPMessage
    {
        public uint MessageId => 5555557;

        public uint UserId;
        public string Message;

        public void Serialize(IGSPStream stream)
        {
            stream.Serialize(ref UserId);
            stream.Serialize(ref Message);
        }
    }
}