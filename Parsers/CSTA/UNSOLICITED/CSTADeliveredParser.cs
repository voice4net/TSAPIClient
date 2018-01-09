// CSTADeliveredParser.cs
using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTADeliveredParser : ICSTAUnsolicitedParser
    {
        public CSTAUnsolicitedEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTADeliveredParser.Parse: eventType=CSTA_DELIVERED");
                logger.Info("CSTADeliveredParser.Parse: try to read the CSTADeliveredEvent_t unsolicited event...");

                if (reader.TryReadStruct(typeof(CSTADeliveredEvent_t), out result))
                {
                    logger.Info("CSTADeliveredParser.Parse: successfully read the CSTADeliveredEvent_t unsolicited event!");

                    CSTADeliveredEvent_t delivered = (CSTADeliveredEvent_t)result;

                    logger.Info("CSTADeliveredParser.Parse: connection.callID={0};connection.deviceID.device={1};connection.devIDType={2};alertingDevice.deviceID.device={3};alertingDevice.deviceIDStatus={4};alertingDevice.deviceIDType={5};callingDevice.deviceID.device={6};callingDevice.deviceIDStatus={7};callingDevice.deviceIDType={8};calledDevice.deviceID.device={9};calledDevice.deviceIDStatus={10};calledDevice.deviceIDType={11};cause={12}", delivered.connection.callID, delivered.connection.deviceID.device, delivered.connection.devIDType, delivered.alertingDevice.deviceID.device, delivered.alertingDevice.deviceIDStatus, delivered.alertingDevice.deviceIDType, delivered.callingDevice.deviceID.device, delivered.callingDevice.deviceIDStatus, delivered.callingDevice.deviceIDType, delivered.calledDevice.deviceID.device, delivered.calledDevice.deviceIDStatus, delivered.calledDevice.deviceIDType, delivered.cause);

                    CSTAUnsolicitedEvent cstaUnsolicited = new CSTAUnsolicitedEvent { u = { delivered = delivered } };

                    return cstaUnsolicited;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTADeliveredParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_DELIVERED; }
        }
    }
}
