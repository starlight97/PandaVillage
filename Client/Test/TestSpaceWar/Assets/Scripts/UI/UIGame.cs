using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIGame : MonoBehaviour
{

    private Button attackBtn;    
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        attackBtn = GameObject.Find("ButtonAttack").GetComponent<Button>();        
        player = GameObject.Find("Player").GetComponent<Player>();


        this.attackBtn.onClick.AddListener(() => {
            player.Shoot();
        });


    }

   
}
