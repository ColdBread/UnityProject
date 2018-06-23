using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public static LevelController current;
    public int life_num = 3;
    public Image life_pane;
    int coins_num = 0;
    public Sprite empty_heart;
    public Sprite full_heart;
    public Image coins_pane;
    public Image fruit_pane;
    public int total_fruit_num = 11;
    public Image crystals_pane;
    public Sprite Blue;
    public Sprite Green;
    public Sprite Red;
    bool isBlueFound;
    bool isGreenFound;
    bool isRedFound;
    int fruit_num = 0;
    Vector3 startingPosition;

    public void onRabitDeath(HeroRabbit rabit)
    {
        //При смерті кролика повертаємо на початкову позицію minus zhizn`
        life_num--;
        if(life_num > 0){
            Image[] hearts = life_pane.GetComponentsInChildren<Image>();
            hearts[life_num].sprite = empty_heart;
            rabit.transform.position = this.startingPosition;
        } else {
            SceneManager.LoadScene("ChooseLevelScene");
        }
    }

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public void goToChooseLevel(){
        SceneManager.LoadScene("ChooseLevelScene");
    }

    void Awake()
    {
        current = this;
    }

    public void addCoins()
    {
        coins_num++;
        string text;
        if(coins_num <10){
            text = "000"+coins_num;
        } else if (coins_num < 100) {
            text = "00"+coins_num;
        } else if (coins_num < 1000) {
            text = "0"+coins_num;
        } else {
            text = coins_num.ToString();
        }
        coins_pane.GetComponentInChildren<Text>().text = text;

    }
    public void addFruits()
    {
        fruit_num++;
        fruit_pane.GetComponentInChildren<Text>().text = fruit_num+"/"+total_fruit_num;
    }
    public void addCrystals(Crystal color)
    {
        Image[] crystals = crystals_pane.GetComponentsInChildren<Image>();
        switch (color)
        {
            case Crystal.Blue:
                
                foreach(Image crystal in crystals){
                    if(crystal.name == "Crystal-Blue"){
                        crystal.sprite = Blue;
                        isBlueFound = true;
                    }
                }
                break;
            case Crystal.Green:
                
                foreach(Image crystal in crystals){
                    if(crystal.name == "Crystal-Green"){
                        crystal.sprite = Green;
                        isGreenFound = true;
                    }
                }
                break;
            case Crystal.Red:
                foreach(Image crystal in crystals){
                    if(crystal.name == "Crystal-Red"){
                        crystal.sprite = Red;
                        isRedFound = true;
                    }
                }
                break;
            default:
                break;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
