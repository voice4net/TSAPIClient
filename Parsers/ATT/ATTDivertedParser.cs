// ATTDivertedParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTDivertedParser : IATTEventParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public ATTEvent_t Parse(IStructReader reader)
        {
            try
            {
                logger.Info("ATTDivertedParser.Parse: eventType=ATT_DIVERTED");

                ATTDivertedEvent_t divertedEvent = new ATTDivertedEvent_t { deviceHistory = ReadDeviceHistory(reader) };

                ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                attEvent.u.divertedEvent = divertedEvent;

                return attEvent;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDivertedParser.Parse: {0}", err));
            }

            return null;
        }

        private DeviceHistory_t ReadDeviceHistory(IStructReader reader)
        {
            try
            {
                DeviceHistory_t deviceHistory = new DeviceHistory_t();

                logger.Info("ATTDivertedParser.ReadDeviceHistory: read device history count from stream...");
                uint count = reader.ReadUInt32();

                logger.Info("ATTDivertedParser.ReadDeviceHistory: count={0}", count);

                if (count == 1)
                {
                    reader.BaseStream.Position += 4;

                    object result;

                    logger.Info("ATTDivertedParser.ReadDeviceHistory: try to read a device history entry from the stream...");

                    if (reader.TryReadStruct(typeof(DeviceHistoryEntry_t), out result))
                    {
                        logger.Info("ATTDivertedParser.ReadDeviceHistory: successfully read device history entry from stream!");

                        DeviceHistoryEntry_t deviceHistoryEntry = (DeviceHistoryEntry_t)result;

                        logger.Info("ATTDivertedParser.ReadDeviceHistory: deviceHistoryEntry.olddeviceID={0};deviceHistoryEntry.oldconnectionID.callID={1};deviceHistoryEntry.oldconnectionID.deviceID={2};", deviceHistoryEntry.olddeviceID.device, deviceHistoryEntry.oldconnectionID.callID, deviceHistoryEntry.oldconnectionID.deviceID.device);

                        deviceHistory = new DeviceHistory_t()
                        {
                            count = 1,
                            deviceHistoryList = new DeviceHistoryEntry_t[] { deviceHistoryEntry }
                        };
                    }
                }
                else
                {
                    deviceHistory = new DeviceHistory_t()
                    {
                        count = 0,
                        deviceHistoryList = new DeviceHistoryEntry_t[] { }
                    };
                }

                return deviceHistory;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTDivertedParser.ReadDeviceHistory: {0}", err));
            }

            return new DeviceHistory_t() { count = 0, deviceHistoryList = new DeviceHistoryEntry_t[] { } };
        }

        public int eventType
        {
            get { return Constants.ATT_DIVERTED; }
        }
    }
}
