﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEServerWindows
{
    public enum CommandType : byte
    {
        CMD_GETVERSION = 0,
        CMD_CLOSECONNECTION = 1,
        CMD_TERMINATESERVER = 2,
        CMD_OPENPROCESS = 3,
        CMD_CREATETOOLHELP32SNAPSHOT = 4,
        CMD_PROCESS32FIRST = 5,
        CMD_PROCESS32NEXT = 6,
        CMD_CLOSEHANDLE = 7,
        CMD_VIRTUALQUERYEX = 8,
        CMD_READPROCESSMEMORY = 9,
        CMD_WRITEPROCESSMEMORY = 10,
        CMD_STARTDEBUG = 11,
        CMD_STOPDEBUG = 12,
        CMD_WAITFORDEBUGEVENT = 13,
        CMD_CONTINUEFROMDEBUGEVENT = 14,
        CMD_SETBREAKPOINT = 15,
        CMD_REMOVEBREAKPOINT = 16,
        CMD_SUSPENDTHREAD = 17,
        CMD_RESUMETHREAD = 18,
        CMD_GETTHREADCONTEXT = 19,
        CMD_SETTHREADCONTEXT = 20,
        CMD_GETARCHITECTURE = 21,
        CMD_MODULE32FIRST = 22,
        CMD_MODULE32NEXT = 23,
        CMD_GETSYMBOLLISTFROMFILE = 24,
        CMD_LOADEXTENSION = 25,
        CMD_ALLOC = 26,
        CMD_FREE = 27,
        CMD_CREATETHREAD = 28,
        CMD_LOADMODULE = 29,
        CMD_SPEEDHACK_SETSPEED = 30,
        CMD_VIRTUALQUERYEXFULL = 31,
        CMD_GETREGIONINFO = 32,
        CMD_COMMANDLIST2 = 255
    }

}