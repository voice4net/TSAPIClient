using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTARouteRegisterCancelConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTARouteRegisterCancelConfParser.Parse: eventType=CSTA_ROUTE_REGISTER_CANCEL_CONF");
                logger.Info("CSTARouteRegisterCancelConfParser.Parse: try to read the CSTARouteRegisterCancelConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTARouteRegisterCancelConfEvent_t), out result))
                {
                    logger.Info("CSTARouteRegisterCancelConfParser.Parse: successfully read the CSTARouteRegisterCancelConfEvent_t confirmation event!");

                    CSTARouteRegisterCancelConfEvent_t routeCancel = (CSTARouteRegisterCancelConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { routeCancel = routeCancel } };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTARouteRegisterCancelConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_ROUTE_REGISTER_CANCEL_CONF; }
        }
    }
}
