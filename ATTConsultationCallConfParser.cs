using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTConsultationCallConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTConsultationCallConfParser.Parse: eventType=ATT_CONSULTATION_CALL_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTConsultationCallConfEvent_t), out result))
                {
                    logger.Info("ATTConsultationCallConfParser.Parse: successfully read consultation call confirmation event!");

                    ATTConsultationCallConfEvent_t consultationCall = (ATTConsultationCallConfEvent_t)result;

                    logger.Info("ATTConsultationCallConfParser.Parse: consultationCall.Ucid.Value={0}", consultationCall.ucid.value);

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.consultationCall = consultationCall;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTConsultationCallConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_CONSULTATION_CALL_CONF; }
        }
    }
}
