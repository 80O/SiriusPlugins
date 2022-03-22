using Sirius.Api.Messaging;

namespace SuperWireds.Handlers.Messages;

/// <summary>
/// Response class to be send back.
/// </summary>
public class SuperWiredsGivePointsOnceReply : IGSPReply
{
    public bool Given;
    public void Serialize(IGSPStream stream)
    {
        stream.Serialize(ref Given);
    }
}