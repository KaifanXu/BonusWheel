using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class start_spin : MonoBehaviour
{
    public float spinTime = 3;
    public GameObject wheel;
    public GameObject prizeResult;
    public GameObject spriteHolder;
    public GameObject spinButton;
    private bool ifSpin;
    private float spinAngle;
    private int randomPrize;
    private int randIndex;
    
    public List<string> prize = new List<string> 
    {
        "Life 30 min",
        "Brush 3X",
        "Gems 35",
        "Hammer 3X",
        "Coins 750",
        "Brush 1X",
        "Gems 75",
        "Hammer 1X"
    };
    public List<Sprite> resultPrize = new List<Sprite>(8);
    public List<int> weight = new List<int>(8) { 20, 10, 10, 10, 5, 20, 5, 20 }; //should sum up to 100
    public List<AnimationCurve> animationCurves;

    private Dictionary<int, int> prizeWeights = new Dictionary<int, int>(8);
    private List<int> weightedPrize = new List<int>();
 
    // Start is called before the first frame update
    void Start()
    {
        ifSpin = false;
        spinAngle = 360/8;
        weightedPrize.Clear();

        for (var i = 0; i < weight.Count; i++)
        {
            prizeWeights[i + 1] = weight[i];
        }
        foreach(var v in prizeWeights)
        {
            for(var i = 0; i < v.Value; i++)
            {
                weightedPrize.Add(v.Key);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PreSpin()
    {
        if(!ifSpin)
        {
            randIndex = Random.Range(0, weightedPrize.Count);
            randomPrize = weightedPrize[randIndex];
            float randomAngle = Random.Range(1, spinAngle);
            float finalSpinAngle = (360 * spinTime + (randomPrize * spinAngle)) - randomAngle;
            
            StartCoroutine(SpinStarter(spinTime, finalSpinAngle));
        }
    }

    IEnumerator SpinStarter(float time, float finalAngle)
    {
        ifSpin = true;

        float timer = 0.0f;
        float startAngle = transform.eulerAngles.z;
        finalAngle -= startAngle;

        int randAnimationCurve = Random.Range(0, animationCurves.Count);

        while (timer < time)
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, finalAngle * animationCurves[0].Evaluate(timer / time) + startAngle);
            timer += Time.deltaTime;
            yield return 0;
        }
        transform.eulerAngles = new Vector3(0.0f, 0.0f, finalAngle + startAngle);
        StartCoroutine(Wait());
        
        //wheel.SetActive(true);
        Debug.Log("Prize: " + prize[randomPrize - 1]);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.6f);
        prizeResult.SetActive(true);
        spriteHolder.GetComponent<Image>().sprite = resultPrize[randomPrize - 1];
        spinButton.SetActive(false);    
        ifSpin = false;
    }
}
