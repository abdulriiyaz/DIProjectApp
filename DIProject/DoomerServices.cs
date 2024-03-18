
//Di, Serilog, Settings

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace DIProject

{
    public class DoomerServices : IDoomerServices
    {
        private readonly ILogger<DoomerServices> _log;
        private readonly IConfiguration _config;

        public DoomerServices(ILogger<DoomerServices> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }
        public void Run()
        {
            for (var i = 1; i <= _config.GetValue<int>("LoopTimes"); i++)
            {
                _log.LogInformation("We are running {num}", i);

            }
            Console.ReadKey();
        }
    }
}
