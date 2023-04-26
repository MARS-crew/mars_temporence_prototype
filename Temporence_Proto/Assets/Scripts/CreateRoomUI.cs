using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ETema
{
    Jurassic,
    Industry,
    Revolution
}


public class CreateRoomUI : MonoBehaviour
{

    [SerializeField]
    private List<Button> temaButtons;
    private CreateRoomData roomData;


        // Start is called before the first frame update
        void Start()
    {

        roomData = new CreateRoomData() { tema = ETema.Jurassic}; //기본값

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void UpdateRoomTema(int select)
    {
        // Debug.Log(roomData.tema);
       roomData.tema = (ETema)select;
        //모든 버튼 순회
        for (int i = 0; i < temaButtons.Count; i++)
        {
            if (i == select)
            {
                
                temaButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                temaButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        /*
        switch (roomData.tema)
        {
            case ETema.Jurassic:
                KeyboardMouseSelectButton.image.color = Color.green;
                ControlpadSelectButton.image.color = Color.white;
                break;
            case ETema.Industry:
                KeyboardMouseSelectButton.image.color = Color.white;
                ControlpadSelectButton.image.color = Color.green;
                break;
            case ETema.Revolution:
                KeyboardMouseSelectButton.image.color = Color.white;
                ControlpadSelectButton.image.color = Color.green;
                break;
        }
        */
    }

}

public class CreateRoomData
{
    public ETema tema; //ETema 타입의 전역 변수
}

