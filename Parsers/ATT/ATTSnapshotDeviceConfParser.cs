// ATTSnapshotDeviceConfParser.cs
using System;
using System.Collections.Generic;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTSnapshotDeviceConfParser : IATTEventParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public ATTEvent_t Parse(IStructReader reader)
        {
            try
            {
                logger.Info("ATTSnapshotDeviceConfParser.Parse: eventType=ATT_SNAPSHOT_DEVICE_CONF");               

                ATTSnapshotDeviceConfEvent_t snapshotDevice = new ATTSnapshotDeviceConfEvent_t();

                logger.Info("ATTSnapshotDeviceConfParser.Parse: read count from stream...");
                uint count = reader.ReadUInt32();

                logger.Info("ATTSnapshotDeviceConfParser.Parse: count={0}", count);

                List<ATTSnapshotDevice_t> snapshotDeviceList = new List<ATTSnapshotDevice_t>();

                for (int i = 0; i < count; i++)
                {
                    object result;

                    logger.Info("ATTSnapshotDeviceConfParser.Parse: read snapshot device from the stream...");

                    if (reader.TryReadStruct(typeof(ATTSnapshotDevice_t), out result))
                    {
                        logger.Info("ATTSnapshotDeviceConfParser.Parse: successfully read snapshot device from the stream!");

                        ATTSnapshotDevice_t device = (ATTSnapshotDevice_t)result;

                        logger.Info("ATTSnapshotDeviceConfParser.Parse: index={0};snapshot.pSnapshotDevice.call.callID={1};snapshot.pSnapshotDevice.call.deviceID.device={2}, snapshot.pSnapshotDevice.call.devIDType={3};snapshot.pSnapshotDevice.state={4}", i, device.call.callID, device.call.deviceID.device, device.call.devIDType, device.state);

                        snapshotDeviceList.Add(device);
                    }
                }

                snapshotDevice.count = count;
                snapshotDevice.pSnapshotDevice = snapshotDeviceList.ToArray();

                ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                attEvent.u.snapshotDevice = snapshotDevice;

                return attEvent;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTSnapshotDeviceConfParser.Parse: {0}", err));
            }

            return null;      
        }

        public int eventType
        {
            get { return Constants.ATT_SNAPSHOT_DEVICE_CONF; }
        }
    }
}
