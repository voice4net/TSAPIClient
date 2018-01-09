// ATTQueryStationStatusConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTQueryStationStatusConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTQueryStationStatusConfParser.Parse: eventType=ATT_QUERY_STATION_STATUS_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTQueryStationStatusConfEvent_t), out result))
                {
                    ATTQueryStationStatusConfEvent_t queryStationStatus = (ATTQueryStationStatusConfEvent_t)result;

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.queryStationStatus = queryStationStatus;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTQueryStationStatusConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_QUERY_STATION_STATUS_CONF; }
        }
    }
}
