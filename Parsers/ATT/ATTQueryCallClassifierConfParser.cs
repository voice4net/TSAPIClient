// ATTQueryCallClassifierConfParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTQueryCallClassifierConfParser : IATTEventParser
    {
        public ATTEvent_t Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                logger.Info("ATTQueryCallClassifierConfParser.Parse: eventType=ATT_QUERY_CALL_CLASSIFIER_CONF");

                object result;

                if (reader.TryReadStruct(typeof(ATTQueryCallClassifierConfEvent_t), out result))
                {
                    ATTQueryCallClassifierConfEvent_t queryCallClassifier = (ATTQueryCallClassifierConfEvent_t)result;

                    ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                    attEvent.u.queryCallClassifier = queryCallClassifier;

                    return attEvent;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTQueryCallClassifierConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.ATT_QUERY_CALL_CLASSIFIER_CONF; }
        }
    }
}
