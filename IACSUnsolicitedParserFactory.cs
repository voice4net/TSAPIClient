 // ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public interface IACSUnsolicitedParserFactory
    {
        IACSUnsolicitedParser CreateParser (int eventType);
    }
}
