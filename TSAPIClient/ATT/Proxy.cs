using System;
using System.Runtime.InteropServices;
using System.Text;
using TSAPIClient.CSTA;

namespace TSAPIClient.ATT
{
    internal static class Proxy
    {
        [DllImport("attprv32.dll")]
        internal static extern int attMakeVersionString([MarshalAs(UnmanagedType.LPStr)] string requestedVersion, StringBuilder supportedVersion);

        [DllImport("attprv32.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern int attPrivateData(ref PrivateData_t privateData, ref ATTEventBuf_t attEventBuf);

        [DllImport("attprv32.dll")]
        internal static extern int attV6ClearConnection(ref PrivateData_t privateData, ATTDropResource_t dropResource, ref ATTUserToUserInfo_t userInfo);

        [DllImport("attprv32.dll")]
        internal static extern int attV6MakeCall(ref PrivateData_t privateData, ref DeviceID_t destRoute, bool priorityCalling, ref ATTUserToUserInfo_t userInfo);

        [DllImport("attprv32.dll")]
        internal static extern int attQueryAcdSplit(ref PrivateData_t privateData, ref DeviceID_t device);

        [DllImport("attprv32.dll")]
        internal static extern int attQueryUCID(ref PrivateData_t privateData, ref ConnectionID_t call);

        [DllImport("attprv32.dll")]
        internal static extern int attMonitorFilterExt(ref PrivateData_t privateData, ref ATTPrivateFilter_t privateFilter);

        [DllImport("attprv32.dll")]
        internal static extern int attV6ConsultationCall(ref PrivateData_t privateData, ref DeviceID_t destRoute, bool priorityCalling, ref ATTUserToUserInfo_t userInfo);

        [DllImport("attprv32.dll")]
        internal static extern int attV6ReconnectCall(ref PrivateData_t privateData, ATTDropResource_t dropResource, ref ATTUserToUserInfo_t userInfo);

        [DllImport("attprv32.dll")]
        internal static extern int attV6SetAgentState(ref PrivateData_t privateData, ATTWorkMode_t workMode, int reasonCode, bool enablePending);

        [DllImport("attprv32.dll")]
        internal static extern int attSingleStepTransferCall(ref PrivateData_t privateData, ref ConnectionID_t activeCall, ref DeviceID_t transferredTo);

        [DllImport("attprv32.dll")]
        internal static extern int attMonitorCallsViaDevice(ref PrivateData_t privateData, ref ATTPrivateFilter_t privateFilter, bool flowPredictiveCallEvents);

        [DllImport("attprv32.dll")]
        internal static extern int attSendDTMFToneExt(ref PrivateData_t privateData, ref ConnectionID_t sender, IntPtr receivers, [MarshalAs(UnmanagedType.LPStr)] string tones, short toneDuration, short pauseDuration);
    }
}
