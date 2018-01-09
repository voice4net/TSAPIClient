using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTAAnswerCallConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;                

                logger.Info("CSTAAnswerCallConfParser.Parse: eventType=CSTA_ANSWER_CALL_CONF");
                logger.Info("CSTAAnswerCallConfParser.Parse: try to read the CSTAAnswerCallConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTAAnswerCallConfEvent_t), out result))
                {
                    logger.Info("CSTAAnswerCallConfParser.Parse: successfully read the CSTAAnswerCallConfEvent_t confirmation event!");

                    CSTAAnswerCallConfEvent_t answerCall = (CSTAAnswerCallConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { answerCall = answerCall } };

                    return cstaConfirmation;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTAAnswerCallConfParser.Parse: {0}", err));
            }

            return null;            
        }

        public int eventType
        {
            get { return Constants.CSTA_ANSWER_CALL_CONF; }
        }
    }
}
