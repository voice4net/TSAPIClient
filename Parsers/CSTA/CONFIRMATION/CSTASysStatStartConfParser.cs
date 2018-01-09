using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTASysStatStartConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTASysStatStartConfParser.Parse: eventType=CSTA_SYS_STAT_START_CONF");
                logger.Info("CSTASysStatStartConfParser.Parse: try to read the CSTASysStatStartConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTASysStatStartConfEvent_t), out result))
                {
                    logger.Info("CSTASysStatStartConfParser.Parse: successfully read the CSTASysStatStartConfEvent_t confirmation event!");

                    CSTASysStatStartConfEvent_t sysStatStart = (CSTASysStatStartConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {sysStatStart = sysStatStart}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTASysStatStartConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_SYS_STAT_START_CONF; }
        }
    }
}
