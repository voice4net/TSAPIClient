using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTASetDndConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTASetDndConfParser.Parse: eventType=CSTA_SET_DND_CONF");
                logger.Info("CSTASetDndConfParser.Parse: try to read the CSTASetDndConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTASetDndConfEvent_t), out result))
                {
                    logger.Info("CSTASetDndConfParser.Parse: successfully read the CSTASetDndConfEvent_t confirmation event!");

                    CSTASetDndConfEvent_t setDnd = (CSTASetDndConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { setDnd = setDnd } };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTASetDndConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_SET_DND_CONF; }
        }
    }
}
