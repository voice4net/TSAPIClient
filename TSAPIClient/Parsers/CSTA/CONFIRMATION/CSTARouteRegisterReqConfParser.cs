using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTARouteRegisterReqConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTARouteRegisterReqConfParser.Parse: eventType=CSTA_ROUTE_REGISTER_REQ_CONF");
                logger.Info("CSTARouteRegisterReqConfParser.Parse: try to read the CSTARouteRegisterReqConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTARouteRegisterReqConfEvent_t), out result))
                {
                    logger.Info("CSTARouteRegisterReqConfParser.Parse: successfully read the CSTARouteRegisterReqConfEvent_t confirmation event!");

                    CSTARouteRegisterReqConfEvent_t routeRegister = (CSTARouteRegisterReqConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {routeRegister = routeRegister}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTARouteRegisterReqConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_ROUTE_REGISTER_REQ_CONF; }
        }
    }
}
