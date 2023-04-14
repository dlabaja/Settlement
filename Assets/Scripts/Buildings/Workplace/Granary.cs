using System.Collections.Generic;

namespace Buildings.Workplace
{
    public class Granary : Warehouse
    {
        private void Start()
        {
            storeableItems = new List<Const.Item>{
                Const.Item.None, Const.Item.Berries, Const.Item.Wheat,
                Const.Item.Flour, Const.Item.Bread, Const.Item.Fish
            };
        }
    }
}
