namespace TSAPIClient.ATT
{
    public class ATTEvent_t
    {
        public ushort eventType;

        private readonly ATTEvent_Union m_Union = new ATTEvent_Union();
        public ATTEvent_Union u
        {
            get { return m_Union; }
        }

        private char[] m_heap = { };
        public char[] heap
        {
            get { return m_heap; }
            set { m_heap = value; }
        }
    }

    public class ATTEvent_Union
    {
        private object m_UnionObject = new object();

        public ATTConferenceCallConfEvent_t? conferenceCall
        {
            #region conferenceCall

            get
            {
                return m_UnionObject as ATTConferenceCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion conferenceCall
        }

        public ATTConsultationCallConfEvent_t? consultationCall
        {
            #region consultationCall

            get
            {
                return m_UnionObject as ATTConsultationCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion consultationCall
        }

        public ATTMakeCallConfEvent_t? makeCall
        {
            #region makeCall

            get
            {
                return m_UnionObject as ATTMakeCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion makeCall
        }

        public ATTMakePredictiveCallConfEvent_t? makePredictiveCall
        {
            #region makePredictiveCall

            get
            {
                return m_UnionObject as ATTMakePredictiveCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion makePredictiveCall
        }

        public ATTSingleStepConferenceCallConfEvent_t? ssconference
        {
            #region ssconference

            get
            {
                return m_UnionObject as ATTSingleStepConferenceCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion ssconference
        }

        public ATTSingleStepTransferCallConfEvent_t? ssTransferCallConf
        {
            #region ssTransferCallConf

            get
            {
                return m_UnionObject as ATTSingleStepTransferCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion ssTransferCallConf
        }

        public ATTTransferCallConfEvent_t? transferCall
        {
            #region transferCall

            get
            {
                return m_UnionObject as ATTTransferCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion transferCall
        }

        public ATTSetAgentStateConfEvent_t? setAgentState
        {
            #region setAgentState

            get
            {
                return m_UnionObject as ATTSetAgentStateConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion setAgentState
        }

        public ATTQueryAcdSplitConfEvent_t? queryAcdSplit
        {
            #region queryAcdSplit

            get
            {
                return m_UnionObject as ATTQueryAcdSplitConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryAcdSplit
        }

        public ATTQueryAgentLoginConfEvent_t? queryAgentLogin
        {
            #region queryAgentLogin

            get
            {
                return m_UnionObject as ATTQueryAgentLoginConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryAgentLogin
        }

        public ATTQueryAgentLoginResp_t? queryAgentLoginResp
        {
            #region queryAgentLoginResp

            get
            {
                return m_UnionObject as ATTQueryAgentLoginResp_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryAgentLoginResp
        }

        public ATTQueryAgentStateConfEvent_t? queryAgentState
        {
            #region queryAgentState

            get
            {
                return m_UnionObject as ATTQueryAgentStateConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryAgentState
        }

        public ATTQueryCallClassifierConfEvent_t? queryCallClassifier
        {
            #region queryCallClassifier

            get
            {
                return m_UnionObject as ATTQueryCallClassifierConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryCallClassifier
        }

        public ATTQueryDeviceInfoConfEvent_t? queryDeviceInfo
        {
            #region queryDeviceInfo

            get
            {
                return m_UnionObject as ATTQueryDeviceInfoConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryDeviceInfo
        }

        public ATTQueryDeviceNameConfEvent_t? queryDeviceName
        {
            #region queryDeviceName

            get
            {
                return m_UnionObject as ATTQueryDeviceNameConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryDeviceName
        }

        public ATTQueryMwiConfEvent_t? queryMwi
        {
            #region queryMwi

            get
            {
                return m_UnionObject as ATTQueryMwiConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryMwi
        }

        public ATTQueryStationStatusConfEvent_t? queryStationStatus
        {
            #region queryStationStatus

            get
            {
                return m_UnionObject as ATTQueryStationStatusConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryStationStatus
        }

        public ATTQueryTodConfEvent_t? queryTOD
        {
            #region queryTOD

            get
            {
                return m_UnionObject as ATTQueryTodConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryTOD
        }

        public ATTQueryTgConfEvent_t? queryTg
        {
            #region queryTg

            get
            {
                return m_UnionObject as ATTQueryTgConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryTg
        }

        public ATTQueryUcidConfEvent_t? queryUCID
        {
            #region queryUCID

            get
            {
                return m_UnionObject as ATTQueryUcidConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryUCID
        }

        public ATTSnapshotCallConfEvent_t? snapshotCallConf
        {
            #region snapshotCallConf

            get
            {
                return m_UnionObject as ATTSnapshotCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion snapshotCallConf
        }

        public ATTSnapshotDeviceConfEvent_t? snapshotDevice
        {
            #region snapshotDevice

            get
            {
                return m_UnionObject as ATTSnapshotDeviceConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion snapshotDevice
        }

        public ATTMonitorConfEvent_t? monitorStart
        {
            #region monitorStart

            get
            {
                return m_UnionObject as ATTMonitorConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion monitorStart
        }

        public ATTMonitorCallConfEvent_t? monitorCallStart
        {
            #region monitorCallStart

            get
            {
                return m_UnionObject as ATTMonitorCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion monitorCallStart
        }

        public ATTMonitorStopOnCallConfEvent_t? monitorStopOnCall
        {
            #region monitorStopOnCall

            get
            {
                return m_UnionObject as ATTMonitorStopOnCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion monitorStopOnCall
        }

        public ATTCallClearedEvent_t? callClearedEvent
        {
            #region callClearedEvent

            get
            {
                return m_UnionObject as ATTCallClearedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion callClearedEvent
        }

        public ATTChargeAdviceEvent_t? chargeAdviceEvent
        {
            #region chargeAdviceEvent

            get
            {
                return m_UnionObject as ATTChargeAdviceEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion chargeAdviceEvent
        }

        public ATTConferencedEvent_t? conferencedEvent
        {
            #region conferencedEvent

            get
            {
                return m_UnionObject as ATTConferencedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion conferencedEvent
        }

        public ATTConnectionClearedEvent_t? connectionCleared
        {
            #region connectionCleared

            get
            {
                return m_UnionObject as ATTConnectionClearedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion connectionCleared
        }

        public ATTDeliveredEvent_t? deliveredEvent
        {
            #region deliveredEvent

            get
            {
                return m_UnionObject as ATTDeliveredEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion deliveredEvent
        }

        public ATTDivertedEvent_t? divertedEvent
        {
            #region divertedEvent

            get
            {
                return m_UnionObject as ATTDivertedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion divertedEvent
        }

        public ATTEstablishedEvent_t? establishedEvent
        {
            #region establishedEvent

            get
            {
                return m_UnionObject as ATTEstablishedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion establishedEvent
        }

        public ATTFailedEvent_t? failedEvent
        {
            #region failedEvent

            get
            {
                return m_UnionObject as ATTFailedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion failedEvent
        }

        public ATTHeldEvent_t? heldEvent
        {
            #region heldEvent

            get
            {
                return m_UnionObject as ATTHeldEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion heldEvent
        }

        public ATTLoggedOffEvent_t? loggedOffEvent
        {
            #region loggedOffEvent

            get
            {
                return m_UnionObject as ATTLoggedOffEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion loggedOffEvent
        }

        public ATTLoggedOnEvent_t? loggedOnEvent
        {
            #region loggedOnEvent

            get
            {
                return m_UnionObject as ATTLoggedOnEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion loggedOnEvent
        }

        public ATTNetworkReachedEvent_t? networkReachedEvent
        {
            #region networkReachedEvent

            get
            {
                return m_UnionObject as ATTNetworkReachedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion networkReachedEvent
        }

        public ATTOriginatedEvent_t? originatedEvent
        {
            #region originatedEvent

            get
            {
                return m_UnionObject as ATTOriginatedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion originatedEvent
        }

        public ATTQueuedEvent_t? queuedEvent
        {
            #region queuedEvent

            get
            {
                return m_UnionObject as ATTQueuedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queuedEvent
        }

        public ATTServiceInitiatedEvent_t? serviceInitiated
        {
            #region serviceInitiated

            get
            {
                return m_UnionObject as ATTServiceInitiatedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion serviceInitiated
        }

        public ATTTransferredEvent_t? transferredEvent
        {
            #region transferredEvent

            get
            {
                return m_UnionObject as ATTTransferredEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion transferredEvent
        }

        public ATTRouteRequestEvent_t? routeRequest
        {
            #region routeRequest

            get
            {
                return m_UnionObject as ATTRouteRequestEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion routeRequest
        }

        public ATTRouteUsedEvent_t? routeUsed
        {
            #region routeUsed

            get
            {
                return m_UnionObject as ATTRouteUsedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion routeUsed
        }

        public ATTLinkStatusEvent_t? linkStatus
        {
            #region linkStatus

            get
            {
                return m_UnionObject as ATTLinkStatusEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion linkStatus
        }

        public ATTSendDTMFToneConfEvent_t? sendDTMFTone
        {
            #region sendDTMFTone

            get
            {
                return m_UnionObject as ATTSendDTMFToneConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion sendDTMFTone
        }
    }
}
