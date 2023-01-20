using Mcd.Pos.Devices.PeripheralManager.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.Pos.Devices.PeripheralManager.CashDrawer
{
    public class CashDrawerService : ICashDrawerService
    {
        private IManagerConfig _managerConfig;
        private ILogger<CashDrawerService> _logger;
        //private int? _procId;
        private Dictionary<int, Process> _processes { get; set; }

        public CashDrawerService(IManagerConfig managerConfig, ILogger<CashDrawerService> logger)
        {
            _processes = new Dictionary<int, Process>();
            _managerConfig = managerConfig;
            _logger = logger;
        }

        public int StartCashDrawer()
        {
            var cdProcess = new Process();
            var path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}";
            cdProcess.StartInfo.FileName = $"{Directory.GetParent(path).FullName}\\{_managerConfig.CashDrawerLocation}";
            //_cdProcess.StartInfo.Arguments = $" -c {_managerConfig.ConfigurationLocation}/{_managerConfig.ConfigurationName}";
            try
            {
                bool started = false;

                started = cdProcess.Start();
                _processes.Add(cdProcess.Id, cdProcess);

                return cdProcess.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(path, ex);
                _logger.LogError("Err occured in PManager", ex);
            }

             return 0;


            //try
            //{
            //    _procId = _cdProcess.Id;
            //    Console.WriteLine("ID: " + _procId);
            //}
            //catch (InvalidOperationException)
            //{
            //    started = false;
            //}
            //catch (Exception ex)
            //{
            //    started = false;
            //}
        }

        public void StopCashDrawer(int id)
        {
            //if (_procId != null)
            //{
            //    var process = Process.GetProcessById(_procId.Value);
            //    process.Kill();
            //}

            _processes[id].Kill();
            _processes.Remove(id);
        }
    }
}
