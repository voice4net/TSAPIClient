// ATTFailedParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

namespace TSAPIClient.Parsers.ATT
{
    public class ATTFailedParser : IATTEventParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public ATTEvent_t Parse(IStructReader reader)
        {  
            try
            {
                logger.Info("ATTFailedParser.Parse: eventType=ATT_FAILED");

                ATTFailedEvent_t failedEvent = new ATTFailedEvent_t
                {
                    deviceHistory = ReadDeviceHistory(reader),
                    callingDevice = ReadCallingDevice(reader)
                };

                ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                attEvent.u.failedEvent = failedEvent;

                return attEvent;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTFailedParser.Parse: {0}", err));
            }

            return null;
        }

        private DeviceHistory_t ReadDeviceHistory(IStructReader reader)
        {
            try
            {
                DeviceHistory_t deviceHistory = new DeviceHistory_t();

                logger.Info("ATTFailedParser.ReadDeviceHistory: read device history count from stream...");
                uint count = reader.ReadUInt32();

                logger.Info("ATTFailedParser.ReadDeviceHistory: count={0}", count);

                if (count == 1)
                {
                    reader.BaseStream.Position += 4;

                    object result;

                    logger.Info("ATTFailedParser.ReadDeviceHistory: try to read a device history entry from the stream...");
                    
                    if (reader.TryReadStruct(typeof(DeviceHistoryEntry_t), out result))
                    {
                        logger.Info("ATTFailedParser.ReadDeviceHistory: successfully read device history entry from stream!");

                        DeviceHistoryEntry_t deviceHistoryEntry = (DeviceHistoryEntry_t)result;

                        logger.Info("ATTFailedParser.ReadDeviceHistory: deviceHistoryEntry.olddeviceID={0};deviceHistoryEntry.cause={1};deviceHistoryEntry.oldconnectionID.callID={2};deviceHistoryEntry.oldconnectionID.deviceID={3};", deviceHistoryEntry.olddeviceID.device, deviceHistoryEntry.cause, deviceHistoryEntry.oldconnectionID.callID, deviceHistoryEntry.oldconnectionID.deviceID.device);

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
                logger.Error(string.Format("Error in ATTFailedParser.ReadDeviceHistory: {0}", err));
            }

            return new DeviceHistory_t() { count = 0, deviceHistoryList = new DeviceHistoryEntry_t[] { } };
        }

        private CallingDeviceID_t ReadCallingDevice(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTFailedParser.ReadCallingDevice: read CalledDeviceID_t from stream...");

                if (reader.TryReadStruct(typeof(CallingDeviceID_t), out result))
                {
                    logger.Info("ATTFailedParser.ReadCallingDevice: successfully read the CalledDeviceID_t!");

                    CallingDeviceID_t callingDevice = (CallingDeviceID_t)result;

                    logger.Info("ATTFailedParser.ReadCallingDevice: callingDevice.deviceID={0};callingDevice.deviceIDType={1};callingDevice.deviceIDStatus={2};", callingDevice.value.deviceID.device, callingDevice.value.deviceIDType, callingDevice.value.deviceIDStatus);

                    return callingDevice;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTFailedParser.ReadCallingDevice: {0}", err));
            }

            return new CallingDeviceID_t();
        }

        public int eventType
        {
            get { return Constants.ATT_FAILED; }
        }
    }
}
