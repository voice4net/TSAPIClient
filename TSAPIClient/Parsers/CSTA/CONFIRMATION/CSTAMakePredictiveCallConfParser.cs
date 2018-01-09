using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAMakePredictiveCallConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTAMakePredictiveCallConfParser.Parse: eventType=CSTA_MAKE_PREDICTIVE_CALL_CONF");
                logger.Info("CSTAMakePredictiveCallConfParser.Parse: try to read the CSTAMakePredictiveCallConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAMakePredictiveCallConfEvent_t), out result))
                {
                    logger.Info("CSTAMakePredictiveCallConfParser.Parse: successfully read the CSTAMakePredictiveCallConfEvent_t confirmation event!");

                    CSTAMakePredictiveCallConfEvent_t makePredictiveCall = (CSTAMakePredictiveCallConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {makePredictiveCall = makePredictiveCall}
                    };

                    return cstaConfirmation;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAMakePredictiveCallConfParser.Parse: {0}", err));
            }          

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_MAKE_PREDICTIVE_CALL_CONF; }
        }
    }
}
