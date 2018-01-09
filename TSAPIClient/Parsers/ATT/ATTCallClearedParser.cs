// ATTCallClearedParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTCallClearedParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("ATTCallClearedParser.Parse: eventType=ATT_CALL_CLEARED");
                logger.Info("ATTCallClearedParser.Parse: try to read the ATTCallClearedEvent_t event...");

                if (reader.TryReadStruct(typeof(ATTCallClearedEvent_t), out result))
                {
                    logger.Info("ATTCallClearedParser.Parse: successfully read the ATTCallClearedEvent_t event!");

                    ATTCallClearedEvent_t callClearedEvent = (ATTCallClearedEvent_t)result;

                    logger.Info("ATTCallClearedParser.Parse: reason={0}", callClearedEvent.reason);

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.callClearedEvent = callClearedEvent;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTCallClearedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_CALL_CLEARED; }
        }
    }
}