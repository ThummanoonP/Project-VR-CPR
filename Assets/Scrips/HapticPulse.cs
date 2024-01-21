using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticPulse : MonoBehaviour
{
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;
    [SerializeField] private GameObject Character;
    [SerializeField] private GameObject LeftHandTouch;
    [SerializeField] private GameObject RightHandTouch;



    private CharacterHeartRate HeartRate;

    bool LeftCheck = false;
    bool RightCheck = false;
    private float duration = 0.0f;
    private float timer = 0.0f;
    private ActionBasedController LeftController = null;
    private ActionBasedController RightController = null;

    private void OnTriggerEnter(Collider other)
    {

        if ((other.gameObject.name == "Right Controller") && ( LeftCheck == false))
        {
            RightHandTouch.SetActive(true);
            RightCheck = true;

        }
        else if ((other.gameObject.name == "Left Controller") && ( RightCheck == false))
        {
            LeftHandTouch.SetActive(true);
            LeftCheck  = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Right Controller" && (RightCheck == true))
        {
            RightHandTouch.SetActive(false);
            RightCheck = false;
        }
        else if (other.gameObject.name == "Left Controller" && (LeftCheck == true))
        {
            LeftHandTouch.SetActive(false);
            LeftCheck  = false;
        }
    }

    public bool HandOutOfPosition()
    {
        if ((LeftCheck == false) && (RightCheck == false)) return true;
        else return false;
    }


    void Update()
    {
        HeartRate = Character.GetComponent<CharacterHeartRate>();
        timer += Time.deltaTime;
        RightController = RightHand.GetComponent<ActionBasedController>();
        LeftController = LeftHand.GetComponent<ActionBasedController>();

        if ((RightHandTouch.activeSelf) || (LeftHandTouch.activeSelf)) 
        {
            if (timer >= (60.0f / HeartRate.GetHeartRate()))
            {
                duration = (HeartRate.GetHeartRate() / 120) * 0.2f;

                if (RightCheck) RightController.SendHapticImpulse(0.02f, duration);
                else if (LeftCheck) LeftController.SendHapticImpulse(0.02f, duration);

                timer = 0.0f;
            }
        }
        
    }
}