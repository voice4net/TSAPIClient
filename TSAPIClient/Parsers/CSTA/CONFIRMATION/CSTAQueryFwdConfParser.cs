using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAQueryFwdConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAQueryFwdConfParser.Parse: eventType=CSTA_QUERY_FWD_CONF");
                logger.Info("CSTAQueryFwdConfParser.Parse: try to read the CSTAQueryFwdConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAQueryFwdConfEvent_t), out result))
                {
                    logger.Info("CSTAQueryFwdConfParser.Parse: successfully read the CSTAQueryFwdConfEvent_t confirmation event!");

                    CSTAQueryFwdConfEvent_t queryFwd = (CSTAQueryFwdConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { queryFwd = queryFwd } };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAQueryFwdConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_QUERY_FWD_CONF; }
        }
    }
}
