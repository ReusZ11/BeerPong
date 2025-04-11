using UnityEngine;

public enum AlcoType
{ 
    Beer,
    Vodka,
    Whiskey,
    Tequila
}
public class PongAlco : MonoBehaviour
{
    [SerializeField]
    private int beerDamage = 15;
    [SerializeField]
    private int vodkaDamage = 40;
    [SerializeField]
    private int whiskeyDamage = 30;
    [SerializeField]
    private int tequilaDamage = 25;


    private Score ScoreText;
    public GameObject enemy;
    private Enemy enemyScript;

    [SerializeField]
    private GameObject animPrefab;

    private void Awake()
    {
       /// enemy = GameObject.Find("Enemy");

    }


    public void ShowScore()
    {
        // ScoreText = Instantiate(animPrefab, score.transform).GetComponent<Score>();

    }

    public int GetDamage(AlcoType type)
    {
        switch (type)
        {
            case AlcoType.Beer:
                //enemy.GetComponent<Enemy>().OnPongLandedToTheCup(beerDamage);
                return beerDamage;
            case AlcoType.Vodka:
                return vodkaDamage;
            case AlcoType.Whiskey:
                return whiskeyDamage;
            case AlcoType.Tequila:
                return tequilaDamage;
            default:
                Debug.LogError("PongAlco: An unknown enum was given");
                return 0;
        }
    }


  
    
}
