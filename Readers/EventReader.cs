using System;
using NLog;
using TSAPIClient.CSTA;
using TSAPIClient.Parsers;

namespace TSAPIClient.Readers
{
    internal class EventReader
    {
        private readonly IStructReader m_FrameReader;
        private static readonly Logger logger = LogManager.GetLogger("TSAPIClient");

        private readonly ICSTAConfirmationParserFactory m_CSTAConfParserFactory;
        private readonly ICSTAUnsolicitedParserFactory m_CSTAUnsolicitedParserFactory;
        private readonly IACSConfirmationParserFactory m_ACSConfParserFactory;
        private readonly IACSUnsolicitedParserFactory m_ACSUnsolicitedParserFactory;

        internal EventReader(IStructReader reader, ICSTAConfirmationParserFactory cstaConfParserFactory, ICSTAUnsolicitedParserFactory cstaUnsolicitedParserFactory, IACSConfirmationParserFactory acsConfParserFactory, IACSUnsolicitedParserFactory acsUnsolicitedParserFactory)
        {
            m_FrameReader = reader;

            m_CSTAConfParserFactory = cstaConfParserFactory;
            m_CSTAUnsolicitedParserFactory = cstaUnsolicitedParserFactory;
            m_ACSConfParserFactory = acsConfParserFactory;
            m_ACSUnsolicitedParserFactory = acsUnsolicitedParserFactory;
        }

        internal CSTAEvent_t Read()
        {
            try
            {
                logger.Info("EventReader.Read: read event header...");

                object result;

                if (m_FrameReader.TryReadStruct(typeof(ACSEventHeader_t), out result))
                {
                    ACSEventHeader_t eventHeader = (ACSEventHeader_t)result;

                    logger.Info("EventReader.Read: acsHandle={0};eventClass={1};eventType={2};", eventHeader.acsHandle, eventHeader.eventClass, eventHeader.eventType);

                    switch (eventHeader.eventClass)
                    {
                        case Constants.CSTACONFIRMATION:

                            #region CSTACONFIRMATION

                            int invokeID = m_FrameReader.ReadInt32();

                            logger.Info("EventReader.Read.CSTACONFIRMATION: invokeID={0};", invokeID);

                            logger.Info("EventReader.Read.CSTACONFIRMATION: Getting CSTAConfParser from parserFactory...");
                            ICSTAConfirmationParser cstaConfParser = m_CSTAConfParserFactory.CreateParser(eventHeader.eventType);

                            if (cstaConfParser != null)
                            {
                                CSTAConfirmationEvent cstaConfirmation = cstaConfParser.Parse(m_FrameReader);
                                cstaConfirmation.invokeID = invokeID;

                                CSTAEvent_t cstaEvent = new CSTAEvent_t()
                                {
                                    eventHeader = eventHeader,
                                    Event = {cstaConfirmation = cstaConfirmation}
                                };

                                return cstaEvent;
                            }

                            logger.Info("EventReader.Read.CSTACONFIRMATION: ICSTAConfParserFactory failed to return parser!!");

                            #endregion CSTACONFIRMATION

                            break;

                        case Constants.CSTAUNSOLICITED:

                            #region CSTAUNSOLICITED

                            int xref = m_FrameReader.ReadInt32();

                            logger.Info("EventReader.Read.CSTAUNSOLICITED: xref={0};", xref);

                            logger.Info("EventReader.Read.CSTAUNSOLICITED: Getting CSTAUnsolicitedParser from parserFactory...");
                            ICSTAUnsolicitedParser cstaUnsolicitedParser = m_CSTAUnsolicitedParserFactory.CreateParser(eventHeader.eventType);

                            if (cstaUnsolicitedParser != null)
                            {
                                CSTAUnsolicitedEvent cstaUnsolicited = cstaUnsolicitedParser.Parse(m_FrameReader);

                                cstaUnsolicited.monitorCrossRefId = xref;

                                CSTAEvent_t cstaEvent = new CSTAEvent_t()
                                {
                                    eventHeader = eventHeader,
                                    Event = {cstaUnsolicited = cstaUnsolicited}
                                };


                                return cstaEvent;
                            }

                            logger.Info("EventReader.Read.CSTAUNSOLICITED: ICSTAUnsolicitedParserFactory failed to return parser!!");

                            #endregion CSTAUNSOLICITED

                            break;

                        case Constants.ACSCONFIRMATION:

                            #region ACSCONFIRMATION

                            invokeID = m_FrameReader.ReadInt32();

                            logger.Info("EventReader.Read.ACSCONFIRMATION: invokeID={0};", invokeID);

                            logger.Info("EventReader.Read.ACSCONFIRMATION: Getting ACSConfirmationParser from parserFactory...");
                            IACSConfirmationParser acsConfParser = m_ACSConfParserFactory.CreateParser(eventHeader.eventType);

                            if (acsConfParser != null)
                            {
                                ACSConfirmationEvent acsConfirmation = acsConfParser.Parse(m_FrameReader);
                                acsConfirmation.invokeID = invokeID;

                                CSTAEvent_t cstaEvent = new CSTAEvent_t()
                                {
                                    eventHeader = eventHeader,
                                    Event = {acsConfirmation = acsConfirmation}
                                };

                                return cstaEvent;
                            }

                            logger.Info("EventReader.Read.ACSCONFIRMATION: IACSConfirmationParserFactory failed to return parser!!");

                            #endregion ACSCONFIRMATION

                            break;

                        case Constants.ACSUNSOLICITED:

                            #region ACSUNSOLICITED

                            logger.Info("ACSUNSOLICITED :: Getting ACSUnsolicitedParser from parserFactory...");
                            IACSUnsolicitedParser acsUnsolicitedParser = m_ACSUnsolicitedParserFactory.CreateParser(eventHeader.eventType);

                            if (acsUnsolicitedParser != null)
                            {
                                ACSUnsolicitedEvent acsUnsolicited = acsUnsolicitedParser.Parse(m_FrameReader);

                                CSTAEvent_t cstaEvent = new CSTAEvent_t()
                                {
                                    eventHeader = eventHeader,
                                    Event = {acsUnsolicited = acsUnsolicited}
                                };


                                return cstaEvent;
                            }

                            logger.Info("ACSUNSOLICITED :: IACSUnsolicitedParserFactory failed to return parser!!");

                            #endregion ACSUNSOLICITED

                            break;
                    }
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in EventReader.ReadEvent: {0}", err));
            }

            return null;
        }
    }

    internal static class SequenceNumberGenerator
    {
        private static int m_SequenceNumber;
        private static readonly object locker = new object();
        internal static string Generate()
        {
            string seq_num;

            lock (locker)
            {
                if (m_SequenceNumber == 99999)
                {
                    m_SequenceNumber = 0;
                }

                m_SequenceNumber++;

                seq_num = m_SequenceNumber.ToString();
                seq_num = seq_num.PadLeft(5, '0');
            }

            return seq_num;
        }
    }
}
