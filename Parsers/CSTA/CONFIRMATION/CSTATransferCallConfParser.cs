using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class CSTATransferCallConfParser : ICSTAConfirmationParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public CSTAConfirmationEvent Parse(IStructReader reader)
        {
            try
            {
                logger.Info("CSTATransferCallConfParser.Parse: eventType=CSTA_TRANSFER_CALL_CONF");
                CSTATransferCallConfEvent_t transferCall = new CSTATransferCallConfEvent_t
                {
                    newCall = ReadNewCall(reader),
                    connList = ReadConnList(reader)
                };

                CSTAConfirmationEvent cstaConfirmation = new CSTAConfirmationEvent { u = { transferCall = transferCall } };

                return cstaConfirmation;               
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTATransferCallConfParser.Parse: {0}", err));
            }

            return null;
        }

        private ConnectionID_t ReadNewCall(IStructReader reader)
        {
            try
            {
                object result;

                logger.Info("CSTATransferCallConfParser.ReadNewCall: try to read new call from stream...");

                if (reader.TryReadStruct(typeof(ConnectionID_t), out result))
                {
                    logger.Info("CSTATransferCallConfParser.ReadNewCall: successfully read new call from stream!");

                    ConnectionID_t newCall = (ConnectionID_t)result;

                    logger.Info("CSTATransferCallConfParser.ReadNewCall: newCall.callID={0};newCall.deviceID.device={1};newCall.devIDType={2};", newCall.callID, newCall.deviceID.device, newCall.devIDType);

                    return newCall;
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in CSTATransferCallConfParser.ReadNewCall: {0}", err));
            }

            return new ConnectionID_t();
        }

        private ConnectionList_t ReadConnList(IStructReader reader)
        {
            try
            {
                logger.Info("CSTATransferCallConfParser.ReadConnList: BaseStream.Position={0};BaseStream.Length={1};", reader.BaseStream.Position, reader.BaseStream.Length);

                if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 4)
                {
                    logger.Info("CSTATransferCallConfParser.ReadConnList: read count of transferred connections...");

                    uint count = reader.ReadUInt32();

                    logger.Info("CSTATransferCallConfParser.ReadConnList: count={0}", count);

                    List<Connection_t> connections = new List<Connection_t>();

                    if (count > 0)
                    {
                        if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 12)
                        {
                            logger.Info("CSTATransferCallConfParser.ReadConnList: advance the base stream 12 positions...");
                            reader.BaseStream.Position += 12;
                        }

                        int size = Marshal.SizeOf(typeof(Connection_t));

                        for (int i = 0; i < count; i++)
                        {
                            logger.Info("CSTATransferCallConfParser.ReadConnList: try to read connection from stream...");

                            if ((reader.BaseStream.Length - reader.BaseStream.Position) >= size)
                            {
                                object result;

                                if (reader.TryReadStruct(typeof(Connection_t), out result))
                                {
                                    logger.Info("CSTATransferCallConfParser.ReadConnList: successfully read connection from stream!");

                                    Connection_t connection = (Connection_t)result;

                                    logger.Info("CSTATransferCallConfParser.ReadConnList: connection.party.callID={0};connection.party.deviceID.device={1};connection.party.devIDType={2};connection.staticDevice.deviceID.device={3};connection.staticDevice.deviceIDType={4};connection.staticDevice.deviceIDType={4};", connection.party.callID, connection.party.deviceID.device, connection.party.devIDType, connection.staticDevice.deviceID.device, connection.staticDevice.deviceIDType);
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
                logger.Error(string.Concat("Error in CSTATransferCallConfParser.ReadConnList: ", err.ToString()));
            }

            return new ConnectionList_t() { count = 0, connections = new Connection_t[] { } };
        }

        public int eventType
        {
            get { return Constants.CSTA_TRANSFER_CALL_CONF; }
        }
    }
}
