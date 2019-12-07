
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager2 : MonoBehaviour
{

    public GameObject pausePanel;
    public Transform cursor;
    public GameObject[] menuOptions;
    public GameObject optionPanel;
    public GameObject itemList;
    public GameObject itemListPrefab;
    public RectTransform content;
    public Text descriptionText;
    public Scrollbar scrollVertical;
    public Text healthText, manaText, strengthText, attackText, defenseText;
  
    public Text messageText;

    private bool pauseMenu = false;
    private int cursorIndex = 0;
    private Inventory inventory;
    public List<ItemList> items;
    private bool itemListActive = false;
    private Player_Controller player;
    private bool isMessageActive = false;
    private float textTimer;
    private bool axisInUse = false;

    // Use this for initialization
    void Start()
    {

        inventory = Inventory.inventory;
        player = FindObjectOfType<Player_Controller>();


    }

    // Update is called once per frame
    void Update()
    {

       /* if (isMessageActive)
        {
            Color color = messageText.color;
            color.a += 2f * Time.deltaTime;
            messageText.color = color;
            if (color.a >= 1)
            {
                isMessageActive = false;
                textTimer = 0;
            }
        }
        else if (!isMessageActive)
        {
            textTimer += Time.deltaTime;
            if (textTimer >= 2f)
            {
                Color color = messageText.color;
                color.a -= 2f * Time.deltaTime;
                messageText.color = color;
                if (color.a <= 0)
                {
                    messageText.text = "";
                }
            }
        }*/

        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu = !pauseMenu;
            cursorIndex = 0;
            itemListActive = false;
            descriptionText.text = "";
            itemList.SetActive(false);
            optionPanel.SetActive(true);
            UpdateAtributes();
            if (pauseMenu)
            {
                pausePanel.SetActive(true);
            }
            else
            {
                pausePanel.SetActive(false);
            }
        }

        if (pauseMenu)
        {
            Vector3 cursorposition = new Vector3();
            if (!itemListActive)
            {
                cursorposition = menuOptions[cursorIndex].transform.position;
                cursor.position = new Vector3(cursorposition.x - 100, cursorposition.y, cursorposition.z);
            }
            else if (itemListActive && items.Count > 0)
            {
                cursor.position = items[cursorIndex].transform.position;
                cursor.position = new Vector3(cursorposition.x - 75, cursorposition.y, cursorposition.z);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                cursorIndex++;
                if (itemListActive && items.Count > 0)
                {
                    scrollVertical.value -= (1f / (items.Count - 1));
                    UpdateDescription();
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                cursorIndex--;
                if (itemListActive && items.Count > 0)
                {
                    scrollVertical.value += (1f / (items.Count - 1));
                    UpdateDescription();
                }
            }
            if (Input.GetButtonDown("Submit") && !itemListActive)
            {
                optionPanel.SetActive(false);
                itemList.SetActive(true);
                RefreshItemList();
                UpdateItemsList(cursorIndex);
                cursorIndex = 0;
                if (items.Count > 0)
                    UpdateDescription();
                itemListActive = true;

            }
            else if (Input.GetKeyDown(KeyCode.U) && itemListActive) ;
            {
                if (items.Count > 0)
                {
                    UseItem();
                }
            }
        }

    }

    void UseItem()
    {
        if (items[cursorIndex].arma != null)
        {
            player.AddArma(items[cursorIndex].arma);
        }
        else if (items[cursorIndex].itemConsumivel != null)
        {
            player.UseIten(items[cursorIndex].itemConsumivel);
            inventory.RemoveItem(items[cursorIndex].itemConsumivel);
            cursorIndex = 0;
            RefreshItemList();
            UpdateItemsList(2);
            scrollVertical.value = 1;
        }
        else if (items[cursorIndex].armadura != null)
        {
            player.AddArmadura(items[cursorIndex].armadura);
        }
        UpdateAtributes();
        if (items.Count > 0)
        {
            UpdateDescription();
        }
        else
            descriptionText.text = "";

    }

    void UpdateDescription()
    {
        if (items[cursorIndex].arma != null)
            descriptionText.text = items[cursorIndex].arma.descrição;
        else if (items[cursorIndex].itemConsumivel != null)
            descriptionText.text = items[cursorIndex].itemConsumivel.descrição;
        else if (items[cursorIndex].armadura != null)
            descriptionText.text = items[cursorIndex].armadura.descricao;

    }

    void RefreshItemList()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Destroy(items[i].gameObject);
        }
        items.Clear();
    }

    void UpdateItemsList(int option)
    {
        if (option == 0)
        {
            for (int i = 0; i < inventory.weapons.Count; i++)
            {
                GameObject tempItem = Instantiate(itemListPrefab, content.transform);
                tempItem.GetComponent<ItemList>().setUpWeapon(inventory.weapons[i]);
                items.Add(tempItem.GetComponent<ItemList>());
            }
        }
        else if (option == 1)
        {
            for (int i = 0; i < inventory.armaduras.Count; i++)
            {
                GameObject tempItem = Instantiate(itemListPrefab, content.transform);
                tempItem.GetComponent<ItemList>().setUpArmor(inventory.armaduras[i]);
                items.Add(tempItem.GetComponent<ItemList>());
            }
        }
        else if (option == 2)
        {
            for (int i = 0; i < inventory.itens.Count; i++)
            {
                GameObject tempItem = Instantiate(itemListPrefab, content.transform);
                tempItem.GetComponent<ItemList>().setUpItem(inventory.itens[i]);
                items.Add(tempItem.GetComponent<ItemList>());
            }
        }
        
    }

    void UpdateAtributes()
    {
        healthText.text = "Vida: " + Player_Controller.vidaAtual + "/" + player.vidaMax;
        manaText.text = "Mana: " + Player_Controller.manaAtual + "/" + player.manaMax;
        strengthText.text = "Força: " + Player_Controller.forca;
        attackText.text = "Ataque: " + (Player_Controller.forca + player.GetComponentInChildren<Attack>().getDano());
        defenseText.text = "Defesa: " + player.defesa;
    }


}

