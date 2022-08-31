using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    private VariableJoystick joystick;
    public UnityAction<Vector2> onUpdate;

    private void Awake()
    {
        joystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
    }
    private void Start()
    {    
        this.joystick.SnapX = true;
        this.joystick.SnapY = true;
    }


    public void Run()
    {
        this.StartCoroutine(RunRoutine());
    }
    private IEnumerator RunRoutine()
    {
        while (true)
        {

            this.onUpdate(new Vector2(joystick.Horizontal, joystick.Vertical));

            yield return null;
        }
    }
}
