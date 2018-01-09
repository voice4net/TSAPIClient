using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAUnsolicitedParserFactory: ICSTAUnsolicitedParserFactory
    {
        private readonly Dictionary<int, ICSTAUnsolicitedParser> m_CSTAUnsolicitedParsers = new Dictionary<int, ICSTAUnsolicitedParser>();       

        public CSTAUnsolicitedParserFactory()
        {
            LoadTypes();
        }

        private void LoadTypes()
        {
            Assembly asm = typeof(ICSTAUnsolicitedParser).Assembly;

            asm.GetTypes().Where(type => typeof(ICSTAUnsolicitedParser).IsAssignableFrom(type) && type.IsClass).ToList().ForEach(type =>
            {
                object result = Activator.CreateInstance(type);

                ICSTAUnsolicitedParser parser = result as ICSTAUnsolicitedParser;

                if (parser != null)
                {       
                    if (!m_CSTAUnsolicitedParsers.ContainsKey(parser.eventType))
                    {
                        m_CSTAUnsolicitedParsers.Add(parser.eventType, parser);
                    }
                }
            });
        }

        public ICSTAUnsolicitedParser CreateParser(int eventType)
        {
            if (m_CSTAUnsolicitedParsers.ContainsKey(eventType))
            {
                return m_CSTAUnsolicitedParsers[eventType];
            }

            return null;
        }
    }
}
