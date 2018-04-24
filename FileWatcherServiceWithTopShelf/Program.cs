using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace FileWatcherServiceWithTopShelf
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(serviceConfig =>
            {
                serviceConfig.UseNLog();

                serviceConfig.Service<ConverterService>(serviceInstance => {
                    serviceInstance.ConstructUsing(() => new ConverterService());
                    serviceInstance.WhenStarted(execute => execute.Start());
                    serviceInstance.WhenStopped(execute => execute.Stop());

                    //using sc.exe to pass command 128-255
                    serviceInstance.WhenCustomCommandReceived((execute, hostControl, commandNumber) => execute.CustomCommand(commandNumber));
                });

                serviceConfig.EnableServiceRecovery(recoveryOption =>
                {
                    recoveryOption.RestartService(1);
                });

                serviceConfig.SetServiceName("FileWatcherService");
                serviceConfig.SetDisplayName("File Watcher Service");
                serviceConfig.SetDescription("Demo service use topshelf");

                serviceConfig.StartManually();
            });
        }
    }
}
