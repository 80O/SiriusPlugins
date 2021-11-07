using Sirius.Api.Messaging;

namespace SuperWireds.Handlers.Messages
{
    /// <summary>
    /// Request class we send to Sirius.
    /// </summary>
    public class SuperWiredsGivePointsOnceRequest : IGSPMessage
    {
        public uint MessageId => 5555555;

        public uint ItemId;
        public uint UserId;
        public uint PointsType;
        public int PointsAmount;

        public void Serialize(IGSPStream stream)
        {
            stream.Serialize(ref ItemId);
            stream.Serialize(ref UserId);
            stream.Serialize(ref PointsType);
            stream.Serialize(ref PointsAmount);
        }
    }
}