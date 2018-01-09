using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTASnapshotDeviceConfParser : ICSTAConfirmationParser
    {
        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            Logger logger = LogManager.GetLogger("TSAPIClient");

            try
            {
                object result;

                logger.Info("CSTASnapshotDeviceConfParser.Parse: eventType=CSTA_SNAPSHOT_DEVICE_CONF");
                logger.Info("CSTASnapshotDeviceConfParser.Parse: try to read the CSTASnapshotDeviceConfEvent_t confirmation event...");

                if (reader.TryReadStruct(typeof(CSTASnapshotDeviceConfEvent_t), out result))
                {
                    logger.Info("CSTASnapshotDeviceConfParser.Parse: successfully read the CSTASnapshotDeviceConfEvent_t confirmation event!");

                    CSTASnapshotDeviceConfEvent_t snapshotDevice = (CSTASnapshotDeviceConfEvent_t)result;

                    CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent
                    {
                        u = {snapshotDevice = snapshotDevice}
                    };

                    return cstaConfirmation;
                }

                return null;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTASnapshotDeviceConfParser.Parse: {0}", err));
            }

            return null;
        }

        public int eventType
        {
            get { return Constants.CSTA_SNAPSHOT_DEVICE_CONF; }
        }
    }
}
