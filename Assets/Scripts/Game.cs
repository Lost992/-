using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // อ้างอิงจาก Unity IDE
    public GameObject chesspiece;

    // เมทริกซ์ที่จำเป็น ตำแหน่งของ GameObjects แต่ละตัว
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    // ผู้เล่นปัจจุบัน
    private string currentPlayer = "white";

    // การสิ้นสุดเกม
    private bool gameOver = false;

    // Unity เรียกฟังก์ชันนี้เมื่อเกมเริ่มต้น
    public void Start()
    {
        playerWhite = new GameObject[] { Create("white_rook", 0, 0), Create("white_knight", 1, 0),
            Create("white_bishop", 2, 0), Create("white_queen", 3, 0), Create("white_king", 4, 0),
            Create("white_bishop", 5, 0), Create("white_knight", 6, 0), Create("white_rook", 7, 0),
            Create("white_pawn", 0, 1), Create("white_pawn", 1, 1), Create("white_pawn", 2, 1),
            Create("white_pawn", 3, 1), Create("white_pawn", 4, 1), Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1), Create("white_pawn", 7, 1) };
        playerBlack = new GameObject[] { Create("black_rook", 0, 7), Create("black_knight",1,7),
            Create("black_bishop",2,7), Create("black_queen",3,7), Create("black_king",4,7),
            Create("black_bishop",5,7), Create("black_knight",6,7), Create("black_rook",7,7),
            Create("black_pawn", 0, 6), Create("black_pawn", 1, 6), Create("black_pawn", 2, 6),
            Create("black_pawn", 3, 6), Create("black_pawn", 4, 6), Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6), Create("black_pawn", 7, 6) };

        // ตั้งค่าตำแหน่งของหมากรุกทั้งหมดบนกระดาน
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>(); // เรามีการเข้าถึง GameObject เราต้องการสคริปต์
        cm.name = name; // นี่เป็นตัวแปรที่ Unity มีอยู่แล้ว ดังนั้นเราไม่ต้องประกาศมันก่อน
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate(); // มันมีทุกอย่างที่ตั้งค่าแล้วจึงสามารถ Activate() ได้
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        // เขียนทับพื้นที่ว่างหรืออะไรก็ตามที่อยู่ที่นั่น
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    }

    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            // ใช้ UnityEngine.SceneManagement ที่นี่
            SceneManager.LoadScene("Game"); // เริ่มเกมใหม่โดยการโหลดฉากอีกครั้ง
        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;

        // ใช้ UnityEngine.UI ที่นี่
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }
}
