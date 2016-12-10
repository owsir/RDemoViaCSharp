using System;
using Renci.SshNet;
using fourworkx.Common;

namespace fourworkx.SSH
{
    public class SSHHelper
    {

        private static SshClient _ssh;
        private static ForwardedPortLocal _forwardedPortLocal;
        public static void Start()
        {
            ConnectionInfo connection = new PrivateKeyConnectionInfo(ConfigCollection.LinuxRServerIP.ToString(), 22,ConfigCollection.SSHServerName, new PrivateKeyFile(ConfigCollection.SSHKeyFile, string.Empty));
            connection.Timeout = TimeSpan.FromMinutes(2);
            _ssh = new SshClient(connection);
            _ssh.Connect();

            _forwardedPortLocal = new ForwardedPortLocal("127.0.0.1", (uint)ConfigCollection.SSHLocalForwardPort, "127.0.0.1", (uint)ConfigCollection.LinuxRServerPort);

            _ssh.AddForwardedPort(_forwardedPortLocal);

            _forwardedPortLocal.Start();
        }

        public static void Stop()
        {
            if (_forwardedPortLocal.IsStarted)
            {
                _forwardedPortLocal.Stop();
            }

            if (_ssh.IsConnected)
            {
                _ssh.Disconnect();
            }
        }
    }
}
