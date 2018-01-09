using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAConfirmationParserFactory : ICSTAConfirmationParserFactory
    {
        private readonly Dictionary<int, ICSTAConfirmationParser> m_CSTAConfParsers = new Dictionary<int, ICSTAConfirmationParser>();

        public CSTAConfirmationParserFactory()
        {
            LoadTypes();
        }

        private void LoadTypes()
        {
            Assembly asm = typeof(ICSTAConfirmationParser).Assembly;

            asm.GetTypes().Where(type => typeof(ICSTAConfirmationParser).IsAssignableFrom(type) && type.IsClass).ToList().ForEach(type =>
            {
                object result = Activator.CreateInstance(type);

                ICSTAConfirmationParser parser = result as ICSTAConfirmationParser;

                if (parser != null)
                {
                    if (!m_CSTAConfParsers.ContainsKey(parser.eventType))
                    {
                        m_CSTAConfParsers.Add(parser.eventType, parser);
                    }
                }
            });
        }

        public ICSTAConfirmationParser CreateParser(int eventType)
        {
            if (m_CSTAConfParsers.ContainsKey(eventType))
            {
                return m_CSTAConfParsers[eventType];
            }

            return null;
        }
    }
}
