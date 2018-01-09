using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAQueryMwiConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAQueryMwiConfParser.Parse: eventType=CSTA_QUERY_MWI_CONF");
                logger.Info("CSTAQueryMwiConfParser.Parse: try to read the CSTAQueryMwiConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAQueryMwiConfEvent_t), out result))
                {
                    logger.Info("CSTAQueryMwiConfParser.Parse: successfully read the CSTAQueryMwiConfEvent_t confirmation event!");

                    CSTAQueryMwiConfEvent_t queryMwi = (CSTAQueryMwiConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { queryMwi = queryMwi } };

                    return cstaConfirmation;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAQueryMwiConfParser.Parse: {0}", err));
            }          

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_QUERY_MWI_CONF; }
        }
    }
}
