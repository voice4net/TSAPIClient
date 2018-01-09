using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLog;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTEventParserFactory : IATTEventParserFactory
    {
        private readonly Dictionary<int, IATTEventParser> m_ATTEventParsers = new Dictionary<int, IATTEventParser>();
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public ATTEventParserFactory()
        {
            LoadTypes();
        }

        private void LoadTypes()
        {
            logger.Info("ATTEventParserFactory.LoadTypes: get assembly...");

            Assembly asm = typeof(IATTEventParser).Assembly;

            /*
            int foo = sizeof(ushort);

            logger.Info("ATTEventParserFactory.LoadTypes: load types...");

            try
            {
                var types = asm.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();

                Console.WriteLine(  errorMessage);
                //Display or log the error based on your application.
            }
            */

            asm.GetTypes().Where(type => typeof(IATTEventParser).IsAssignableFrom(type) && type.IsClass).ToList().ForEach(type =>
            {
                logger.Info("ATTEventParserFactory.LoadTypes: Type={0}", type.FullName);

                logger.Info("ATTEventParserFactory.LoadTypes: create instance...");
                object result = Activator.CreateInstance(type);

                var parser = result as IATTEventParser;

                if (parser != null)
                {
                    logger.Info("ATTEventParserFactory.LoadTypes: eventType={0}", parser.eventType);

                    if (!m_ATTEventParsers.ContainsKey(parser.eventType))
                    {
                        logger.Info("ATTEventParserFactory.LoadTypes: add parser to dictionary...");
                        m_ATTEventParsers.Add(parser.eventType, parser);
                    }
                }
            });
        }

        public IATTEventParser CreateParser(int eventType)
        {
            if (m_ATTEventParsers.ContainsKey(eventType))
            {
                return m_ATTEventParsers[eventType];
            }

            return null;
        }
    }
}
