using UnityEngine;
using System.Collections;
using NetWorkFrame;
using protocol;
using UnityEngine.UI;
using System.Text;

public class ReceivePacket : Singleton<ReceivePacket>
{
   
    void Start()
    {
        //
        DontDestroyOnLoad(this);
        
        //收到server 不同的数据后的不同处理
        MessageDispatcher.GetInstance().RegisterMessageHandler((uint)EModelMessage.SOCKET_CONNECTED, testReq);

        MessageDispatcher.GetInstance().RegisterMessageHandler((uint)ENetworkMessage.ENTER_LIVE_HALL_RSP, enterLiveHallRsp);

        MessageDispatcher.GetInstance().RegisterMessageHandler((uint)ENetworkMessage.EXIT_LIVE_HALL_RSP, exitLiveHallRsp);

        MessageDispatcher.GetInstance().RegisterMessageHandler((uint)ENetworkMessage.ENTER_LIVE_ROOM_RSP, enterLiveroomRsp);
        MessageDispatcher.GetInstance().RegisterMessageHandler((uint)ENetworkMessage.ENTER_LIVE_ROOM_SYNC, enterLiveroomSyn);

        MessageDispatcher.GetInstance().RegisterMessageHandler((uint)ENetworkMessage.EXIT_LIVE_ROOM_RSP, exitLiveroomRsp);
        MessageDispatcher.GetInstance().RegisterMessageHandler((uint)ENetworkMessage.EXIT_LIVE_ROOM_SYNC, exitLiveroomSyn);

        MessageDispatcher.GetInstance().RegisterMessageHandler((uint)ENetworkMessage.MOVE_POSITION_RSP, movePositionRsp);
        MessageDispatcher.GetInstance().RegisterMessageHandler((uint)ENetworkMessage.MOVE_POSITION_SYNC, movePositionSyn);

        MessageDispatcher.GetInstance().RegisterMessageHandler((uint)ENetworkMessage.TALKING_RSP, talkOnRsp);

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void testReceived(uint iMessageType, object kParam)
    {
        TestRsp rsp = kParam as TestRsp;
        Log4U.LogInfo("res string = " + rsp.resultString);
        //foreach(var x in rsp.resultNumber)
        //{
        //    Log4U.LogInfo("res numb = " + x.number2);

        //}

    }


    //登录到直播大厅
    private void enterLiveHallRsp(uint iMessageType, object kParam)
    {
        // Todo:
        EnterLiveHallRsp rsp = kParam as EnterLiveHallRsp;
       
    }

    //退出直播大厅
    private void exitLiveHallRsp(uint iMessageType, object kParam)
    {
        // Todo:

        ExitLiveHallRsp rsp = kParam as ExitLiveHallRsp;
    }

    //进入一个直播房间
    private void enterLiveroomRsp(uint iMessageType, object kParam)
    {
        // Todo:
        EnterLiveRoomRsp rsp = kParam as EnterLiveRoomRsp;
        var s = rsp.users;
        StringBuilder text = new StringBuilder("", 12);

        text.Append("enterLiveroomRsp * resultCode :" + rsp.resultCode
           + "\n rsp.resultMessage : " + rsp.resultMessage + "\n userId : ");
        foreach (var v in s)
        {
            text.Append("\n" + v.userName);
        }

    }
    private void enterLiveroomSyn(uint iMessageType, object kParam)
    {
        //EnterLiveRoomReq rsp = kParam as EnterLiveRoomReq;
        EnterLiveRoomSync rsp = kParam as EnterLiveRoomSync;
        StringBuilder text = new StringBuilder("", 12);

        text.Append("\n userId : ");
        var s = rsp.users;
        if (s != null)
        {
            foreach (var v in s)
            {
                text.Append("\n" + v.userName);
            }
        }

    }
    
    //退出一个直播房间
    private void exitLiveroomRsp(uint iMessageType, object kParam)
    {
        // Todo:
        ExitLiveRoomRsp rsp = kParam as ExitLiveRoomRsp;
    }
    private void exitLiveroomSyn(uint iMessageType, object kParam)
    {
        // Todo:
        ExitLiveRoomSync rsp = kParam as ExitLiveRoomSync;
        var s = rsp.users;
        StringBuilder text = new StringBuilder("", 12);

        text.Append("\n userId : ");
        foreach (var v in s)
        {
            text.Append("\n" + v.userName);
        }
    }
    
    //移动位置
    private void movePositionRsp(uint iMessageType, object kParam)
    {
        // Todo:
        MovePositionRsp rsp = kParam as MovePositionRsp;
        var s = rsp.resultCode;
        StringBuilder text = new StringBuilder("", 12);

        text.Append("\n move request : ");
        //if(s == MovePositionRsp.ResultCode.SUCCESS)
        {
            text.Append("\n" + s.ToString());
        }
        
    }
    private void movePositionSyn(uint iMessageType, object kParam)
    {
        // Todo:
    }


    private void talkOnReq(bool value)
    {
        
    }
    private void talkOnRsp(uint iMessageType, object kParam)
    {
        // Todo:
        TalkingRsp rsp = kParam as TalkingRsp;
        var s = rsp.resultCode;
        StringBuilder text = new StringBuilder("", 12);

        text.Append("\n talk respond : ");
        //if(s == MovePositionRsp.ResultCode.SUCCESS)
        {
            text.Append("\n" + s.ToString());
        }
        
    }

    private void talkOnSyn(uint iMessageType, object kParam)
    {
        // Todo:
    }

    private void testReq(uint iMessageType, object kParam)
    {
        ExitLiveRoomSync rsp = kParam as ExitLiveRoomSync;

        //TestReq req = new TestReq()
        //{
        //    string1 = "wang",
        //    string2 = "liang",
        //};
        //req.numbers.Add(new addNumber { number1 = 1, number2 = 1 });
        //req.numbers.Add(new addNumber { number1 = 2, number2 = 3 });
        //MessageDispatcher.GetInstance().DispatchMessage((uint)EModelMessage.TRY_LOGIN, null);
        //NetworkManager.GetInstance().SendPacket<TestReq>(ENetworkMessage.TEST_ZHAODOND_REQ, req);
    }
}
