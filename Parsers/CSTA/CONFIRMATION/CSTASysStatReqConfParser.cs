using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTASysStatReqConfParser : ICSTAConfirmationParser
    {
        public CSTA.CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTASysStatReqConfParser.Parse: eventType=CSTA_SYS_STAT_REQ_CONF");
                logger.Info("CSTASysStatReqConfParser.Parse: try to read the CSTASysStatReqConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTASysStatReqConfEvent_t), out result))
                {
                    logger.Info("CSTASysStatReqConfParser.Parse: successfully read the CSTASysStatReqConfEvent_t confirmation event!");

                    CSTASysStatReqConfEvent_t sysStatReq = (CSTASysStatReqConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { sysStatReq = sysStatReq } };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTASysStatReqConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_SYS_STAT_REQ_CONF; }
        }
    }
}
