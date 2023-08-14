using Buildings.Workplace;
using Inventory;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;
using Object = UnityEngine.Object;
using Tree = Buildings.Tree;

namespace Tests
{
    public class WorkTest : TestBase
    {
        [UnityTest]
        public IEnumerator Woodcutter()
        {
            var originalMaterialCount = Object.FindObjectsOfType<Tree>().Length * Const.WoodDrop;
            const int materialCount = Const.BuildingStackSize + Const.EntityStackSize;

            yield return EntityWork<Woodcutter>(Const.Item.Wood, Const.Buildings.Tree);

            var currentMaterialCount = 0;
            foreach (var item in Object.FindObjectsOfType<Tree>().Select(x => x.GetComponent<Inventory.Inventory>()))
            {
                currentMaterialCount += item.GetItemCount(Const.Item.Wood);
            }
            Assert.AreEqual(originalMaterialCount - materialCount, currentMaterialCount);
        }

        [UnityTest]
        public IEnumerator Stonecutter() => EntityWork<Stonecutter>(Const.Item.Stone,
            Const.Buildings.Stone);
        
        [UnityTest]
        public IEnumerator Spawn()
        {
            var workplace = Object.FindObjectOfType<Spawn>();
            Assert.AreEqual(Const.Buildings.Spawn, workplace.WorkObject);
            
            entity.Workplace = workplace.gameObject;

            yield return new WaitForSeconds(10);
            Assert.IsTrue(entity.GetComponent<Collider>().bounds.Intersects(workplace.GetComponent<Collider>().bounds));
        }

        [UnityTest]
        public IEnumerator Gatherer() => EntityWork<Gatherer>(Const.Item.Berries,
            Const.Buildings.BerryBush);

        private IEnumerator EntityWork<T>(Const.Item item, Const.Buildings expectedWorkObject) where T : Workplace
        {
            var workplace = Object.FindObjectOfType<T>();
            Assert.AreEqual(expectedWorkObject, workplace.WorkObject);
            Assert.AreEqual(Const.BuildingMaxSlots, workplace.GetComponent<Inventory.Inventory>().slots);
            Assert.AreEqual(Const.BuildingStackSize, workplace.GetComponent<Inventory.Inventory>().stackSize);
            Assert.AreEqual(4, workplace.GetComponent<Workplace>().MaxWorkers);

            const int count = Const.BuildingStackSize + Const.EntityStackSize;
            entity.Workplace = workplace.gameObject;

            yield return new WaitUntil(() => GlobalInventory.GlobalInventoryDict[item] == count);
            Assert.AreEqual(count, GlobalInventory.GlobalInventoryDict[item]);
        }
    }
}
