using System.Collections.Generic;
using UnityEngine;

public class GenMonster : MonoBehaviour
{
    // Start is called before the first frame update
    public FollowAI Silme;
    System.Random random = new System.Random();
    int totalMonster = 0;
    public GameObject x1;
    public GameObject x2;
    public GameObject y;
    public List<FollowAI> enemies;
    public static GenMonster instante;

    void Start()
    {
        float xLeft = x1.transform.position.x;
        float xRight = x2.transform.position.x;
        float yUp = x1.transform.position.y;
        float yDown = y.transform.position.y;

        totalMonster = random.Next(3, 6);
        genMonRandom(totalMonster, xLeft, xRight, yUp, yDown);
    }

    // Update is called once per frame
    void Update()
    {
        enemies.RemoveAll(item => item == null);
    }

    private void genMonRandom(int quantity, float xLeft, float xRight, float yUp, float yDown)
    {

        for (int i = 0; i < quantity; i++)
        {
            float x = UnityEngine.Random.Range(xLeft, xRight);
            float y = UnityEngine.Random.Range(yDown, yUp);
            Vector2 vector2 = new Vector2(x, y);
            Quaternion quaternion = Quaternion.identity;
            //Silme.transform.localScale = new Vector2(5f, 5f);
            FollowAI gameObject = Instantiate(Silme, vector2, quaternion);
            gameObject.gameObject.SetActive(false);
            enemies.Add(gameObject);
        }
    }

}
