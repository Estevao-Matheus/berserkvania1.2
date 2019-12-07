using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    private bool pausemenu = false;
    public Transform cursor;
    public GameObject pausePanel;
    public GameObject[] menuOptions;
    private int cursorIndex = 0;
    public GameObject optionPanel;
    public GameObject itemlist;
    private Inventory iventory;
    public GameObject itemListPrefab;
    public RectTransform content;
    public List<ItemList> itens;
    private bool itemListActive = false;
    public Text descricaotexto;
    public Scrollbar scrollVertical;
    public Text manaTexto, vidaTexto, forcaTexto, defesaTexto, ataqueTexto;
    private Player_Controller player;
    // Start is called before the first frame update



    void Start()
    {
        iventory = Inventory.inventory;
        player = FindObjectOfType<Player_Controller>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            pausemenu = !pausemenu;
            cursorIndex = 0;
            itemListActive = false;
            descricaotexto.text = "";
            itemlist.SetActive(false);
            optionPanel.SetActive(true);
            UpdateAtributes();
            RefreshItemList();

            if (pausemenu)
            {
                pausePanel.SetActive(true);
            }
            else
            {
                pausePanel.SetActive(false);
            }
        }

        if (pausemenu)
        {
            Vector3 cursorposition = new Vector3();
            if (!itemListActive)
            {
                cursorposition = menuOptions[cursorIndex].transform.position;
                cursor.position = new Vector3(cursorposition.x - 100, cursorposition.y, cursorposition.z);
            }
            else if (itemListActive && itens.Count > 0)
            {
                cursor.position = itens[cursorIndex].transform.position;
                cursor.position = new Vector3(cursorposition.x - 75, cursorposition.y, cursorposition.z);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (!itemListActive && cursorIndex >= menuOptions.Length - 1)
                {
                    cursorIndex = menuOptions.Length - 1;
                }
                else if (itemListActive && cursorIndex >= itens.Count - 1)
                {
                    if (itens.Count == 0)
                    {
                        cursorIndex = 0;
                    }
                    else
                    {
                        cursorIndex = itens.Count - 1;

                    }
                }
                else
                    cursorIndex++;
                if (itemListActive && itens.Count > 0)
                {
                    scrollVertical.value -= (1f / (itens.Count - 1));
                    updateDescription();
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (cursorIndex == 0)
                {
                    cursorIndex = 0;
                }
                else
                    cursorIndex--;
                if (itemListActive && itens.Count > 0)
                {
                    scrollVertical.value += (1f / (itens.Count - 1));
                    updateDescription();
                }
            }
            if (Input.GetButtonDown("Submit") && !itemListActive)
            {
                optionPanel.SetActive(false);
                itemlist.SetActive(true);
                RefreshItemList();
                updateItemList(cursorIndex);
                cursorIndex = 0;
                if (itens.Count > 0)
                    updateDescription();
                itemListActive = true;

            }
            else if (Input.GetKeyDown(KeyCode.U) && itemListActive) ;
            {
                if (itens.Count > 0)
                {
                    UseItem();
                }
            }


        }


    }

    void UseItem()
    {
        if (itens[cursorIndex].arma != null)
        {
            player.AddArma(itens[cursorIndex].arma);
        }
        else if (itens[cursorIndex].itemConsumivel != null)
        {
            player.UseIten(itens[cursorIndex].itemConsumivel);
            iventory.RemoveItem(itens[cursorIndex].itemConsumivel);
            cursorIndex = 0;
            RefreshItemList();
            updateItemList(2);
            scrollVertical.value = 1;
        }
        else if (itens[cursorIndex].armadura != null)
        {
            player.AddArmadura(itens[cursorIndex].armadura);
        }
        UpdateAtributes();
        if (itens.Count > 0)
        {
            updateDescription();
        }
        else
            descricaotexto.text = "";
    }
    void updateDescription()
    {
        if (itens[cursorIndex].arma != null)
            descricaotexto.text = itens[cursorIndex].arma.descrição;
        if (itens[cursorIndex].itemConsumivel != null)
            descricaotexto.text = itens[cursorIndex].itemConsumivel.descrição;
        if (itens[cursorIndex].armadura != null)
            descricaotexto.text = itens[cursorIndex].armadura.descricao;
    }

    void RefreshItemList()
    {
        for (int i = 0; i < itens.Count; i++)
        {
            Destroy(itens[i].gameObject);
        }
        itens.Clear();
    }

    void updateItemList(int option)
    {
        if (option == 0)
        {
            for (int i = 0; i < iventory.weapons.Count; i++)
            {
                GameObject tempItem = Instantiate(itemListPrefab, content.transform);
                tempItem.GetComponent<ItemList>().setUpWeapon(iventory.weapons[i]);
                itens.Add(tempItem.GetComponent<ItemList>());
            }
        }
        if (option == 1)
        {
            for (int h = 0; h < iventory.armaduras.Count; h++)
            {
                GameObject tempItem = Instantiate(itemListPrefab, content.transform);
                tempItem.GetComponent<ItemList>().setUpArmor(iventory.armaduras[h]);
                itens.Add(tempItem.GetComponent<ItemList>());
            }
        }
        if (option == 2)
        {
            for (int j = 0; j < iventory.itens.Count; j++)
            {
                GameObject tempItem = Instantiate(itemListPrefab, content.transform);
                tempItem.GetComponent<ItemList>().setUpItem(iventory.itens[j]);
                itens.Add(tempItem.GetComponent<ItemList>());
            }
        }


    }
    void UpdateAtributes()
    {
        vidaTexto.text = "Vida: " + Player_Controller.vidaAtual + "/" + player.vidaMax;
        Debug.Log(vidaTexto.text);
        manaTexto.text = "Mana: " + Player_Controller.manaAtual + "/" + player.manaMax;
        forcaTexto.text = "Força: " + Player_Controller.forca;
        ataqueTexto.text = "Ataque: " + (Player_Controller.forca + player.GetComponentInChildren<Attack>().getDano());
        defesaTexto.text = "Defesa: " + player.defesa;
    }

}