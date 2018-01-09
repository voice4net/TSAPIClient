using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public interface IATTEventParser
    {
        ATTEvent_t Parse(IStructReader reader);
        int eventType { get; }
    }
}
