using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Common
{
    public enum ConnectionEventMessages
    {
        None,

        Idle,
        Connecting,
        Connected,
        Disconnecting,
        Disconnected,

        RingHub,
        RingHub_Fail,
        RingHub_Success,

        SignOn,
        SignOn_Fail,
        SignOn_Success,

        SignOff,
        SignOff_Fail,
        SignOff_Success,

        SendingSMS,
        SendingSMS_Fail,
        SendingSMS_Success,

        OperationTimeout,

        OpenCommunication,
        OpenCommunication_Fail,
        OpenCommunication_Success,

        CloseCommunication,
        CloseCommunication_Fail,
        CloseCommunication_Success,

        CommandExecuting,
        CommandExecuted_Fail,
        CommandExecuted_Success,

        ReadingFromDevice,
        ReadingFromDevice_Fail,
        WritingToDevice,
        WritingToDevice_Fail
    }
}
