using MessageCore.Models;
using MessageRequest;
using MessengerClient.Constant;
using MessengerClient.DbModels;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MessengerClient.Models
{
    class ServerConnectionHandler
    {
        public User ThisUser { get; set; }
        public TcpClient TcpClient { get; private set; }

        public static ConcurrentBag<string> RequestsToSend { get; }
        private UserHandler userHandler = UserHandler.GetInstance();
        private MessageHandler messageHandler = MessageHandler.GetInstance();

        static ServerConnectionHandler()
        {
            RequestsToSend = new ConcurrentBag<string>();
        }

        public async void Start()
        {
            await Task.Run(() =>
            {
                try
                {
                    TcpClient = new TcpClient(Constants.IP, Constants.PORT);
                    NetworkStream ns = TcpClient.GetStream();
                    string buff;

                    while (true)
                    {
                        while (ns.DataAvailable)
                        {
                            string receivedPackage = Package.Read(ns);

                            RequestType type = RequestConverter.GetRequestType(receivedPackage);
                            string data = RequestConverter.GetData(receivedPackage);
                            switch (type)
                            {
                                case RequestType.LoginResponse:
                                    userHandler.Found = RequestConverter.DecomposeLoginResponse(data);
                                    break;
                                case RequestType.RegistrationResponse:
                                    break;
                                case RequestType.SendMessage:
                                    Message message = RequestConverter.DecomposeMessage(data);
                                    messageHandler.SaveAndShowMessage(new LocalMessage(message));
                                    Package.Write(ns, RequestConverter.ComposeMessageReceived(message.Id));
                                    break;
                                case RequestType.CheckUserExistResponse:
                                    userHandler.ReceiverFound = RequestConverter.DecomposeUserExistResponse(data);
                                    break;
                                case RequestType.MessageSyncInfo:
                                    messageHandler.SyncMessage(RequestConverter.DecomposeMessageSyncInfo(data));
                                    break;
                                default:
                                    break;
                            }
                        }

                        while (!RequestsToSend.IsEmpty)
                        {
                            RequestsToSend.TryTake(out buff);
                            Package.Write(ns, buff);
                        }

                        Thread.Sleep(5);
                    }
                }
                catch (IOException ioEx)
                {
                    Console.WriteLine(ioEx.Message);
                }
                finally
                {
                    TcpClient?.GetStream().Close();
                    TcpClient?.Close();
                }
            });
        }
    }
}
