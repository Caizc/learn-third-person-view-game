using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    void OnGUI()
    {
        int posX = 10;
        int posY = 10;
        int width = 100;
        int height = 30;
        int buffer = 10;

        List<string> itemList = Managers.Inventory.GetItemList();

        if (0 == itemList.Count)
        {
            GUI.Box(new Rect(posX, posY, width, height), "No Items");
        }

        foreach (string item in itemList)
        {
            int count = Managers.Inventory.GetItemCount(item);
            Texture2D image = Resources.Load<Texture2D>("Icons/" + item);

            GUI.Box(new Rect(posX, posY, width, height), new GUIContent("(" + count + ")", image));

            posX = posX + width + buffer;
        }

        string equipped = Managers.Inventory.equippedItem;
        if (null != equipped)
        {
            posX = Screen.width - (width + buffer);
            Texture2D image = Resources.Load<Texture2D>("Icons/" + equipped);
            GUI.Box(new Rect(posX, posY, width, height), new GUIContent("Equipped", image));

            if ("health" == equipped)
            {
                if (GUI.Button(new Rect(posX, posY + height + buffer, width, height), "Use Health"))
                {
                    Managers.Inventory.EquipItem("health");
                    Managers.Inventory.ConsumeItem("health");
                    Managers.Player.ChangeHealth(25);
                }
            }
        }

        posX = 10;
        posY = posY + height + buffer;

        foreach (string item in itemList)
        {
            if (GUI.Button(new Rect(posX, posY, width, height), "Equip " + item))
            {
                Managers.Inventory.EquipItem(item);
            }

            posX = posX + width + buffer;
        }
    }
}
