using Sirius.Api.Messaging;

namespace SuperWireds.Handlers.Messages
{
    public class SuperWiredsInClientLinkNotification : IGSPMessage
    {
        public uint MessageId => 5555560;

        public uint UserId;
        public string Url;

        public void Serialize(IGSPStream stream)
        {
            stream.Serialize(ref UserId);
            stream.Serialize(ref Url);
        }

    }
}