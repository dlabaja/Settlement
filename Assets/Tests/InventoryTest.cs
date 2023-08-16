using Inventory;
using NUnit.Framework;
using System;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Tests
{
    public class InventoryTest : TestBase
    {
        [Test]
        public void TestSerializedFields()
        {
            try
            {
                TextAsset yamlFile = Resources.Load<TextAsset>("Entity");
                if (yamlFile != null)
                {
                    var yaml = yamlFile.text;
                    var deserializer = new DeserializerBuilder()
                        .WithNamingConvention(UnderscoredNamingConvention.Instance)
                        .Build();

                    var data = deserializer.Deserialize<YamlData>(yaml);
                    int rootOrder = data.Transform.RootOrder;
                    Debug.Log("m_RootOrder: " + rootOrder);
                }
                else
                {
                    Debug.LogError("YAML file not found.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
}
