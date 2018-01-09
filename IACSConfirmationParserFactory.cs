 // ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public interface IACSConfirmationParserFactory
    {
        IACSConfirmationParser CreateParser(int eventType);
    }
}
