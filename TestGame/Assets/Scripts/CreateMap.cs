using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    //�I�u�W�F�N�g
    public GameObject grass;
    public GameObject mountain;
    public GameObject water;
    public GameObject castle1;
    public GameObject castle2;
    public GameObject resource;

    //�^�C���ݒu�̍ŏ��̈ʒu
    public float SetTileStart_X;
    public float SetTileStart_Y;
    //�^�C���̑傫��
    public float TILESIZE_X;
    public float TILESIZE_Y;
    //�}�b�v�T�C�Y
    public int MAPSIZE_X;
    public int MAPSIZE_Y;
    //�^�C���Ԃ̒���
    public float TILESPACE;

    //�X�y�[�X�������ꂽ���t���O
    private bool PUSHSPACE;
    //���ݐݒu���Ă���^�C���̃}�b�v���W
    private int x, y;
    //���ݐݒu���Ă���^�C���̃��[���h���W
    private float SET_X, SET_Y;

    private List<string> smap = new List<string>();
    public int[] map;
    //csv�t�@�C���̏ꏊ
    private string csv_place = "Assets/Resources/map.csv";

    /// <summary>
    /// csv�t�@�C���̓ǂݍ��ݗp���W���[��
    /// </summary>
    /// <param name="..//Resources/map.csv">csv�t�@�C���̃p�X</param>
    /// <returns>csv���番�����ꂽList<string>��Ԃ�</string></returns>
    public List<string> Csv_Input(string pass)
    {
        List<string> str_lists = new List<string>();//�l�i�[�p���X�g
        try
        {
            //�p�X���w�肵��csv�t�@�C�����J��
            StreamReader csv = new StreamReader(pass);
            //�t�@�C�������܂Ŏ��s
            while (!csv.EndOfStream)
            {
                string line = csv.ReadLine();//�t�@�C������1�s�ǂݍ���
                string[] values = line.Split(',');//","�ŋ�؂��Ĕz��ɕۑ�
                str_lists.AddRange(values);// �z�񂩂烊�X�g�Ɋi�[����
            }
            csv.Close();//�t�@�C�������
            Debug.Log("public List<string> Csv_Input(string pass)�ł̓ǂݍ��݊���");
        }
        catch
        {
            Debug.Log("public List<string> Csv_Input(string pass)�ł̓ǂݍ��݃G���[");
        }
        return str_lists;//string�^���X�g��߂�
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
                    case 0://��
                        Instantiate(grass, new Vector3(SET_X, SET_Y, 0.0f), Quaternion.identity);
                        break;
                    case 1://�R
                        Instantiate(mountain, new Vector3(SET_X, SET_Y, 0.0f), Quaternion.identity);
                        break;
                    case 2://��
                        Instantiate(water, new Vector3(SET_X, SET_Y, 0.0f), Quaternion.identity);
                        break;
                    case 3://����
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

