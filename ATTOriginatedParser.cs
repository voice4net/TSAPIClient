// ATTOriginatedParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTOriginatedParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTOriginatedParser.Parse: eventType=ATT_ORIGINATED");

                object result;

                if (reader.TryReadStruct(typeof(ATTOriginatedEvent_t), out result))
                {
                    logger.Info("ATTOriginatedParser.Parse: successfully read originated event!");

                    ATTOriginatedEvent_t originatedEvent = (ATTOriginatedEvent_t)result;

                    logger.Info("ATTOriginatedParser.Parse: originatedEvent.logicalAgent.device={0}; originatedEvent.userInfo.type={1}; originated.userInfo.length={2}; originatedEvent.userInfo.data={3}; originatedEvent.consultMode={4};", originatedEvent.logicalAgent.device, originatedEvent.userInfo.type, originatedEvent.userInfo.length, originatedEvent.userInfo.data, originatedEvent.consultMode);
                    
                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.originatedEvent = originatedEvent;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTOriginatedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_ORIGINATED; }
        }
    }
}
