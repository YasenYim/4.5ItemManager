using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_5道具管理器
{
    // 道具
    class Item
    {
        public int autoID;      // 自动ID
        public int itemID;      // 设计ID
        public string name;     // 道具名称
        public int count;       // 叠加数量
        public int endurance;   // 耐久度
        public Item(int itemID, string name,int count=1,int endurance=0)
        {
            this.itemID = itemID;
            this.name = name;
            this.count = count;
            this.endurance = endurance;
        }
        public override string ToString()
        {
            return $"{{ {autoID} : {itemID}-{name} 数量：{count} 耐久:{endurance} }}";  // {{代表< ,}}代表>,转译字符
        }

    }
    //  背包，道具管理器
    class ItemManager
    {
        Dictionary<int, Item> dict = new Dictionary<int, Item>();
        Dictionary<string, List<Item>> dictName = new Dictionary<string, List<Item>>();
        public int counter = 1;
        //  添加道具
        public Item AddItem(Item item)
        {
            if(item.autoID !=0)
            {
                Console.WriteLine("道具autoID不为0，不能添加到背包中");
                return null;
            }
            item.autoID = counter;
            counter += 1;

            dict.Add(item.autoID, item);
            //加入第二个字典名字
            //if (!dictName.ContainsKey(item.name))
            //{
            //    dictName.Add(item.name, new List<Item>());
            //}
            //dictName[item.name].Add(item);


            if (dictName.ContainsKey(item.name))
            {
                dictName[item.name].Add(item);
            }
            else
            {
                List<Item> newList = new List<Item>();
                newList.Add(item);
                dictName.Add(item.name, newList);
            }
            return item;
        }

        // 删除道具
        public bool RemoveItem(int id)
        {
            if(!dict.ContainsKey(id))
            {
                Console.WriteLine($"没有找到道具：{id}");
                return false;
            }

            Item item = dict[id];

            dict.Remove(id);
            // 从第二个字典中删除

            //列表Remove写法

            //dictName[item.name].Remove(item);

            //列表RemoveAt写法
            List<Item> t = dictName[item.name];
            for(int i = 0;i<t.Count;i++)
            {
                if(t[i].autoID == id)
                {
                    t.RemoveAt(i);
                }
            }
           

            return true;
        }
        // 查找道具
        public Item FindItem(int id)
        {
            if (!dict.ContainsKey(id))
            {
                Console.WriteLine($"未找到道具：{id}");
                return null;
            }
            return dict[id];
        }

        // 根据名字查找道具
        public List<Item> FindItemByName(string name)
        {
            if (!dictName.ContainsKey(name))
            {
                Console.WriteLine($"未找到道具：{name}");
                return null;
            }
            return dictName[name];

            //List<Item> list = new List<Item>();
            //foreach(var pair in dict)
            //{
            //    if(pair.Value.name == name)
            //    {
            //        list.Add(pair.Value);
            //    }
            //}
            //return list;

            //foreach(var pair in dict)
            //{
            //    if(pair.Value.name == name)
            //    {
            //        return pair.Value;
            //    }
            //}
            //return null;


        }
        // 打印背包内容
        public void PrintItem()
        {
            Console.WriteLine("-------------------");

            foreach(var pair in dict)
            {
                Console.WriteLine("Key:"+pair.Key + "Value:" + pair.Value);
            }

            Console.WriteLine("----------dictName-----------");

            foreach (var pair in dictName)
            {
                Console.WriteLine(pair.Key + " :");
                foreach(var item in pair.Value)
                {
                    Console.WriteLine($"     {item}");
                }
            }

            Console.WriteLine("-------------------");
        }

        public void PrintList(List<Item> list)
        {
            for(int i = 0;i<list.Count;i++)
            {
                { Console.WriteLine(list[i] + " "); }
                //Console.WriteLine();
            }
        }
    }

    class Program
    {
        static ItemManager itemManager; // 背包
        static void Main(string[] args)
        {
            itemManager = new ItemManager();
            Item item1 = new Item(1001, "红药水", 10);
            Item item2 = new Item(1002, "红药水", 5);
            Item item3 = new Item(1003, "铜剑", 1, 100);
            Item item4 = new Item(1004, "草鞋", 1, 90);
            Item item5 = new Item(1004, "草鞋", 1, 80);
            itemManager.AddItem(item1);
            itemManager.AddItem(item2);
            itemManager.AddItem(item3);
            itemManager.AddItem(item4);
            itemManager.AddItem(item5);

            // 打印背包内容 
            itemManager.PrintItem();
            itemManager.RemoveItem(4);
            itemManager.PrintItem();
            List<Item> list = itemManager.FindItemByName("红药水");
            itemManager.PrintList(list);
            Console.ReadKey();
        }
    }
}
