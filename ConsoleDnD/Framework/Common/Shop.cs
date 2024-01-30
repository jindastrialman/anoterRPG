using ConsoleDnD.Framework.Interfaces;

namespace ConsoleDnD.Framework.Common
{
    class Shop : IShop
    {
        public IEnumerable<TradeDeal> Deals { get; }

        public virtual bool TryDoTheTrade(TradeDeal SelectedDeal)
        {
            return true;
        }
    }
}
