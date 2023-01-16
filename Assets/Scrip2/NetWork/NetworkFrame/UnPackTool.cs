using UnityEngine;
using ProtoBuf;
using System.IO;
using GameJamDemo;
using Protocol;
using Google.Protobuf;

namespace NetWorkFrame
{
    public class UnPackTool
    {
        public static IMessage UnPack(MessageType messageType, int startIndex, int length, byte[] buffer)
        {
            IMessage packet = null;

            try
            {
                using (MemoryStream streamForProto = new MemoryStream(buffer, startIndex, length))
                {
                    GameManager gameMgr = GameManager.Instance;
                    switch (messageType)
                    {
                        case MessageType.ChangeDirection:
                            Debug.LogWarning("ChangeDirection");
                            packet = C2GChangeDir.Parser.ParseFrom(streamForProto);
                            break;
                        case MessageType.GameBegin:
                            Debug.LogWarning("GameBegin");
                            packet = G2CGameBegin.Parser.ParseFrom(streamForProto);
                            gameMgr.OnStartGame((G2CGameBegin)packet);
                            break;
                        case MessageType.FrameOperation:
                            Debug.LogWarning("FrameOperation");
                            packet = G2CFrameOperation.Parser.ParseFrom(streamForProto);
                            gameMgr.OnFrameOperation((G2CFrameOperation)packet);
                            break;
                        case MessageType.Move:
                            Debug.LogWarning("Move");
                            gameMgr.OnMove();
                            break;
                        case MessageType.Bomb:
                            Debug.LogWarning("Bomb");
                            gameMgr.OnBomb();
                            break;
                        default:
                            Log4U.LogInfo("No Such Packet, packet type is " + streamForProto);
                        break; 
                    }
                }
            }
            catch (System.Exception ex)
            {
                Log4U.LogError(ex.Message + "\n " + ex.StackTrace + "\n" + ex.Source);

            }return packet;
        }
    }
}
