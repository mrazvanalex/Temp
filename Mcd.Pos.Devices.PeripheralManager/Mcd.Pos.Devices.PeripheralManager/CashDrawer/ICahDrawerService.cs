using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcd.Pos.Devices.PeripheralManager.CashDrawer
{
    public interface ICashDrawerService
    {
        int StartCashDrawer();
        void StopCashDrawer(int id);
    }
}
