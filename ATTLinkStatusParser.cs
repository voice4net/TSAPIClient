// ATTLinkStatusParser.cs
using System;
using System.Collections.Generic;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.Readers;

// ReSharper disable once CheckNamespace
namespace TSAPIClient.Parsers
{
    public class ATTLinkStatusParser : IATTEventParser
    {
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        public ATTEvent_t Parse(IStructReader reader)
        {
            try
            {
                logger.Info("ATTLinkStatusParser.Parse: eventType=ATT_LINK_STATUS");

                ATTLinkStatusEvent_t linkStatus = new ATTLinkStatusEvent_t();

                logger.Info("ATTLinkStatusParser.Parse: read count from stream...");
                uint count = reader.ReadUInt32();

                logger.Info("ATTLinkStatusParser.Parse: count={0}", count);

                List<ATTLinkStatus_t> linkStatusList = new List<ATTLinkStatus_t>();

                for (int i = 0; i < count; i++)
                {
                    short linkID = 0;

                    if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 2)
                    {
                        logger.Info("ATTLinkStatusParser.Parse: read link id from stream...");
                        linkID = reader.ReadInt16();

                        logger.Info("ATTLinkStatusParser.Parse: linkID={0}", linkID);
                    }

                    if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 2)
                    {
                        logger.Info("ATTLinkStatusParser.Parse: advance position of base stream by 2 positions...");
                        reader.BaseStream.Position += 2;
                    }

                    ATTLinkState_t linkState = ATTLinkState_t.LS_LINK_UNAVAIL;

                    if ((reader.BaseStream.Length - reader.BaseStream.Position) >= 4)
                    {
                        logger.Info("ATTLinkStatusParser.Parse: read link state from stream...");
                        var value = reader.ReadInt32();                        

                        logger.Info("ATTLinkStatusParser.Parse: value={0}", value);

                        if (Enum.IsDefined(typeof(ATTLinkState_t), value))
                        {
                            linkState = (ATTLinkState_t)value;
                            logger.Info("ATTLinkStatusParser.Parse: linkState={0}", linkState);
                        }
                    }

                    ATTLinkStatus_t status = new ATTLinkStatus_t() { linkID = linkID, linkState = linkState };

                    linkStatusList.Add(status);
                }

                linkStatus.count = count;
                linkStatus.pLinkStatus = linkStatusList.ToArray();

                ATTEvent_t attEvent = new ATTEvent_t() { eventType = (ushort)eventType };

                attEvent.u.linkStatus = linkStatus;

                return attEvent;
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in ATTLinkStatusParser.Parse: {0}", err));
            }

            return null;
        }       

        public int eventType
        {
            get { return Constants.ATT_LINK_STATUS; }
        }
    }
}
