using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    // บางฟังก์ชันจะต้องการอ้างอิงถึง controller
    public GameObject controller;

    // หมากรุกที่ถูกแตะเพื่อสร้าง MovePlate นี้
    GameObject reference = null;

    // ตำแหน่งบนกระดาน
    int matrixX;
    int matrixY;

    // false: การเคลื่อนที่, true: การโจมตี
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            // ตั้งค่าเป็นสีแดง
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        // ทำลายหมากรุกที่ถูกโจมตี
        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black");
            if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white");

            Destroy(cp);
        }

        // ตั้งค่าตำแหน่งเดิมของหมากรุกให้ว่าง
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard());

        // ย้ายหมากรุกไปยังตำแหน่งนี้
        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        // อัปเดตเมทริกซ์
        controller.GetComponent<Game>().SetPosition(reference);

        // สลับผู้เล่นปัจจุบัน
        controller.GetComponent<Game>().NextTurn();

        // ทำลาย move plates รวมถึงตัวเอง
        reference.GetComponent<Chessman>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
