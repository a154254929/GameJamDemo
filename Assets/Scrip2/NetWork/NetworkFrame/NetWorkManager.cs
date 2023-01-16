using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.IO;
using Protocol;
using Google.Protobuf;
using ProtoBuf;

namespace NetWorkFrame
{

    public class NetworkMessageParam
    {
        public IMessage req;
        public IMessage rsp;
        public string msgID;
    }

    public class NetworkManager : Singleton<NetworkManager>
    {
        private Socket _socket;

        private byte[] _receiveBuffer;
        private int readedLen = 0;
        private MessageType type = 0;
        private int messageLen = 0;

        private const int HEAD_SIZE = 1;
        private const int HEAD_NUM = 2;

        private float CONNECT_TIME_OUT = 1.0f;
        private float REQ_TIME_OUT = 5.0f;
        private float KEEP_ALIVE_TIME_OUT = 10.0f;

        private byte[] _sendBuffer = new byte[512];

        //private bool _isKeepAlive = false;


        private HashSet<MessageType> _forcePushMessageType;
        private HashSet<MessageType> _needReqMessageType;

        public bool IsConncted
        {
            get { return _socket != null && _socket.Connected; }
        }

        #region LifeCycle
        public override void Init()
        {
            Log4U.LogInfo("Client Running...");

            InitForcePushMessageType();
            InitNeedReqMessageType();
            MessageDispatcher.GetInstance().RegisterMessageHandler((uint)EModelMessage.SOCKET_CONNECTED, OnSocketConnected);


        }

        public override void Release()
        {

            CloseConnection();
        }



        public void OnSocketConnected(uint iMessageType, object kParam)
        {
            if (_receiveBuffer == null)
            {
                _receiveBuffer = new byte[_socket.ReceiveBufferSize];
            }

            //_isKeepAlive = true;

            BeginReceivePacket();
        }

        public void StartGame()
        {

            StartCoroutine(BeginTryConnect());
        }

        #endregion

        #region Connection

        private IEnumerator BeginConnection()
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                _socket.BeginConnect(IPConfig.IPAddress, IPConfig.IPPort, new AsyncCallback(FinishConnection), null);
            }
            catch (Exception ex)
            {
                Log4U.LogError(ex.StackTrace);
                yield break;
            };

            yield return new WaitForSeconds(CONNECT_TIME_OUT);

            if (_socket.Connected == false)
            {
                Log4U.LogInfo("Client Connect Time Out...");
                //CloseConnection();
            }
            else Log4U.LogError("Client Connect Success");


            //_isKeepAlive = _socket.Connected;
        }

        private void FinishConnection(IAsyncResult ar)
        {
            _socket.EndConnect(ar);

            if (_socket.Connected)
            {
                MessageDispatcher.GetInstance().DispatchMessageAsync((uint)EModelMessage.SOCKET_CONNECTED, null);
            }
        }

        private void CloseConnection()
        {
            if (_socket != null)
            {
                if (_socket.Connected)
                {
                    _socket.Shutdown(SocketShutdown.Both);
                    Log4U.LogInfo("Client Close...");
                }
                _socket.Close();
            }
        }

        /// <summary>
        /// 当无法接收到心跳包的时候尝试重新连接服务器
        /// </summary>
        /// <returns></returns>
        private IEnumerator BeginTryConnect()
        {
            yield return null;
            while (_socket == null || !_socket.Connected)
            {
                Log4U.LogInfo("Begin Connect...");
                CloseConnection();
                yield return StartCoroutine(BeginConnection());
            }

            //while (_isKeepAlive)
            //{
            //    _isKeepAlive = false;
            //    yield return new WaitForSeconds(KEEP_ALIVE_TIME_OUT);
            //}

            MessageDispatcher.GetInstance().DispatchMessageAsync((uint)EModelMessage.SOCKET_DISCONNECTED, null);
        }

        //public void OnKeepAliveSync(uint iMessageType, object kParam)
        //{
        //    _isKeepAlive = true;
        //}

        #endregion

        #region ReceivePacket

        private void BeginReceivePacket()
        {
            try
            {
                lock (_socket)
                {
                    if (type == 0 || messageLen == 0)
                    {
                        _socket.BeginReceive(_receiveBuffer, readedLen, 2 - readedLen, SocketFlags.None, new AsyncCallback(EndReceivePacket), null);
                    }
                    else
                    {
                        _socket.BeginReceive(_receiveBuffer, readedLen, messageLen - readedLen, SocketFlags.None, new AsyncCallback(EndReceivePacket), null);
                    }
                }
            }
            catch (Exception ex)
            {
                Log4U.LogError(ex.Message);
            }

        }

        private void EndReceivePacket(IAsyncResult ar)
        {
            int bytesRead = -1;
            try
            {
                if (IsConncted)
                {
                    lock (_socket)
                    {
                        bytesRead = _socket.EndReceive(ar);
                    }
                }

                if (bytesRead == -1)
                {
                    CloseConnection();
                    return;
                }
            }
            catch (ObjectDisposedException)
            {
                Log4U.LogInfo("Receive Closed");
            }
            catch (Exception ex)
            {
                Log4U.LogError(ex.Message + "\n " + ex.StackTrace + "\n" + ex.Source);
            }
            readedLen += bytesRead;
            if (type == 0 || messageLen == 0)
            {
                if (readedLen >= 2)
                {
                    type = (MessageType)MiniConverter.BytesToInt8(_receiveBuffer, HEAD_SIZE * 0);
                    messageLen = (int)MiniConverter.BytesToInt8(_receiveBuffer,  HEAD_SIZE * 1);
                    if (messageLen == 0)
                    {
                        // TODO 此时有一个type的消息过来、没有内容
                        IMessage rspPacket = UnPackTool.UnPack(type, 0, messageLen, _receiveBuffer);
                        type = 0;
                    }
                    readedLen = 0;
                }
            }
            else
            {
                if (readedLen >= messageLen)
                {
                    // TODO _receiveBuffer 里前messageLen是协议结构体解析后处理

                    IMessage rspPacket = UnPackTool.UnPack(type, 0, messageLen, _receiveBuffer);
                    if (rspPacket != null)
                    {
                        MessageArgs args = new MessageArgs
                        {
                            iMessageType = (uint)type,
                            kParam = rspPacket,
                        };

                        NetworkMessageParam networkParam = new NetworkMessageParam
                        {
                            rsp = rspPacket,
                        };
                    }
                }
                messageLen = 0;
                type = 0;
                readedLen = 0;
            }



            Array.Clear(_receiveBuffer, 0, _socket.ReceiveBufferSize);

            BeginReceivePacket();
        }

        /// <summary>
        /// 配置需要回复服务器的消息类型
        /// </summary>
        private void InitForcePushMessageType()
        {
            _forcePushMessageType = new HashSet<MessageType>
            {

            };
        }

        /// <summary>
        /// 配置在Rsp包中同时需要Req包信息的消息类型
        /// </summary>
        private void InitNeedReqMessageType()
        {
            _needReqMessageType = new HashSet<MessageType>
            {

            };
        }

        #endregion


        #region SendPacket

        public string SendPacket<T>(MessageType messageType, T packet, uint timeoutMessage = (uint)EModelMessage.REQ_TIMEOUT) where T : IMessage
        {
            byte[] msgIDBytes = BitConverter.GetBytes(UnityEngine.Random.value);

            if (timeoutMessage == (uint)EModelMessage.REQ_TIMEOUT)
            {
                // DialogManager.GetInstance().ShowLoadingDialog();
            }

            StartCoroutine(BeginSendPacket<T>(messageType, packet, timeoutMessage, msgIDBytes));

            return BitConverter.ToString(msgIDBytes);
        }

        public string SendPacket(MessageType messageType, uint timeoutMessage = (uint)EModelMessage.REQ_TIMEOUT)
        {
            byte[] msgIDBytes = BitConverter.GetBytes(UnityEngine.Random.value);

            if (timeoutMessage == (uint)EModelMessage.REQ_TIMEOUT)
            {
                // DialogManager.GetInstance().ShowLoadingDialog();
            }

            StartCoroutine(BeginSendPacket(messageType, timeoutMessage, msgIDBytes));

            return BitConverter.ToString(msgIDBytes);
        }

        private IEnumerator BeginSendPacket<T>(MessageType messageType, T packet, uint timeoutMessage, byte[] msgIDBytes) where T : IMessage
        {
            string msgID = BitConverter.ToString(msgIDBytes);

            Log4U.LogInfo("Send : " + messageType + " msgID : " + msgID);

            DoBeginSendPacket<T>(messageType, packet);
            yield return new WaitForSeconds(REQ_TIME_OUT);

        }

        private IEnumerator BeginSendPacket(MessageType messageType, uint timeoutMessage, byte[] msgIDBytes)
        {
            string msgID = BitConverter.ToString(msgIDBytes);

            Log4U.LogInfo("Send : " + messageType + " msgID : " + msgID);

            DoBeginSendPacket(messageType);
            yield return new WaitForSeconds(REQ_TIME_OUT);
        }

        /// <summary>
        /// 协议格式：
        /// SIZE ： 4 | TYPE ： 4 | MsgID : 4 | PACKET ： dynamic
        /// </summary>
        /// <typeparam name="T">向服务器发送的packet的类型</typeparam>
        /// <param name="networkMessage">向服务器发送的请求的类型</param>
        /// <param name="packet">向服务器发送的packet</param>
        /// 
        private void DoBeginSendPacket<T>(MessageType messageType, T packet) where T : IMessage
        {
            try
            {
                MemoryStream streamForProto = new MemoryStream();
                Serializer.Serialize<T>(streamForProto, packet);
                char bodylen = (char)streamForProto.Length;
                byte[] bufferSizeBytes = MiniConverter.Int8ToBytes(bodylen);

                byte[] messageTypeBytes = MiniConverter.IntToBytes((int)messageType);

                // int有四个字节
                Array.Copy(messageTypeBytes, 3, _sendBuffer, HEAD_SIZE * 0, HEAD_SIZE);

                Array.Copy(bufferSizeBytes, 0, _sendBuffer, HEAD_SIZE * 1, HEAD_SIZE);
                Array.Copy(streamForProto.ToArray(), 0, _sendBuffer, HEAD_SIZE * HEAD_NUM, streamForProto.Length);
                lock (_socket)
                {
                    if (_socket != null && _socket.Connected)
                    {
                        _socket.BeginSend(_sendBuffer, 0, (int)streamForProto.Length + HEAD_SIZE * HEAD_NUM, SocketFlags.None, new AsyncCallback(EndSendPacket), null);
                    }
                }
                streamForProto.Dispose();

            }
            catch (ObjectDisposedException)
            {
                Log4U.LogInfo("Send Closed");
            }
            catch (Exception ex)
            {
                Log4U.LogError(ex.Message);
            }
        }
        private void DoBeginSendPacket(MessageType messageType)
        {
            try
            {
                MemoryStream streamForProto = new MemoryStream();
                char bodylen = (char)0;
                byte[] bufferSizeBytes = MiniConverter.Int8ToBytes(bodylen);
                byte[] messageTypeBytes = MiniConverter.IntToBytes((int)messageType);

                // int有四个字节
                Array.Copy(messageTypeBytes, 3, _sendBuffer, HEAD_SIZE * 0, HEAD_SIZE);
                Array.Copy(bufferSizeBytes, 0, _sendBuffer, HEAD_SIZE * 1, HEAD_SIZE);
                lock (_socket)
                {
                    if (_socket != null && _socket.Connected)
                    {
                        _socket.BeginSend(_sendBuffer, 0, HEAD_SIZE * HEAD_NUM, SocketFlags.None, new AsyncCallback(EndSendPacket), null);
                    }
                }
                streamForProto.Dispose();

            }
            catch (ObjectDisposedException)
            {
                Log4U.LogInfo("Send Closed");
            }
            catch (Exception ex)
            {
                Log4U.LogError(ex.Message);
            }
        }

        private void DoBeginSendPacket(MessageType networkMessage, byte[] msgID)
        {
            try
            {
                byte[] sendBuffer = new byte[HEAD_SIZE * HEAD_NUM];

                byte[] bufferSizeBytes = MiniConverter.IntToBytes(HEAD_SIZE * HEAD_NUM);
                byte[] networkMessageBytes = MiniConverter.IntToBytes((int)networkMessage);

                Array.Copy(bufferSizeBytes, 0, sendBuffer, HEAD_SIZE * 0, HEAD_SIZE);
                Array.Copy(networkMessageBytes, 0, sendBuffer, HEAD_SIZE * 1, HEAD_SIZE);
                Array.Copy(msgID, 0, sendBuffer, HEAD_SIZE * 2, HEAD_SIZE);

                lock (_socket)
                {
                    _socket.BeginSend(sendBuffer, 0, HEAD_SIZE * HEAD_NUM, SocketFlags.None, new AsyncCallback(EndSendPacket), null);
                }
            }
            catch (Exception ex)
            {
                Log4U.LogError(ex.Message);
            }
        }

        private void EndSendPacket(IAsyncResult ar)
        {
            //int bytesSend = 0;
            try
            {
                lock (_socket)
                {
                    _socket.EndSend(ar);
                }
            }
            catch (Exception ex)
            {
                Log4U.LogError(ex.Message);
            }
        }
        #endregion

        #region Misc

        private void RemoveMsgID(string msgID)
        {

            MessageDispatcher.GetInstance().DispatchMessageAsync((uint)EModelMessage.REQ_FINISH, null);

        }

        public void OnReqFinish(uint iMessageType, object kParam)
        {
            //DialogManager.GetInstance().HideLoadingDialog();
        }

        #endregion
    }
}


