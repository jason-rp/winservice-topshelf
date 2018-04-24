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
                serviceConfig.Service<ConverterService>(serviceInstance => {
                    serviceInstance.ConstructUsing(() => new ConverterService());
                    serviceInstance.WhenStarted(execute => execute.Start());
                    serviceInstance.WhenStopped(execute => execute.Stop());
                });

                serviceConfig.SetServiceName("FileWatcherService");
                serviceConfig.SetDisplayName("File Watcher Service");
                serviceConfig.SetDescription("Demo service use topshelf");

                serviceConfig.StartManually();
            });
        }
    }
}
