// ATTRouteUsedParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTRouteUsedParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTRouteUsedParser.Parse: eventType=ATT_ROUTE_USED");

                object result;

                if (reader.TryReadStruct(typeof(ATTRouteUsedEvent_t), out result))
                {
                    ATTRouteUsedEvent_t routeUsed = (ATTRouteUsedEvent_t)result;

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.routeUsed = routeUsed;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTRouteUsedParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_ROUTE_USED; }
        }
    }
}
