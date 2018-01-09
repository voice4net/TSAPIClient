// ATTConnectionClearedParser.cs
using System;
using System.Text;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTConnectionClearedParser : IATTEventParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public ATTEvent_t Parse(IStructReader reader)
        {
            try
            {
                logger.Info("ATTConnectionClearedParser.Parse: eventType=ATT_CONNECTION_CLEARED");

                ATTConnectionClearedEvent_t connectionCleared = new ATTConnectionClearedEvent_t
                {
                    userInfo = ReadUserToUserInfo(reader),
                    deviceHistory = ReadDeviceHistory(reader)
                };

                ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                attEvent.u.connectionCleared = connectionCleared;

                return attEvent;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTConnectionClearedParser.Parse: {0}", err));
            }

            return null;
        }

        private ATTUserToUserInfo_t ReadUserToUserInfo(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTConnectionClearedParser.ReadUserToUserInfo: try to read user to user info from stream...");

                if (reader.TryReadStruct(typeof(ATTUserToUserInfo_t), out result))
                {
                    logger.Info("ATTConnectionClearedParser.ReadUserToUserInfo: successfully read user to user info from stream!");

                    ATTUserToUserInfo_t userInfo = (ATTUserToUserInfo_t)result;

                    logger.Info("ATTConnectionClearedParser.ReadUserToUserInfo: userInfo.type={0};userInfo.length={1};userInfo.data={2};", userInfo.type, userInfo.length, Encoding.Default.GetString(userInfo.data));

                    return userInfo;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTConnectionClearedParser.ReadUserToUserInfo: {0}", err));
            }

            return new ATTUserToUserInfo_t();
        }

        private DeviceHistory_t ReadDeviceHistory(IStructReader reader)
        {
            try
            {
                logger.Info("ATTConnectionClearedParser.ReadDeviceHistory: read device history count from stream...");

                uint count = reader.ReadUInt32();

                logger.Info("ATTConnectionClearedParser.ReadDeviceHistory: count={0}", count);

                if (count != 1)
                {
                    return new DeviceHistory_t() { count = 0, deviceHistoryList = new DeviceHistoryEntry_t[] { }};
                }

                reader.BaseStream.Position += 4;

                object result;

                logger.Info("ATTConnectionClearedParser.ReadDeviceHistory: try to read a device history entry from the stream...");

                if (reader.TryReadStruct(typeof(DeviceHistoryEntry_t), out result))
                {
                    logger.Info("ATTConnectionClearedParser.ReadDeviceHistory: successfully read device history entry from stream!");

                    DeviceHistoryEntry_t deviceHistoryEntry = (DeviceHistoryEntry_t)result;

                    logger.Info("ATTConnectionClearedParser.ReadDeviceHistory: deviceHistoryEntry.olddeviceID={0};deviceHistoryEntry.oldconnectionID.callID={1};deviceHistoryEntry.oldconnectionID.deviceID={2};", deviceHistoryEntry.olddeviceID.device, deviceHistoryEntry.oldconnectionID.callID, deviceHistoryEntry.oldconnectionID.deviceID.device);

                    return new DeviceHistory_t() { count = 1, deviceHistoryList = new DeviceHistoryEntry_t[] { deviceHistoryEntry } };
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTConnectionClearedParser.ReadDeviceHistory: {0}", err));
            }

            return new DeviceHistory_t() { count = 0, deviceHistoryList = new DeviceHistoryEntry_t[] { } };
        }

        public int eventType
        {
            get { return Constants.ATT_CONNECTION_CLEARED; }
        }
    }
}
