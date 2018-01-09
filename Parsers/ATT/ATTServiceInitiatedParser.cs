// ATTServiceInitiatedParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTServiceInitiatedParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTServiceInitiatedParser.Parse: eventType=ATT_SERVICE_INITIATED");

                object result;

                if (reader.TryReadStruct(typeof(ATTServiceInitiatedEvent_t), out result))
                {
                    logger.Info("ATTServiceInitiatedParser.Parse: successfully read service initiated event!");

                    ATTServiceInitiatedEvent_t serviceInitiated = (ATTServiceInitiatedEvent_t)result;

                    logger.Info("ATTServiceInitiatedParser.Parse: serviceInitiated.ucid.value={0}; serviceInitiated.consultMode={1};", serviceInitiated.ucid.value, serviceInitiated.consultMode);

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.serviceInitiated = serviceInitiated;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTServiceInitiatedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_SERVICE_INITIATED; }
        }
    }
}
