// ATTSingleStepConferenceCallConfParser.cs
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;
using Constants = TSAPIClient.ATT.Constants;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTSingleStepConferenceCallConfParser : IATTEventParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public ATTEvent_t Parse(IStructReader reader)
        {
            try
            {
                logger.Info("ATTSingleStepConferenceCallConfParser.Parse: eventType=ATT_SINGLE_STEP_CONFERENCE_CALL_CONF");

                ATTSingleStepConferenceCallConfEvent_t ssconference = new ATTSingleStepConferenceCallConfEvent_t
                {
                    newCall = ReadNewCall(reader),
                    connList = ReadConnList(reader),
                    ucid = ReadUCID(reader)
                };

                ATTEvent_t attEvent = new ATTEvent_t();

                attEvent.u.ssconference = ssconference;

                return attEvent;                                
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTSingleStepConferenceCallConfParser.Parse: {0}", err));
            }

            return null;
        }

        private ConnectionID_t ReadNewCall(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("ATTSingleStepConferenceCallConfParser.ReadNewCall: try to read new call from stream...");

                if (reader.TryReadStruct(typeof(ConnectionID_t), out result))
                {
                    logger.Info("ATTSingleStepConferenceCallConfParser.ReadNewCall: successfully read new call from stream!");

                    ConnectionID_t newCall = (ConnectionID_t)result;

                    logger.Info("ATTSingleStepConferenceCallConfParser.ReadNewCall: newCall.callID={0};newCall.deviceID.device={1};newCall.devIDType={2};", newCall.callID, newCall.deviceID.device, newCall.devIDType);

                    return newCall;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Concat("Error in ATTSingleStepConferenceCallConfParser.ReadNewCall: ", err.ToString()));
            }

            return new ConnectionID_t();
        }

        private ConnectionList_t ReadConnList(IStructReader reader)
        {
            try
            {
                logger.Info("ATTSingleStepConferenceCallConfParser.ReadConnList: BaseStream.Position={0};BaseStream.Length={1};", reader.BaseStream.Position, reader.BaseStream.Length);

                if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 4)
                {
                    logger.Info("ATTSingleStepConferenceCallConfParser.ReadConnList: read count of single step conference connections...");

                    uint count = reader.ReadUInt32();

                    logger.Info("ATTSingleStepConferenceCallConfParser.ReadConnList: count={0}", count);

                    List<Connection_t> connections = new List<Connection_t>();

                    if (count > 0)
                    {
                        if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 12)
                        {
                            logger.Info("ATTSingleStepConferenceCallConfParser.ReadConnList: advance the base stream 12 positions...");
                            reader.BaseStream.Position += 12;
                        }

                        int size = Marshal.SizeOf(typeof(Connection_t));

                        for (int i = 0; i < count; i++)
                        {
                            logger.Info("ATTSingleStepConferenceCallConfParser.ReadConnList: try to read connection from stream...");

                            if ((reader.BaseStream.Length - reader.BaseStream.Position) >= size)
                            {
                                object result;

                                if (reader.TryReadStruct(typeof(Connection_t), out result))
                                {
                                    logger.Info("ATTSingleStepConferenceCallConfParser.ReadConnList: successfully read connection from stream!");

                                    Connection_t connection = (Connection_t)result;

                                    logger.Info("ATTSingleStepConferenceCallConfParser.ReadConnList: connection.party.callID={0};connection.party.deviceID.device={1};connection.party.devIDType={2};connection.staticDevice.deviceID.device={3};connection.staticDevice.deviceIDType={4};connection.staticDevice.deviceIDType={4};", connection.party.callID, connection.party.deviceID.device, connection.party.devIDType, connection.staticDevice.deviceID.device, connection.staticDevice.deviceIDType);
                                    connections.Add(connection);
                                }
                            }
                        }
                    }

                    return new ConnectionList_t() { count = count, connections = connections.ToArray() };
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Concat("Error in ATTSingleStepConferenceCallConfParser.ReadConnList: ", err.ToString()));
            }

            return new ConnectionList_t() { count = 0, connections = new Connection_t[] { } };
        }

        private ATTUCID_t ReadUCID(IStructReader reader)
        {
            try
            {
                logger.Info("ATTSingleStepConferenceCallConfParser.ReadUCID: BaseStream.Position={0};BaseStream.Length={1};", reader.BaseStream.Position, reader.BaseStream.Length);

                int size = Marshal.SizeOf(typeof(ATTUCID_t));

                logger.Info("ATTSingleStepConferenceCallConfParser.ReadUCID: try to read UCID from stream...");                

                if ((reader.BaseStream.Length - reader.BaseStream.Position) >= size)
                {
                    object result;

                    if (reader.TryReadStruct(typeof(ATTUCID_t), out result))
                    {
                        logger.Info("ATTSingleStepConferenceCallConfParser.ReadUCID: successfully read UCID from stream!");

                        ATTUCID_t ucid = (ATTUCID_t)result;

                        logger.Info("ATTSingleStepConferenceCallConfParser.ReadUCID: UCID.value={0};", ucid.value);

                        return ucid;
                    }
                }                
            }
            catch (Exception err)
            {
                logger.Error(string.Concat("Error in ATTSingleStepConferenceCallConfParser.ReadUCID: ", err.ToString()));
            }

            return new ATTUCID_t() { value = string.Empty };
        }

        public int eventType
        {
            get { return Constants.ATT_SINGLE_STEP_CONFERENCE_CALL_CONF; }
        }
    }
}