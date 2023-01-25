using Pos.PeripheralManager.Config;
using System.Diagnostics;

namespace Pos.PeripheralManager
{
    public class CashDrawerService : ICashDrawerService
    {
        private IManagerConfig _managerConfig;
        //private int? _procId;
        private Dictionary<int, Process> _processes { get; set; }

        public CashDrawerService(IManagerConfig managerConfig)
        {
            _processes = new Dictionary<int, Process>();
            _managerConfig = managerConfig;
        }

        public int StartCashDrawer()
        {
            var cdProcess = new Process();
            var path = $"C:\\git\\Temp\\Mcd.Pos.Devices.PeripheralManager\\Mcd.Pos.Devices.PeripheralManager\\bin\\x86\\Debug\\CashDrawer\\McD.Pos.Devices.CashDrawer.exe";
            cdProcess.StartInfo.FileName = path;
            cdProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);
            cdProcess.StartInfo.CreateNoWindow = true;
            //_cdProcess.StartInfo.Arguments = $" -c {_managerConfig.ConfigurationLocation}/{_managerConfig.ConfigurationName}";

            bool started = false;

            started = cdProcess.Start();
            _processes.Add(cdProcess.Id, cdProcess);

            return cdProcess.Id;

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
