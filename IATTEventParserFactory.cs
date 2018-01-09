 // ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public interface IATTEventParserFactory
    {
        IATTEventParser CreateParser(int eventType);
    }
}
