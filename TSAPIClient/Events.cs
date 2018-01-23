using System;
using TSAPIClient.ATT;
using TSAPIClient.CSTA;

namespace TSAPIClient
{
    public class TSAPIEventArgs : EventArgs
    {
        public CSTAEvent_t cstaEvent { get; set; }

        public ATTEvent_t attEvent { get; set; }

        public PrivateData_t? privateData { get; set; }

        private string m_SequenceNumber = string.Empty;
        public string SequenceNumber
        {
            get { return m_SequenceNumber; }
            set { m_SequenceNumber = value; }
        }
    }
}
