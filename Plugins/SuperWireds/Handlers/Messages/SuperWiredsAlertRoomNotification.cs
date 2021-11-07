using Sirius.Api.Messaging;

namespace SuperWireds.Handlers.Messages
{
    public class SuperWiredsAlertRoomNotification : IGSPMessage
    {
        public uint MessageId => 5555558;

        public string Message;
        public uint RoomId;

        public void Serialize(IGSPStream stream)
        {
            stream.Serialize(ref Message);
            stream.Serialize(ref RoomId);
        }
    }
}