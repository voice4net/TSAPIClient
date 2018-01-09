using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAQueryDndConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAQueryDndConfParser.Parse: eventType=CSTA_QUERY_DND_CONF");
                logger.Info("CSTAQueryDndConfParser.Parse: try to read the CSTAQueryDndConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAQueryDndConfEvent_t), out result))
                {
                    logger.Info("CSTAQueryDndConfParser.Parse: successfully read the CSTAQueryDndConfEvent_t confirmation event!");

                    CSTAQueryDndConfEvent_t queryDnd = (CSTAQueryDndConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { queryDnd = queryDnd } };

                    return cstaConfirmation;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAQueryDndConfParser.Parse: {0}", err));
            }          

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_QUERY_DND_CONF; }
        }
    }
}
