// ATTSingleStepTransferCallConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTSingleStepTransferCallConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTSingleStepTransferCallConfParser.Parse: eventType=ATT_SINGLE_STEP_TRANSFER_CALL_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTSingleStepTransferCallConfEvent_t), out result))
                {
                    ATTSingleStepTransferCallConfEvent_t ssTransferCallConf = (ATTSingleStepTransferCallConfEvent_t)result;

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.ssTransferCallConf = ssTransferCallConf;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTSingleStepTransferCallConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_SINGLE_STEP_TRANSFER_CALL_CONF; }
        }
    }
}
