using UnityEngine;
using System.Collections;
using NetWorkFrame;
using protocol;
using UnityEngine.UI;
using System.Text;

public class SendPacket : Singleton<SendPacket>
{
    void Start()
    {
        DontDestroyOnLoad(this);
        //接收消息 然后发送相应的数据到server

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
        EnterLiveHallReq req = new EnterLiveHallReq();
        req.userId = "aa";
        req.userPassword = "ccccccc";
        //NetworkManager.GetInstance().SendPacket<EnterLiveHallReq>(ENetworkMessage.ENTER_LIVE_HALL_REQ, req);
    }


    //退出直播大厅
    private void exitLiveHallReq()
    {
        ExitLiveHallReq req = new ExitLiveHallReq();
        //NetworkManager.GetInstance().SendPacket<ExitLiveHallReq>(ENetworkMessage.EXIT_LIVE_HALL_REQ, req);
    }

    //进入一个直播房间请求
    private void enterLiveroomReq()
    {
        EnterLiveRoomReq req = new EnterLiveRoomReq();
        req.liveId = " i am liveId";
        req.userId = "aa";
        //NetworkManager.GetInstance().SendPacket<EnterLiveRoomReq>(ENetworkMessage.ENTER_LIVE_ROOM_REQ, req);
    }


    //退出一个直播房间请求
    private void exitLiveroomReq()
    {
        ExitLiveRoomReq req = new ExitLiveRoomReq();
        req.userId = "";
        //NetworkManager.GetInstance().SendPacket<ExitLiveRoomReq>(ENetworkMessage.EXIT_LIVE_ROOM_REQ, req);
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

    private void talkOnReq(bool value)
    {
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
