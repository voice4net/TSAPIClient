// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public interface ICSTAUnsolicitedParserFactory
    {
        ICSTAUnsolicitedParser CreateParser(int eventType);
    }
}
