using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private int _Zoffset = -10;
    private int _Yoffset = 1;
    private string sceneName;
    
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
    
    void Update()
    {
        switch (sceneName)
        {
            case "bill_horizontalScene":
                transform.position = new Vector3(player.transform.position.x, 0, _Zoffset);
                break;
            case "bill_verticalScene":
                if (player.transform.position.y > -1)
                { 
                    transform.position = new Vector3(0, player.transform.position.y + _Yoffset, _Zoffset);
                }
                break;
        }
   
    }
}
