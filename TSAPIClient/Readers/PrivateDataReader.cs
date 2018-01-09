using System;
using System.IO;
using System.Text;
using NLog;
using TSAPIClient.ATT;
using TSAPIClient.CSTA;
using TSAPIClient.Parsers;

namespace TSAPIClient.Readers
{
    internal class PrivateDataReader
    {
        private PrivateData_t m_PrivateData;
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");
        private readonly IATTEventParserFactory m_ParserFactory;

        internal PrivateDataReader(PrivateData_t privateData, IATTEventParserFactory parserFactory)
        {
            m_PrivateData = privateData;
            m_ParserFactory = parserFactory;
        }

        internal ATTEvent_t Read()
        {
            try
            {
                if (m_PrivateData.length > 0)
                {
                    ATTEventBuf_t attEventBuf = new ATTEventBuf_t();

                    logger.Info("PrivateDataReader.ReadEvent: privateData.length={0};privateData.vendor={1};privateData.data={2}", m_PrivateData.length, m_PrivateData.vendor, new string(m_PrivateData.data));

                    logger.Info("PrivateDataReader.ReadEvent: decode the private data buffer...");
                    int RetCode_t = ATT.Proxy.attPrivateData(ref m_PrivateData, ref attEventBuf);

                    logger.Info("PrivateDataReader.ReadEvent: ReturnCode={0}", RetCode_t);

                    if (RetCode_t == CSTA.Constants.ACSPOSITIVE_ACK)
                    {
                        logger.Info("PrivateDataReader.ReadEvent: attEventBuf.data.Length={0};attEventBuf.data={1};", attEventBuf.data.Length, Encoding.UTF8.GetString(attEventBuf.data));

                        if (attEventBuf.data.Length > 0)
                        {
                            MemoryStream stream = new MemoryStream(attEventBuf.data);
                            StructReader reader = new StructReader(stream);

                            logger.Info("PrivateDataReader.ReadEvent: eventType={0}", attEventBuf.eventType);

                            if (m_ParserFactory != null)
                            {
                                logger.Info("PrivateDataReader.ReadEvent: create event parser...");
                                IATTEventParser parser = m_ParserFactory.CreateParser(attEventBuf.eventType);

                                if (parser != null)
                                {
                                    logger.Info("PrivateDataReader.ReadEvent: parse the private data event...");
                                    ATTEvent_t attEvent = parser.Parse(reader);

                                    logger.Info("PrivateDataReader.ReadEvent: attEvent={0}", attEvent);

                                    return attEvent;
                                }

                                logger.Info("***************************");
                                logger.Info("* UNKNOWN EVENT TYPE: {0} *", attEventBuf.eventType);
                                logger.Info("***************************");
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in PrivateDataReader.Read: {0}", err));
            }

            return null;
        }
    }
}
