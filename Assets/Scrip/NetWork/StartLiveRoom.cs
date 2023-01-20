using UnityEngine;
using System.Collections;
using NetWorkFrame;
using protocol;
using UnityEngine.UI;
using System.Text;
using Protocol;

public class StartLiveRoom: MonoBehaviour
{
    public Text serverInfo;
    public Button Btn_enterHall;
    public Button Btn_exitHall;
    public Button Btn_enterRoom;
    public Button Btn_exitRoom;
    public Button Btn_emoveTest;
    public Toggle Toggle_speaker;
    //public Button Btn_exitHall;

    // Use this for initialization
    void Start()
    {
        //
        DontDestroyOnLoad(this);
        serverInfo.text = "...";
        //测试 通过按钮发出请求
        Btn_enterHall.onClick.AddListener(enterLiveHallReq);
        Btn_exitHall.onClick.AddListener(exitLiveHallReq);
        Btn_enterRoom.onClick.AddListener(enterLiveroomReq);
        Btn_exitRoom.onClick.AddListener(exitLiveroomReq);
        Btn_emoveTest.onClick.AddListener(movePositionReq);
        Toggle_speaker.onValueChanged.AddListener(talkOnReq);
        //  收到server回应后的处理
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
       // MessageDispatcher.GetInstance().RegisterMessageHandler((uint)ENetworkMessage.TALKING_SYNC, talkOnSyn);

        //MessageDispatcher.GetInstance().RegisterMessageHandler((uint)EModelMessage.TEST_RECEIVED, testReceived);

        //StartCoroutine(BeginTryConnect());
        //NetworkManager.GetInstance().OnSocketDisConnected();

        //MessageDispatcher.GetInstance().RegisterMessageHandler((uint)EModelMessage.SOCKET_CONNECTED, testReq);

        MessageDispatcher.GetInstance().DispatchMessageAsync((uint)EModelMessage.SOCKET_DISCONNECTED, null);
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
    private void enterLiveHallReq()
    {
        C2GChangeDir req = new C2GChangeDir();
        req.Dir = 1;
        NetworkManager.GetInstance().SendPacket<C2GChangeDir>(MessageType.ChangeDirection, req);
    }
    //
    private void enterLiveHallRsp(uint iMessageType, object kParam)
    {
        // Todo:
        EnterLiveHallRsp rsp = kParam as EnterLiveHallRsp;
        serverInfo.text = "enterLiveHallRsp* resultCode :" + rsp.resultCode
           + "\n rsp.resultMessage : " + rsp.resultMessage;
    }

    //退出直播大厅
    private void exitLiveHallReq()
    {
        ExitLiveHallReq req = new ExitLiveHallReq();
        //NetworkManager.GetInstance().SendPacket<ExitLiveHallReq>(ENetworkMessage.EXIT_LIVE_HALL_REQ, req);
    }
    private void exitLiveHallRsp(uint iMessageType, object kParam)
    {
        // Todo:

        ExitLiveHallRsp rsp = kParam as ExitLiveHallRsp;
        serverInfo.text = "resultCode :" + rsp.resultCode;
        // + "\n rsp.resultMessage : " + ;
    }

    //进入一个直播房间请求
    private void enterLiveroomReq()
    {
        EnterLiveRoomReq req = new EnterLiveRoomReq();
        req.liveId = " i am liveId";
        req.userId = "aa";
        //NetworkManager.GetInstance().SendPacket<EnterLiveRoomReq>(ENetworkMessage.ENTER_LIVE_ROOM_REQ, req);
    }
    //
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
        serverInfo.text = text.ToString();

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
        serverInfo.text = text.ToString();

    }
    //退出一个直播房间请求
    private void exitLiveroomReq()
    {
        ExitLiveRoomReq req = new ExitLiveRoomReq();
        req.userId = "";
        //NetworkManager.GetInstance().SendPacket<ExitLiveRoomReq>(ENetworkMessage.EXIT_LIVE_ROOM_REQ, req);
    }
    private void exitLiveroomRsp(uint iMessageType, object kParam)
    {
        // Todo:
        ExitLiveRoomRsp rsp = kParam as ExitLiveRoomRsp;
        serverInfo.text = "exitLiveroomRsp"
            + "\nresultCode :" + rsp.resultCode
            + "\n rsp.resultMessage : " + rsp.resultMessage;
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
        serverInfo.text = text.ToString();
    }
    //移动位置请求
    private void movePositionReq()
    {

        MovePositionReq req = new MovePositionReq();
        req.userId = IPConfig.UserId;
        Position p = new Position();
        p.x = 1; p.y = 2; p.z = 3;
        req.position = p;
        //NetworkManager.GetInstance().SendPacket(ENetworkMessage.EXIT_LIVE_ROOM_REQ, req);
    }
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
        serverInfo.text = text.ToString();
    }
    private void movePositionSyn(uint iMessageType, object kParam)
    {
        // Todo:
    }


    private void talkOnReq(bool value)
    {
        if (Toggle_speaker.isOn == true)
        {
            TalkingReq req = new TalkingReq();
            req.userId = IPConfig.UserId;
            //NetworkManager.GetInstance().SendPacket(ENetworkMessage.TALKING_REQ, req);

        }
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
        serverInfo.text = text.ToString();
    }

    private void talkOnSyn(uint iMessageType, object kParam)
    {
        // Todo:
    }

    private void testReq(uint iMessageType, object kParam)
    {
        ExitLiveRoomSync rsp = kParam as ExitLiveRoomSync;

        serverInfo.text = "server connected";
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
