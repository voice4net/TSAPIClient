using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ACSUnsolicitedParserFactory : IACSUnsolicitedParserFactory
    {
        private readonly Dictionary<int, IACSUnsolicitedParser> m_ACSUnsolicitedParsers = new Dictionary<int, IACSUnsolicitedParser>();       

        public ACSUnsolicitedParserFactory()
        {
            LoadTypes();
        }

        private void LoadTypes()
        {
            Assembly asm = typeof(IACSUnsolicitedParser).Assembly;

            asm.GetTypes().Where(type => typeof(IACSUnsolicitedParser).IsAssignableFrom(type) && type.IsClass).ToList().ForEach(type =>
            {
                object result = Activator.CreateInstance(type);

                var parser = result as IACSUnsolicitedParser;

                if (parser != null)
                {
                    if (!m_ACSUnsolicitedParsers.ContainsKey(parser.eventType))
                    {
                        m_ACSUnsolicitedParsers.Add(parser.eventType, parser);
                    }
                }
            });
        }

        public IACSUnsolicitedParser CreateParser(int eventType)
        {
            return m_ACSUnsolicitedParsers.ContainsKey(eventType) ? m_ACSUnsolicitedParsers[eventType] : null;
        }
    }
}
