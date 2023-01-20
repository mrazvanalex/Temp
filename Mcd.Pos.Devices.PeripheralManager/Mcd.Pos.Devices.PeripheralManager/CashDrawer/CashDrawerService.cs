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



            System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo();
            procInfo.FileName = ("C:\\git\\Temp\\Mcd.Pos.Devices.PeripheralManager\\Mcd.Pos.Devices.PeripheralManager\\bin\\x86\\Debug\\CashDrawer\\McD.Pos.Devices.CashDrawer.exe");
            System.Diagnostics.Process.Start(procInfo);

            //System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo();
            //procInfo.FileName = ("mspaint.exe");
            //System.Diagnostics.Process.Start(procInfo);

            try
            {
                bool started = false;
                Process.Start("C:\\git\\Temp\\Mcd.Pos.Devices.PeripheralManager\\Mcd.Pos.Devices.PeripheralManager\\bin\\x86\\Debug\\CashDrawer\\McD.Pos.Devices.CashDrawer.exe");
                started = cdProcess.Start();
                _processes.Add(cdProcess.Id, cdProcess);    

                return cdProcess.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("Err occured in PManager", ex);
                throw new Exception(path, ex);
            }


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
