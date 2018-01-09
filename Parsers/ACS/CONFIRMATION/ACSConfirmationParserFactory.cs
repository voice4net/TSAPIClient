using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ACSConfirmationParserFactory : IACSConfirmationParserFactory
    {
        private readonly Dictionary<int, IACSConfirmationParser> m_ACSConfParsers = new Dictionary<int, IACSConfirmationParser>();

        public ACSConfirmationParserFactory()
        {
            LoadTypes();
        }

        private void LoadTypes()
        {
            Assembly asm = typeof(IACSConfirmationParser).Assembly;

            asm.GetTypes().Where(type => typeof(IACSConfirmationParser).IsAssignableFrom(type) && type.IsClass).ToList().ForEach(type =>
            {
                object result = Activator.CreateInstance(type);

                var parser = result as IACSConfirmationParser;

                if (parser != null)
                {
                    if (!m_ACSConfParsers.ContainsKey(parser.eventType))
                    {
                        m_ACSConfParsers.Add(parser.eventType, parser);
                    }
                }
            });
        }

        public IACSConfirmationParser CreateParser(int eventType)
        {
            return m_ACSConfParsers.ContainsKey(eventType) ? m_ACSConfParsers[eventType] : null;
        }
    }
}
