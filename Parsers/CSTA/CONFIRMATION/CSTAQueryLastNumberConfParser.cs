using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAQueryLastNumberConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAQueryLastNumberConfParser.Parse: eventType=CSTA_QUERY_LAST_NUMBER_CONF");
                logger.Info("CSTAQueryLastNumberConfParser.Parse: try to read the CSTAQueryLastNumberConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAQueryLastNumberConfEvent_t), out result))
                {
                    logger.Info("CSTAQueryLastNumberConfParser.Parse: successfully read the CSTAQueryLastNumberConfEvent_t confirmation event!");

                    CSTAQueryLastNumberConfEvent_t queryLastNumber = (CSTAQueryLastNumberConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {queryLastNumber = queryLastNumber}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAQueryLastNumberConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_QUERY_LAST_NUMBER_CONF; }
        }
    }
}
