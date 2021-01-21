using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraTest : MonoBehaviour {

    public MonoBehaviour[] cameras;
    public Text cameraName;
    public Text variableValue;
    public Text description;

    private Type cameraType;
    private MonoBehaviour currentCamera;
    private int cameraIndex = 0;
    private int previousIndex = 0;

	void Start () {
        EventPool eventPool = GameObject.FindObjectOfType<EventPool>();
        GameEvent playerFoundEvent;
        eventPool.GetEventFromPool("Player Found", out playerFoundEvent);
        playerFoundEvent.onHandleEvent += EventZoom;

        currentCamera = cameras[0];
        cameraType = currentCamera.GetType();
        cameraName.text = cameraType.ToString();
        description.text = "This camera follows the player rigidly and can zoom in and out\npress up/down arrows to increase/decrease";
        
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            cameraIndex ++;

        if (previousIndex != cameraIndex)
        {
            for (int i = 0; i < cameras.Length; i++)
                cameras[i].enabled = false;


            currentCamera = cameras[cameraIndex % cameras.Length];
            currentCamera.enabled = true;
            cameraType = currentCamera.GetType();

            cameraName.text = cameraType.ToString();
        }

        if (cameraType == typeof(CameraBehavior))
        {
            variableValue.text = "Zoom : " + Camera.main.orthographicSize;
            description.text = "Thia camera follows the player rigidly and can zoom in and out\npress up/down arrows to increase/decrease zoom";

            if (Input.GetKey(KeyCode.UpArrow))
                Camera.main.orthographicSize += 0.01f;
            else if (Input.GetKey(KeyCode.DownArrow))
                Camera.main.orthographicSize -= 0.01f;
        }
        else if (cameraType == typeof(SpringCamera))
        {
            SpringCamera dCamera = currentCamera as SpringCamera;
            variableValue.text = "Spring Stiffness : " + dCamera.springReference.stiffness;
            description.text = "This camera uses a spring to extend the camera in the movement direction and" + 
                "snap back to player\npress up/down arrows to increase/decrease spring stiffness";

            if (Input.GetKey(KeyCode.UpArrow))
                dCamera.springReference.stiffness += 0.01f;
            else if (Input.GetKey(KeyCode.DownArrow))
                dCamera.springReference.stiffness -= 0.01f;
        }
        else if (cameraType == typeof(InterpolationCamera))
        {
            InterpolationCamera dCamera = currentCamera as InterpolationCamera;
            variableValue.text = "Extend Distance : " + dCamera.cameraExtentDistance;
            description.text = "Thia camera Extends a set distance away from the player in the direction " +
                "of the destination. Once close enoght the camera halts\npress up/down arrows to increase/decrease distance";

            if (Input.GetKey(KeyCode.UpArrow))
                dCamera.cameraExtentDistance += 0.01f;
            else if (Input.GetKey(KeyCode.DownArrow))
                dCamera.cameraExtentDistance -= 0.01f;
        }
        else if (cameraType == typeof(DestinationCamera))
        {

            DestinationCamera dCamera = currentCamera as DestinationCamera;
            variableValue.text = "Speed : " + dCamera.speed;
            description.text = "This camera moves straight from the player position to the chosen destination " +
                "at a set speed\npress up/down arrows to increase/decrease speed";

            if (Input.GetKey(KeyCode.UpArrow))
                dCamera.speed += 0.01f;
            else if (Input.GetKey(KeyCode.DownArrow))
                dCamera.speed -= 0.01f;
        }

        previousIndex = cameraIndex;
    }

    void EventZoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
