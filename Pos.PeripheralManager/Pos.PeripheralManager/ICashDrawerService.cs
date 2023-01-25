namespace Pos.PeripheralManager
{
    public interface ICashDrawerService
    {
        int StartCashDrawer();
        void StopCashDrawer(int id);
    }
}
