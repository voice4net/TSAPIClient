// ATTChargeAdviceParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTChargeAdviceParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTChargeAdviceParser.Parse: eventType=ATT_CHARGE_ADVICE");

                object result;

                if (reader.TryReadStruct(typeof(ATTChargeAdviceEvent_t), out result))
                {
                    ATTChargeAdviceEvent_t chargeAdviceEvent = (ATTChargeAdviceEvent_t)result;

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.chargeAdviceEvent = chargeAdviceEvent;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTChargeAdviceParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_CHARGE_ADVICE; }
        }
    }
}

