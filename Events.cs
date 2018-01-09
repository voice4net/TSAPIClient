using System;

namespace TSAPIClient.CSTA
{
    public class CSTAEvent_t : EventArgs
    {
        public ACSEventHeader_t eventHeader { get; set; }

        private CSTAEvent_Union m_Event = new CSTAEvent_Union();
        public CSTAEvent_Union Event
        {
            get { return m_Event; }
            set { m_Event = value; }
        }

        public char[] heap { get; set; }
    }

    public class CSTAEvent_Union
    {
        public ACSUnsolicitedEvent acsUnsolicited { get; set; }

        public ACSConfirmationEvent acsConfirmation { get; set; }

        public CSTARequestEvent cstaRequest { get; set; }

        public CSTAUnsolicitedEvent cstaUnsolicited { get; set; }

        public CSTAConfirmationEvent cstaConfirmation { get; set; }
    }

    public class ACSUnsolicitedEvent
    {
        public ACSUniversalFailureEvent_t failureEvent;
    }

    public class ACSConfirmationEvent
    {
        public int invokeID { get; set; }

        private readonly ACSConfirmationEvent_Union m_union = new ACSConfirmationEvent_Union();
        public ACSConfirmationEvent_Union u
        {
            get { return m_union; }
        }
    }

    public class ACSConfirmationEvent_Union
    {
        private object m_UnionObject = new object();

        public ACSOpenStreamConfEvent_t? acsopen
        {
            #region acsopen

            get
            {
                return m_UnionObject as ACSOpenStreamConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion acsopen
        }

        public ACSCloseStreamConfEvent_t? acsclose
        {
            #region acsclose

            get
            {
                return m_UnionObject as ACSCloseStreamConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion acsclose
        }

        public ACSUniversalFailureConfEvent_t? failureEvent
        {
            #region failureEvent

            get
            {
                return m_UnionObject as ACSUniversalFailureConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion failureEvent
        }

        public ACSSetHeartbeatIntervalConfEvent_t? acssetheartbeatinterval
        {
            #region acssetheartbeatinterval

            get
            {
                return m_UnionObject as ACSSetHeartbeatIntervalConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion acssetheartbeatinterval
        }
    }

    public class CSTARequestEvent
    {
        public int invokeID { get; set; }

        private CSTARequestEvent_Union m_union = new CSTARequestEvent_Union();
        public CSTARequestEvent_Union u
        {
            get { return m_union; }
            set { m_union = value; }
        }
    }

    public class CSTARequestEvent_Union
    {
        private object m_UnionObject = new object();

        public CSTARouteRequestEvent_t? routeRequest
        {
            #region routeRequest

            get
            {
                return m_UnionObject as CSTARouteRequestEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion routeRequest
        }

        public CSTARouteRequestExtEvent_t? routeRequestExt
        {
            #region routeRequestExt

            get
            {
                return m_UnionObject as CSTARouteRequestExtEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion routeRequestExt
        }

        public CSTAReRouteRequest_t? reRouteRequest
        {
            #region reRouteRequest

            get
            {
                return m_UnionObject as CSTAReRouteRequest_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion reRouteRequest
        }

        public CSTAEscapeSvcReqEvent_t? escapeSvcRequest
        {
            #region escapeSvcRequest

            get
            {
                return m_UnionObject as CSTAEscapeSvcReqEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion escapeSvcRequest
        }

        public CSTASysStatReqEvent_t? sysStatRequest
        {
            #region sysStatRequest

            get
            {
                return m_UnionObject as CSTASysStatReqEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion sysStatRequest
        }
    }

    public class CSTAUnsolicitedEvent
    {
        public int monitorCrossRefId { get; set; }

        private CSTAUnsolicitedEvent_Union m_union = new CSTAUnsolicitedEvent_Union();
        public CSTAUnsolicitedEvent_Union u
        {
            get { return m_union; }
            set { m_union = value; }
        }
    }

    public class CSTAUnsolicitedEvent_Union
    {
        private object m_UnionObject = new object();

        public CSTACallClearedEvent_t? callCleared
        {
            #region callCleared

            get
            {
                return m_UnionObject as CSTACallClearedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion callCleared
        }

        public CSTAConferencedEvent_t? conferenced
        {
            #region conferenced

            get
            {
                return m_UnionObject as CSTAConferencedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion conferenced
        }

        public CSTAConnectionClearedEvent_t? connectionCleared
        {
            #region connectionCleared

            get
            {
                return m_UnionObject as CSTAConnectionClearedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion connectionCleared
        }

        public CSTADeliveredEvent_t? delivered
        {
            #region delivered

            get
            {
                return m_UnionObject as CSTADeliveredEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion delivered
        }

        public CSTADivertedEvent_t? diverted
        {
            #region diverted

            get
            {
                return m_UnionObject as CSTADivertedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion diverted
        }

        public CSTAEstablishedEvent_t? established
        {
            #region established

            get
            {
                return m_UnionObject as CSTAEstablishedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion established
        }

        public CSTAFailedEvent_t? failed
        {
            #region failed

            get
            {
                return m_UnionObject as CSTAFailedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion failed
        }

        public CSTAHeldEvent_t? held
        {
            #region held

            get
            {
                return m_UnionObject as CSTAHeldEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion held
        }

        public CSTANetworkReachedEvent_t? networkReached
        {
            #region networkReached

            get
            {
                return m_UnionObject as CSTANetworkReachedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion networkReached
        }

        public CSTAOriginatedEvent_t? originated
        {
            #region originated

            get
            {
                return m_UnionObject as CSTAOriginatedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion originated
        }

        public CSTAQueuedEvent_t? queued
        {
            #region queued

            get
            {
                return m_UnionObject as CSTAQueuedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queued
        }

        public CSTARetrievedEvent_t? retrieved
        {
            #region retrieved

            get
            {
                return m_UnionObject as CSTARetrievedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion retrieved
        }

        public CSTAServiceInitiatedEvent_t? serviceInitiated
        {
            #region serviceInitiated

            get
            {
                return m_UnionObject as CSTAServiceInitiatedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion serviceInitiated
        }

        public CSTATransferredEvent_t? transferred
        {
            #region transferred

            get
            {
                return m_UnionObject as CSTATransferredEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion transferred
        }

        public CSTACallInformationEvent_t? callInformation
        {
            #region callInformation

            get
            {
                return m_UnionObject as CSTACallInformationEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion callInformation
        }

        public CSTADoNotDisturbEvent_t? doNotDisturb
        {
            #region doNotDisturb

            get
            {
                return m_UnionObject as CSTADoNotDisturbEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion doNotDisturb
        }

        public CSTAForwardingEvent_t? forwarding
        {
            #region forwarding

            get
            {
                return m_UnionObject as CSTAForwardingEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion forwarding
        }

        public CSTAMessageWaitingEvent_t? messageWaiting
        {
            #region messageWaiting

            get
            {
                return m_UnionObject as CSTAMessageWaitingEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion messageWaiting
        }

        public CSTALoggedOnEvent_t? loggedOn
        {
            #region loggedOn

            get
            {
                return m_UnionObject as CSTALoggedOnEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion loggedOn
        }

        public CSTALoggedOffEvent_t? loggedOff
        {
            #region loggedOff

            get
            {
                return m_UnionObject as CSTALoggedOffEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion loggedOff
        }

        public CSTANotReadyEvent_t? notReady
        {
            #region notReady

            get
            {
                return m_UnionObject as CSTANotReadyEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion notReady
        }

        public CSTAReadyEvent_t? ready
        {
            #region ready

            get
            {
                return m_UnionObject as CSTAReadyEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion ready
        }

        public CSTAWorkNotReadyEvent_t? workNotReady
        {
            #region workNotReady

            get
            {
                return m_UnionObject as CSTAWorkNotReadyEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion workNotReady
        }

        public CSTAWorkReadyEvent_t? workReady
        {
            #region workReady

            get
            {
                return m_UnionObject as CSTAWorkReadyEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion workReady
        }

        public CSTABackInServiceEvent_t? backInService
        {
            #region backInService

            get
            {
                return m_UnionObject as CSTABackInServiceEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion backInService
        }

        public CSTAOutOfServiceEvent_t? outOfService
        {
            #region outOfService

            get
            {
                return m_UnionObject as CSTAOutOfServiceEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion outOfService
        }

        public CSTAPrivateStatusEvent_t? privateStatus
        {
            #region privateStatus

            get
            {
                return m_UnionObject as CSTAPrivateStatusEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion privateStatus
        }

        public CSTAMonitorEndedEvent_t? monitorEnded
        {
            #region monitorEnded

            get
            {
                return m_UnionObject as CSTAMonitorEndedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion monitorEnded
        }
    }

    public class CSTAConfirmationEvent
    {
        public int invokeID { get; set; }

        private CSTAConfirmationEvent_Union m_union = new CSTAConfirmationEvent_Union();
        public CSTAConfirmationEvent_Union u
        {
            get { return m_union; }
            set { m_union = value; }
        }
    }

    public class CSTAConfirmationEvent_Union
    {
        private object m_UnionObject = new object();

        public CSTAAlternateCallConfEvent_t? alternateCall
        {
            #region alternateCall

            get
            {
                return m_UnionObject as CSTAAlternateCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion alternateCall
        }

        public CSTAAnswerCallConfEvent_t? answerCall
        {
            #region answerCall

            get
            {
                return m_UnionObject as CSTAAnswerCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion answerCall
        }

        public CSTACallCompletionConfEvent_t? callCompletion
        {
            #region callCompletion

            get
            {
                return m_UnionObject as CSTACallCompletionConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion callCompletion
        }

        public CSTAClearCallConfEvent_t? clearCall
        {
            #region clearCall

            get
            {
                return m_UnionObject as CSTAClearCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion clearCall
        }

        public CSTAClearConnectionConfEvent_t? clearConnection
        {
            #region clearConnection

            get
            {
                return m_UnionObject as CSTAClearConnectionConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion clearConnection
        }

        public CSTAConferenceCallConfEvent_t? conferenceCall
        {
            #region conferenceCall

            get
            {
                return m_UnionObject as CSTAConferenceCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion conferenceCall
        }

        public CSTAConsultationCallConfEvent_t? consultationCall
        {
            #region consultationCall

            get
            {
                return m_UnionObject as CSTAConsultationCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion consultationCall
        }

        public CSTADeflectCallConfEvent_t? deflectCall
        {
            #region deflectCall

            get
            {
                return m_UnionObject as CSTADeflectCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion deflectCall
        }

        public CSTAPickupCallConfEvent_t? pickupCall
        {
            #region pickupCall

            get
            {
                return m_UnionObject as CSTAPickupCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion pickupCall
        }

        public CSTAGroupPickupCallConfEvent_t? groupPickupCall
        {
            #region groupPickupCall

            get
            {
                return m_UnionObject as CSTAGroupPickupCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion groupPickupCall
        }

        public CSTAHoldCallConfEvent_t? holdCall
        {
            #region holdCall

            get
            {
                return m_UnionObject as CSTAHoldCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion holdCall
        }

        public CSTAMakeCallConfEvent_t? makeCall
        {
            #region makeCall

            get
            {
                return m_UnionObject as CSTAMakeCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion makeCall
        }

        public CSTAMakePredictiveCallConfEvent_t? makePredictiveCall
        {
            #region makePredictiveCall

            get
            {
                return m_UnionObject as CSTAMakePredictiveCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion makePredictiveCall
        }

        public CSTAQueryMwiConfEvent_t? queryMwi
        {
            #region queryMwi

            get
            {
                return m_UnionObject as CSTAQueryMwiConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryMwi
        }

        public CSTAQueryDndConfEvent_t? queryDnd
        {
            #region queryDnd

            get
            {
                return m_UnionObject as CSTAQueryDndConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryDnd
        }

        public CSTAQueryFwdConfEvent_t? queryFwd
        {
            #region queryFwd

            get
            {
                return m_UnionObject as CSTAQueryFwdConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryFwd
        }

        public CSTAQueryAgentStateConfEvent_t? queryAgentState
        {
            #region queryAgentState

            get
            {
                return m_UnionObject as CSTAQueryAgentStateConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryAgentState
        }

        public CSTAQueryLastNumberConfEvent_t? queryLastNumber
        {
            #region queryLastNumber

            get
            {
                return m_UnionObject as CSTAQueryLastNumberConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryLastNumber
        }

        public CSTAQueryDeviceInfoConfEvent_t? queryDeviceInfo
        {
            #region queryDeviceInfo

            get
            {
                return m_UnionObject as CSTAQueryDeviceInfoConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryDeviceInfo
        }

        public CSTAReconnectCallConfEvent_t? reconnectCall
        {
            #region reconnectCall

            get
            {
                return m_UnionObject as CSTAReconnectCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion reconnectCall
        }

        public CSTARetrieveCallConfEvent_t? retrieveCall
        {
            #region retrieveCall

            get
            {
                return m_UnionObject as CSTARetrieveCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion retrieveCall
        }

        public CSTASetMwiConfEvent_t? setMwi
        {
            #region setMwi

            get
            {
                return m_UnionObject as CSTASetMwiConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion setMwi
        }

        public CSTASetDndConfEvent_t? setDnd
        {
            #region setDnd

            get
            {
                return m_UnionObject as CSTASetDndConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion setDnd
        }

        public CSTASetFwdConfEvent_t? setFwd
        {
            #region setFwd

            get
            {
                return m_UnionObject as CSTASetFwdConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion setFwd
        }

        public CSTASetAgentStateConfEvent_t? setAgentState
        {
            #region setAgentState

            get
            {
                return m_UnionObject as CSTASetAgentStateConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion setAgentState
        }

        public CSTATransferCallConfEvent_t? transferCall
        {
            #region transferCall

            get
            {
                return m_UnionObject as CSTATransferCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion transferCall
        }

        public CSTAUniversalFailureConfEvent_t? universalFailure
        {
            #region universalFailure

            get
            {
                return m_UnionObject as CSTAUniversalFailureConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion universalFailure
        }

        public CSTAMonitorConfEvent_t? monitorStart
        {
            #region monitorStart

            get
            {
                return m_UnionObject as CSTAMonitorConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion monitorStart
        }

        public CSTAChangeMonitorFilterConfEvent_t? changeMonitorFilter
        {
            #region changeMonitorFilter

            get
            {
                return m_UnionObject as CSTAChangeMonitorFilterConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion changeMonitorFilter
        }

        public CSTAMonitorStopConfEvent_t? monitorStop
        {
            #region monitorStop

            get
            {
                return m_UnionObject as CSTAMonitorStopConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion monitorStop
        }

        public CSTASnapshotDeviceConfEvent_t? snapshotDevice
        {
            #region snapshotDevice

            get
            {
                return m_UnionObject as CSTASnapshotDeviceConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion snapshotDevice
        }

        public CSTASnapshotCallConfEvent_t? snapshotCall
        {
            #region snapshotCall

            get
            {
                return m_UnionObject as CSTASnapshotCallConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion snapshotCall
        }

        public CSTARouteRegisterReqConfEvent_t? routeRegister
        {
            #region routeRegister

            get
            {
                return m_UnionObject as CSTARouteRegisterReqConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion routeRegister
        }

        public CSTARouteRegisterCancelConfEvent_t? routeCancel
        {
            #region routeCancel

            get
            {
                return m_UnionObject as CSTARouteRegisterCancelConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion routeCancel
        }

        public CSTAEscapeSvcConfEvent_t? escapeService
        {
            #region escapeService

            get
            {
                return m_UnionObject as CSTAEscapeSvcConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion escapeService
        }

        public CSTASysStatReqConfEvent_t? sysStatReq
        {
            #region sysStatReq

            get
            {
                return m_UnionObject as CSTASysStatReqConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion sysStatReq
        }

        public CSTASysStatStartConfEvent_t? sysStatStart
        {
            #region sysStatStart

            get
            {
                return m_UnionObject as CSTASysStatStartConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion sysStatStart
        }

        public CSTASysStatStopConfEvent_t? sysStatStop
        {
            #region sysStatStop

            get
            {
                return m_UnionObject as CSTASysStatStopConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion sysStatStop
        }

        public CSTAChangeSysStatFilterConfEvent_t? changeSysStatFilter
        {
            #region changeSysStatFilter

            get
            {
                return m_UnionObject as CSTAChangeSysStatFilterConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion changeSysStatFilter
        }

        public CSTAGetAPICapsConfEvent_t? getAPICaps
        {
            #region getAPICaps

            get
            {
                return m_UnionObject as CSTAGetAPICapsConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion getAPICaps
        }

        public CSTAGetDeviceListConfEvent_t? getDeviceList
        {
            #region getDeviceList

            get
            {
                return m_UnionObject as CSTAGetDeviceListConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion getDeviceList
        }

        public CSTAQueryCallMonitorConfEvent_t? queryCallMonitor
        {
            #region queryCallMonitor

            get
            {
                return m_UnionObject as CSTAQueryCallMonitorConfEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion queryCallMonitor
        }
    }

    public class CSTAEventReport
    {
        private CSTAEventReport_Union m_union = new CSTAEventReport_Union();
        public CSTAEventReport_Union u
        {
            get { return m_union; }
            set { m_union = value; }
        }
    }

    public class CSTAEventReport_Union
    {
        private object m_UnionObject = new object();

        public CSTARouteRegisterAbortEvent_t? registerAbort
        {
            #region registerAbort

            get
            {
                return m_UnionObject as CSTARouteRegisterAbortEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion registerAbort
        }

        public CSTARouteUsedEvent_t? routeUsed
        {
            #region routeUsed

            get
            {
                return m_UnionObject as CSTARouteUsedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion routeUsed
        }

        public CSTARouteUsedExtEvent_t? routeUsedExt
        {
            #region routeUsedExt

            get
            {
                return m_UnionObject as CSTARouteUsedExtEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion routeUsedExt
        }

        public CSTARouteEndEvent_t? routeEnd
        {
            #region routeEnd

            get
            {
                return m_UnionObject as CSTARouteEndEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion routeEnd
        }

        public CSTAPrivateEvent_t? privateEvent
        {
            #region privateEvent

            get
            {
                return m_UnionObject as CSTAPrivateEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion privateEvent
        }

        public CSTASysStatEvent_t? sysStat
        {
            #region sysStat

            get
            {
                return m_UnionObject as CSTASysStatEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion sysStat
        }

        public CSTASysStatEndedEvent_t? sysStatEnded
        {
            #region sysStatEnded

            get
            {
                return m_UnionObject as CSTASysStatEndedEvent_t?;
            }
            set
            {
                m_UnionObject = value;
            }

            #endregion sysStatEnded
        }
    }

    internal class ACSParseEventArgs
    {
        public PrivateData_t PrivateData { get; set; }

        private byte[] eventBuf = { };
        public byte[] EventBuffer
        {
            get { return eventBuf; }
            set { eventBuf = value; }
        }
    }
}
