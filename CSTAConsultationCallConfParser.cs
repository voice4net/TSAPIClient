using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAConsultationCallConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAConsultationCallConfParser.Parse: eventType=CSTA_CONSULTATION_CALL_CONF");
                logger.Info("CSTAConsultationCallConfParser.Parse: try to read the CSTAConsultationCallConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAConsultationCallConfEvent_t), out result))
                {
                    logger.Info("CSTAConsultationCallConfParser.Parse: successfully read the CSTAConsultationCallConfEvent_t confirmation event!");

                    CSTAConsultationCallConfEvent_t consultationCall = (CSTAConsultationCallConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {consultationCall = consultationCall}
                    };

                    return cstaConfirmation;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAConsultationCallConfParser.Parse: {0}", err));
            }          

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_CONSULTATION_CALL_CONF; }
        }
    }
}
