using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public interface ICSTAConfirmationParser
    {
        CSTAConfirmationEvent Parse(IStructReader reader);
        int eventType { get; }
    }
}
