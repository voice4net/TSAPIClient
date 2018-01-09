using System;
using System.Runtime.InteropServices;

namespace TSAPIClient.CSTA
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void EsrFunc(uint esrParam);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool EnumServerNamesCB(IntPtr serverName, uint lParam);

    internal static class Proxy
    {
        [DllImport("csta32.dll")]
        internal static extern int acsEnumServerNames(StreamType_t streamType, IntPtr callback, uint lParam);

        [DllImport("csta32.dll")]
        internal static extern int acsOpenStream(ref IntPtr acsHandle, InvokeIDType_t invokeIDType, uint invokeID, StreamType_t streamType, ref ServerID_t serverID, ref LoginID_t loginID, ref Passwd_t passwd, ref AppName_t applicationName, Level_t acsLevelReq, ref Version_t apiVer, ushort sendQSize, ushort sendExtraBufs, ushort recvQSize, ushort recvExtraBufs, ref PrivateData_t privateData);
        
        [DllImport("csta32.dll")]
        internal static extern int acsCloseStream(IntPtr acsHandle, uint invokeID, ref PrivateData_t priv);

        [DllImport("csta32.dll")]
        internal static extern int acsAbortStream(IntPtr acsHandle, ref PrivateData_t priv);

        [DllImport("csta32.dll")]
        internal static extern int acsFlushEventQueue(IntPtr acsHandle);

        [DllImport("csta32.dll")]
        internal static extern int acsGetEventPoll(IntPtr acsHandle, ref EventBuf_t eventBuf, ref ushort eventBufSize, ref PrivateData_t privData, ref ushort numEvents);

        [DllImport("csta32.dll")]
        internal static extern int acsSetESR(IntPtr acsHandle, IntPtr esr, uint esrParam, bool notifyAll);

        [DllImport("csta32.dll")]
        internal static extern int acsSetHeartbeatInterval(IntPtr acsHandle, uint invokeID, ushort heartbeatInterval, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int acsGetEventBlock(IntPtr acsHandle, ref EventBuf_t eventBuf, ref ushort eventBufSize, ref PrivateData_t privData, ref ushort numEvents);

        [DllImport("csta32.dll")]
        internal static extern int acsEventNotify(IntPtr acsHandle, IntPtr hwnd, int msg, bool notifyAll);

        [DllImport("csta32.dll")]
        internal static extern int acsGetFile(IntPtr acsHandle);

        [DllImport("csta32.dll")]
        internal static extern int cstaAlternateCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t activeCall, ref ConnectionID_t otherCall, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaAlternateCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t activeCall, ref ConnectionID_t otherCall, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaAnswerCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t alertingCall, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaAnswerCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t alertingCall, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaCallCompletion(IntPtr acsHandle, uint invokeID, Feature_t feature, ref ConnectionID_t call, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaClearCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t call, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaClearCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t call, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaClearConnection(IntPtr acsHandle, uint invokeID, ref ConnectionID_t call, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaConferenceCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t heldCall, ref ConnectionID_t activeCall, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaConferenceCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t heldCall, ref ConnectionID_t activeCall, IntPtr privateData);
        
        [DllImport("csta32.dll")]
        internal static extern int cstaConsultationCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t activeCall, ref DeviceID_t calledDevice, ref PrivateData_t privateData);        

        [DllImport("csta32.dll")]
        internal static extern int cstaDeflectCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t deflectCall, ref DeviceID_t calledDevice, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaHoldCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t activeCall, bool reservation, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaHoldCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t activeCall, bool reservation, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaMakeCall(IntPtr acsHandle, uint invokeID, ref DeviceID_t callingDevice, ref DeviceID_t calledDevice, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaMakePredictiveCall(IntPtr acsHandle, uint invokeID, ref DeviceID_t callingDevice, ref DeviceID_t calledDevice, AllocationState_t allocationState, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaPickupCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t deflectCall, ref DeviceID_t calledDevice, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaPickupCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t deflectCall, ref DeviceID_t calledDevice, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaReconnectCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t activeCall, ref ConnectionID_t heldCall, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaRetrieveCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t heldCall, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaRetrieveCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t heldCall, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaTransferCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t heldCall, ref ConnectionID_t activeCall, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaTransferCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t heldCall, ref ConnectionID_t activeCall, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSetMsgWaitingInd(IntPtr acsHandle, uint invokeID, ref DeviceID_t device, bool messages, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSetDoNotDisturb(IntPtr acsHandle, uint invokeID, ref DeviceID_t device, bool doNotDisturb, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSetDoNotDisturb(IntPtr acsHandle, uint invokeID, ref DeviceID_t device, bool doNotDisturb, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSetForwarding(IntPtr acsHandle, uint invokeID, ref DeviceID_t device, ForwardingType_t forwardingType, bool forwardingOn, ref DeviceID_t forwardingDestination, ref PrivateData_t privateData);
       
        [DllImport("csta32.dll")]
        internal static extern int cstaSetAgentState(IntPtr acsHandle, uint invokeID, ref DeviceID_t device, AgentMode_t agentMode, ref AgentID_t agentID, ref DeviceID_t agentGroup, ref AgentPassword_t agentPassword, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSetAgentState(IntPtr acsHandle, uint invokeID, ref DeviceID_t device, AgentMode_t agentMode, ref AgentID_t agentID, ref DeviceID_t agentGroup, ref AgentPassword_t agentPassword, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSetAgentState(IntPtr acsHandle, uint invokeID, ref DeviceID_t device, AgentMode_t agentMode, ref AgentID_t agentID, ref DeviceID_t agentGroup, [MarshalAs(UnmanagedType.LPTStr)] string agentPassword, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSetAgentState(IntPtr acsHandle, uint invokeID, ref DeviceID_t device, AgentMode_t agentMode, ref AgentID_t agentID, ref DeviceID_t agentGroup, IntPtr agentPassword, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSetAgentState(IntPtr acsHandle, uint invokeID, ref DeviceID_t device, AgentMode_t agentMode, ref AgentID_t agentID, IntPtr agentGroup, IntPtr agentPassword, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaQueryMsgWaitingInd(IntPtr acsHandle, uint invokeID, ref DeviceID_t device, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaQueryDoNotDisturb(IntPtr acsHandle, uint invokeID, ref DeviceID_t device, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaQueryForwarding(IntPtr acsHandle, uint invokeID, ref DeviceID_t device, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaQueryAgentState(IntPtr acsHandle, uint invokeID, ref DeviceID_t device, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaQueryLastNumber(IntPtr acsHandle, uint invokeID, ref DeviceID_t device, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaQueryDeviceInfo(IntPtr acsHandle, uint invokeID, ref DeviceID_t deviceID, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaQueryDeviceInfo(IntPtr acsHandle, uint invokeID, ref DeviceID_t deviceID, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaMonitorDevice(IntPtr acsHandle, uint invokeID, ref DeviceID_t deviceID, ref CSTAMonitorFilter_t monitorFilter, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaMonitorCall(IntPtr acsHandle, uint invokeID, ref ConnectionID_t call, ref CSTAMonitorFilter_t monitorFilter, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaMonitorCallsViaDevice(IntPtr acsHandle, uint invokeID, ref DeviceID_t deviceID, ref CSTAMonitorFilter_t monitorFilter, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaMonitorCallsViaDevice(IntPtr acsHandle, uint invokeID, ref DeviceID_t deviceID, ref CSTAMonitorFilter_t monitorFilter, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaChangeMonitorFilter(IntPtr acsHandle, uint invokeID, int monitorCrossRefID, ref CSTAMonitorFilter_t filterlist, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaMonitorStop(IntPtr acsHandle, uint invokeID, int monitorCrossRefID, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSnapshotCallReq(IntPtr acsHandle, uint invokeID, ref ConnectionID_t snapshotObj, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSnapshotCallReq(IntPtr acsHandle, uint invokeID, ref ConnectionID_t snapshotObj, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSnapshotDeviceReq(IntPtr acsHandle, uint invokeID, ref DeviceID_t snapshotObj, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSnapshotDeviceReq(IntPtr acsHandle, uint invokeID, ref DeviceID_t snapshotObj, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaRouteRegisterReq(IntPtr acsHandle, uint invokeID, ref DeviceID_t routingDevice, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaRouteRegisterCancel(IntPtr acsHandle, uint invokeID, int routeRegisterReqID, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaRouteSelect(IntPtr acsHandle, int routeRegisterReqID, int routingCrossRefID, ref DeviceID_t routeSelected, short remainRetry, ref SetUpValues_t setupInformation, bool routeUsedReq, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaRouteEnd(IntPtr acsHandle, int routeRegisterReqID, int routingCrossRefID, CSTAUniversalFailure_t errorValue, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaRouteSelectInv(IntPtr acsHandle, uint invokeID, int routeRegisterReqID, int routingCrossRefID, ref DeviceID_t routeSelected, short remainRetry, ref SetUpValues_t setupInformation, bool routeUsedReq, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaRouteEndInv(IntPtr acsHandle, uint invokeID, int routeRegisterReqID, int routingCrossRefID, CSTAUniversalFailure_t errorValue, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaEscapeService(IntPtr acsHandle, uint invokeID, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaEscapeServiceConf(IntPtr acsHandle, uint invokeID, CSTAUniversalFailure_t error, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSendPrivateEvent(IntPtr acsHandle, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSysStatReq(IntPtr acsHandle, uint invokeID, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSysStatReq(IntPtr acsHandle, uint invokeID, IntPtr privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSysStatStart(IntPtr acsHandle, uint invokeID, byte statusFilter, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSysStatStop(IntPtr acsHandle, uint invokeID, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaChangeSysStatFilter(IntPtr acsHandle, uint invokeID, byte statusFilter, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSysStatReqConf(IntPtr acsHandle, uint invokeID, SystemStatus_t systemStatus, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaSysStatEvent(IntPtr acsHandle, SystemStatus_t systemStatus, ref PrivateData_t privateData);

        [DllImport("csta32.dll")]
        internal static extern int cstaGetAPICaps(IntPtr acsHandle, uint invokeID);

        [DllImport("csta32.dll")]
        internal static extern int cstaGetDeviceList(IntPtr acsHandle, uint invokeID, int index, CSTALevel_t level);

        [DllImport("csta32.dll")]
        internal static extern int cstaQueryCallMonitor(IntPtr acsHandle, uint invokeID);
    }
}
