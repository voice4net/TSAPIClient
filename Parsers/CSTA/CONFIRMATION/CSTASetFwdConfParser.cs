using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTASetFwdConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTASetFwdConfParser.Parse: eventType=CSTA_SET_FWD_CONF");
                logger.Info("CSTASetFwdConfParser.Parse: try to read the CSTASetFwdConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTASetFwdConfEvent_t), out result))
                {
                    logger.Info("CSTASetFwdConfParser.Parse: successfully read the CSTASetFwdConfEvent_t confirmation event!");

                    CSTASetFwdConfEvent_t setFwd = (CSTASetFwdConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { setFwd = setFwd } };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTASetFwdConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_SET_FWD_CONF; }
        }
    }
}
