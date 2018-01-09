// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public interface ICSTAConfirmationParserFactory
    {
        ICSTAConfirmationParser CreateParser(int eventType);
    }
}
