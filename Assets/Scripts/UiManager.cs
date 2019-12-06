using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private bool pausemenu=false;
    public Transform cursor;
    public GameObject pausePanel;
    public GameObject[] menuOptions;
    private int cursorIndex = 0;
    public GameObject optionPanel;
    public GameObject itemlist;
    private Inventory iventory;
    public GameObject itemListPrefab;
    public RectTransform content;
    // Start is called before the first frame update
    void Start()
    {
        iventory = Inventory.inventory;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            cursorIndex = 0;
            itemlist.SetActive(false);
            pausemenu = !pausemenu;
            if(pausemenu)
            {
                pausePanel.SetActive(true);
            }else
            {
                pausePanel.SetActive(false);
            }
        }

        if(pausemenu)
        {
            Vector3 cursosrPosition = menuOptions[cursorIndex].transform.position;
            cursor.position = new Vector3(cursosrPosition.x - 100, cursosrPosition.y, cursosrPosition.z);
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                cursorIndex++;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                cursorIndex--;
            }
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            optionPanel.SetActive(false);
            itemlist.SetActive(true);
            updateItemList(cursorIndex);
        }
        
    }
    void updateItemList(int option)
    {
        if(option==0)
        {
            for (int i = 0; i < iventory.weapons.Count;i++)
            {
                GameObject tempItem = Instantiate(itemListPrefab, content.transform);
                tempItem.GetComponent<ItemList>().setUpWeapon(iventory.weapons[i]);
            }
        }


    }

}
