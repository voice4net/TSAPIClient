// ATTNetworkReachedParser.cs
using System;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;
using Constants = TSAPIClient.ATT.Constants;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTNetworkReachedParser : IATTEventParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public ATTEvent_t Parse(IStructReader reader)
        { 
            try
            {
                logger.Info("ATTNetworkReachedParser.Parse: eventType=ATT_NETWORK_REACHED");

                ATTNetworkReachedEvent_t networkReachedEvent = new ATTNetworkReachedEvent_t
                {
                    progressLocation = ReadProgressLocation(reader),
                    progressDescription = ReadProgressDescription(reader),
                    trunkGroup = ReadTrunkGroup(reader),
                    trunkMember = ReadTrunkMember(reader),
                    deviceHistory = ReadDeviceHistory(reader)
                };

                ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                attEvent.u.networkReachedEvent = networkReachedEvent;

                return attEvent;                
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTNetworkReachedParser.Parse: {0}", err));
            }

            return null;
        }        

        private ATTProgressLocation_t ReadProgressLocation(IStructReader reader)
        {
            try
            {
                ATTProgressLocation_t progressLocation = new ATTProgressLocation_t();

                logger.Info("ATTNetworkReachedParser.ReadProgressLocation: read progress location...");
                int intProgressLocation = reader.ReadInt32();

                logger.Info("ATTNetworkReachedParser.ReadProgressLocation: ProgressLocation={0}", intProgressLocation);

                if (Enum.IsDefined(typeof(ATTProgressLocation_t), intProgressLocation))
                {
                    progressLocation = (ATTProgressLocation_t)intProgressLocation;

                    logger.Info("ATTNetworkReachedParser.ReadProgressLocation: progressLocation={0}", progressLocation);
                }

                return progressLocation; 
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTNetworkReachedParser.ReadProgressLocation: {0}", err));
            }

            return ATTProgressLocation_t.PL_NONE;            
        }

        private ATTProgressDescription_t ReadProgressDescription(IStructReader reader)
        {
            try
            {
                ATTProgressDescription_t progressDescription = new ATTProgressDescription_t();

                logger.Info("ATTNetworkReachedParser.ReadProgressDescription: read progress description...");
                int intProgressDescription = reader.ReadInt32();

                logger.Info("ATTNetworkReachedParser.ReadProgressDescription: ProgressDescription={0}", intProgressDescription);

                if (Enum.IsDefined(typeof(ATTProgressDescription_t), intProgressDescription))
                {
                    progressDescription = (ATTProgressDescription_t)intProgressDescription;

                    logger.Info("ATTNetworkReachedParser.ReadProgressDescription: progressDescription={0}", progressDescription);
                }

                return progressDescription;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTNetworkReachedParser.ReadProgressDescription: {0}", err));
            }

            return ATTProgressDescription_t.PD_NONE;            
        }

        private DeviceID_t ReadTrunkGroup(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTNetworkReachedParser.ReadTrunkGroup: read DeviceID_t from stream...");

                if (reader.TryReadStruct(typeof(DeviceID_t), out result))
                {
                    logger.Info("ATTNetworkReachedParser.ReadTrunkGroup: successfully read the DeviceID_t!");

                    DeviceID_t trunkGroup = (DeviceID_t)result;

                    logger.Info("ATTNetworkReachedParser.ReadTrunkGroup: trunkGroup.Device={0}", trunkGroup.device);

                    return trunkGroup;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTNetworkReachedParser.ReadTrunkGroup: {0}", err));
            }

            return new DeviceID_t();
        }

        private DeviceID_t ReadTrunkMember(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTNetworkReachedParser.ReadTrunkMember: read DeviceID_t from stream...");

                if (reader.TryReadStruct(typeof(DeviceID_t), out result))
                {
                    logger.Info("ATTNetworkReachedParser.ReadTrunkMember: successfully read the DeviceID_t!");

                    DeviceID_t trunkMember = (DeviceID_t)result;

                    logger.Info("ATTNetworkReachedParser.ReadTrunkMember: trunkMember.Device={0}", trunkMember.device);

                    return trunkMember;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTNetworkReachedParser.ReadTrunkMember: {0}", err));
            }

            return new DeviceID_t();
        }

        private DeviceHistory_t ReadDeviceHistory(IStructReader reader)
        {
            try
            {
                DeviceHistory_t deviceHistory = new DeviceHistory_t();

                logger.Info("ATTNetworkReachedParser.ReadDeviceHistory: read device history count from stream...");
                uint count = reader.ReadUInt32();

                logger.Info("ATTNetworkReachedParser.ReadDeviceHistory: count={0}", count);

                if (count == 1)
                {
                    reader.BaseStream.Position += 4;

                    object result;

                    logger.Info("ATTNetworkReachedParser.ReadDeviceHistory: try to read a device history entry from the stream...");                    

                    if (reader.TryReadStruct(typeof(DeviceHistoryEntry_t), out result))
                    {
                        logger.Info("ATTNetworkReachedParser.ReadDeviceHistory: successfully read device history entry from stream!");

                        DeviceHistoryEntry_t deviceHistoryEntry = (DeviceHistoryEntry_t)result;

                        logger.Info("ATTNetworkReachedParser.ReadDeviceHistory: deviceHistoryEntry.olddeviceID={0};deviceHistoryEntry.oldconnectionID.callID={1};deviceHistoryEntry.oldconnectionID.deviceID={2};", deviceHistoryEntry.olddeviceID.device, deviceHistoryEntry.oldconnectionID.callID, deviceHistoryEntry.oldconnectionID.deviceID.device);

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
                logger.Error(string.Format("Error in ATTNetworkReachedParser.ReadDeviceHistory: {0}", err));
            }

            return new DeviceHistory_t() { count = 0, deviceHistoryList = new DeviceHistoryEntry_t[] { } };
        }

        public int eventType
        {
            get { return Constants.ATT_NETWORK_REACHED; }
        }
    }
}
