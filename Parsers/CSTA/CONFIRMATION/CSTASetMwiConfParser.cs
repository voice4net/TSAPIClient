using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTASetMwiConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTASetMwiConfParser.Parse: eventType=CSTA_SET_MWI_CONF");
                logger.Info("CSTASetMwiConfParser.Parse: try to read the CSTASetMwiConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTASetMwiConfEvent_t), out result))
                {
                    logger.Info("CSTASetMwiConfParser.Parse: successfully read the CSTASetMwiConfEvent_t confirmation event!");

                    CSTASetMwiConfEvent_t setMwi = (CSTASetMwiConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { setMwi = setMwi } };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTASetMwiConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_SET_MWI_CONF; }
        }
    }
}
