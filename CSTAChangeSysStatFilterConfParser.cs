using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAChangeSysStatFilterConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAChangeSysStatFilterConfParser.Parse: eventType=CSTA_CHANGE_SYS_STAT_FILTER_CONF");
                logger.Info("CSTAChangeSysStatFilterConfParser.Parse: try to read the CSTAChangeSysStatFilterConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAChangeSysStatFilterConfEvent_t), out result))
                {
                    logger.Info("CSTAChangeSysStatFilterConfParser.Parse: successfully read the CSTAChangeSysStatFilterConfEvent_t confirmation event!");

                    CSTAChangeSysStatFilterConfEvent_t changeSysStatFilter = (CSTAChangeSysStatFilterConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {changeSysStatFilter = changeSysStatFilter}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAChangeSysStatFilterConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_CHANGE_SYS_STAT_FILTER_CONF; }
        }
    }
}
