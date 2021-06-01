using System;
using System.Net;
using System.Net.Sockets;

public static class InternetTime
{
    private const string ntpServer = "time.windows.com";
    private const int ntpServerPort = 123;

    public static void GetTime(Action<bool, DateTime> callback)
    {
        byte[] ntpData = new byte[48];

        /*LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)*/
        ntpData[0] = 0b00011011;

        IPHostEntry hostEntry = Dns.GetHostEntry(ntpServer);

        IPEndPoint ipEndPoint = new IPEndPoint(hostEntry.AddressList[0], ntpServerPort);

        try
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.Connect(ipEndPoint);

                socket.ReceiveTimeout = 3000;

                socket.Send(ntpData);
                socket.Receive(ntpData);
                socket.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            callback?.Invoke(false, DateTime.Now);
            return;
        }

        const byte serverReplyTime = 40;

        ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);
        ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

        intPart = SwapEndianness(intPart);
        fractPart = SwapEndianness(fractPart);

        ulong milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

        DateTime internetDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

        callback?.Invoke(true, internetDateTime);
    }

    private static uint SwapEndianness(ulong x)
    {
        return (uint)(((x & 0x000000ff) << 24) +
                       ((x & 0x0000ff00) << 8) +
                       ((x & 0x00ff0000) >> 8) +
                       ((x & 0xff000000) >> 24));
    }
}
