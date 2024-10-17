using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    //オブジェクト
    public GameObject grass;
    public GameObject mountain;
    public GameObject water;
    public GameObject castle1;
    public GameObject castle2;
    public GameObject resource;

    //タイル設置の最初の位置
    public float SetTileStart_X;
    public float SetTileStart_Y;
    //タイルの大きさ
    public float TILESIZE_X;
    public float TILESIZE_Y;
    //マップサイズ
    public int MAPSIZE_X;
    public int MAPSIZE_Y;
    //タイル間の長さ
    public float TILESPACE;

    //スペースが押されたかフラグ
    private bool PUSHSPACE;
    //現在設置しているタイルのマップ座標
    private int x, y;
    //現在設置しているタイルのワールド座標
    private float SET_X, SET_Y;

    private List<string> smap = new List<string>();
    public int[] map;
    //csvファイルの場所
    private string csv_place = "Assets/Resources/map.csv";

    /// <summary>
    /// csvファイルの読み込み用モジュール
    /// </summary>
    /// <param name="..//Resources/map.csv">csvファイルのパス</param>
    /// <returns>csvから分割されたList<string>を返す</string></returns>
    public List<string> Csv_Input(string pass)
    {
        List<string> str_lists = new List<string>();//値格納用リスト
        try
        {
            //パスを指定してcsvファイルを開く
            StreamReader csv = new StreamReader(pass);
            //ファイル末尾まで実行
            while (!csv.EndOfStream)
            {
                string line = csv.ReadLine();//ファイルから1行読み込み
                string[] values = line.Split(',');//","で区切って配列に保存
                str_lists.AddRange(values);// 配列からリストに格納する
            }
            csv.Close();//ファイルを閉じる
            Debug.Log("public List<string> Csv_Input(string pass)での読み込み完了");
        }
        catch
        {
            Debug.Log("public List<string> Csv_Input(string pass)での読み込みエラー");
        }
        return str_lists;//string型リストを戻す
    }

    // Start is called before the first frame update
    void Start()
    {
        map = new int[MAPSIZE_X * MAPSIZE_Y];
        smap = Csv_Input(csv_place);

        for (int i = 0; i < MAPSIZE_X * MAPSIZE_Y; i++)
        {
            map[i] = -999;
        }

        for (int i = 0; i < MAPSIZE_X * MAPSIZE_Y; i++)
        {
            map[i] = int.Parse(smap[i]);
            Debug.Log(map[i]);
            Debug.Log(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !PUSHSPACE)
        {
            PUSHSPACE = true;
        }

        if (PUSHSPACE)
        {

            if (y < MAPSIZE_Y)
            {
                SET_X = SetTileStart_X + (TILESIZE_X + TILESPACE) * x;
                SET_Y = SetTileStart_Y - (TILESIZE_Y + TILESPACE) * y;
                switch (map[x + y * MAPSIZE_Y])
                {
                    case 0://草
                        Instantiate(grass, new Vector3(SET_X, SET_Y, 0.0f), Quaternion.identity);
                        break;
                    case 1://山
                        Instantiate(mountain, new Vector3(SET_X, SET_Y, 0.0f), Quaternion.identity);
                        break;
                    case 2://水
                        Instantiate(water, new Vector3(SET_X, SET_Y, 0.0f), Quaternion.identity);
                        break;
                    case 3://資源
                        Instantiate(resource, new Vector3(SET_X, SET_Y, 0.0f), Quaternion.identity);
                        break;
                }
            }
            else if (y == MAPSIZE_X && x == 0)
            {
                Instantiate(castle1, new Vector3(SetTileStart_X + (TILESIZE_X + TILESPACE) * 22, SetTileStart_Y - (TILESIZE_Y + TILESPACE) * 22, 0.0f), Quaternion.identity);
                Instantiate(castle2, new Vector3(SetTileStart_X + (TILESIZE_X + TILESPACE) * 2, SetTileStart_Y - (TILESIZE_Y + TILESPACE) * 2, 0.0f), Quaternion.identity);
            }
            x++;
            if (x >= MAPSIZE_X)
            {
                x -= MAPSIZE_X;
                y++;
            }
        }
    }
}

