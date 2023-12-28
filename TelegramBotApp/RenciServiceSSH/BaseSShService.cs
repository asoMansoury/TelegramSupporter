using Renci.SshNet;
using System;

namespace TelegramBotApp.RenciServiceSSH
{
    public abstract class BaseSShService
    {
        private readonly SshClient _sshClient;
        private string hostname, username, password = string.Empty;
        private int port;
        public BaseSShService( string hostname, int port, string username, string password)
        {
            this.hostname = hostname;
            this.username = username;
            this.password = password;
            this.port = port;
        }

        public void ExecuteRemoteCommand(List<string> commands)
        {
            try
            {
                Console.WriteLine("trying to connect to ubuntu");
                using (var sshClient = new SshClient(hostname,port, username, password))
                {
                    sshClient.Connect();
                    if (sshClient.IsConnected)
                    {
                        foreach (var command in commands)
                        {
                            Console.WriteLine("exception when trying to run command::=> ", command);
                            ExecuteCommand(sshClient, command);
                        }
                    }
                    sshClient.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception when trying to connect server", ex.Message);
            }

        }

        private void ExecuteCommand(SshClient client, string command)
        {
            using (var sshCommand = client.CreateCommand(command))
            {
                var result = sshCommand.Execute();

            }
        }
    }
}
