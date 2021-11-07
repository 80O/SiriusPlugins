using Sirius.Api.Messaging;

namespace SuperWireds.Handlers.Messages
{
    public class SuperWiredsResetNotification : IGSPMessage
    {
        public uint MessageId => 5555556;

        public uint ItemId;
        public void Serialize(IGSPStream stream)
        {
            stream.Serialize(ref ItemId);
        }
    }
}