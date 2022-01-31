using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicManager : MonoBehaviour
{
    [SerializeField] public Picture PicturePrefab;
    [SerializeField] public Transform PicSpawnPosition;
    [SerializeField] public List<Picture> PictureList;
    [SerializeField] public Vector2 StartPosirion = new Vector2(0f, 10f);
    [SerializeField] private Vector2 offSet = new Vector2(2f, 2f);
    [SerializeField] private List<Material> materialList = new List<Material>();
    [SerializeField] private List<string> texturePathList = new List<string>();
    [SerializeField] private Material firstMaterial;
    [SerializeField] private string firstTexturePath;

    void Start()
    {
        LoadMaterials();
        SpawnPictureMesh(4, 5, StartPosirion, offSet, false);
        MovePic(4, 5, StartPosirion, offSet);
    }

    private void LoadMaterials()
    {
        print("StartLoadMaterials");
        var materialFilePath = GameSet.Instance.GetMaterialDirectoryName();
        var textureFilePath = GameSet.Instance.GetPuzzleCategoryTextureDirectoryName();
        var pairNumber = (int)GameSet.Instance.GetPairNumber();
        const string matBaseName = "Pic";
        var firstMaterialName = "Back";
        for(var index = 1; index <= pairNumber; index++)
        {
            var currentFilePath = materialFilePath + matBaseName + index;
            Material mat = Resources.Load(currentFilePath, typeof(Material)) as Material;
            materialList.Add(mat);
            var currentTextureFilePath = textureFilePath + matBaseName + index;
            texturePathList.Add(currentTextureFilePath);
        }
        firstTexturePath = textureFilePath + firstMaterialName;
        print(materialFilePath + "  " + firstMaterialName);
        firstMaterial = Resources.Load(materialFilePath + firstMaterialName + typeof(Material)) as Material;
        print("EndLoadMaterials");
    }

    void Update()
    {
        
    }
    void SpawnPictureMesh(int row, int colum, Vector2 Pos, Vector2 Offset, bool Scale)
    {
        for(int i = 0; i < colum; i++)
        {
            for(int j = 0; j < row; j++)
            {
                var Pic = (Picture)Instantiate(PicturePrefab, PicSpawnPosition.position, PicturePrefab.transform.rotation);
                Pic.name = Pic.name + 'C' + i + 'R' + j;
                PictureList.Add(Pic);
            }
        }
        ApplyTextures();
    }

    public void ApplyTextures()
    {
        print("StartApplyTextures");
        var rndMaterialIndex = Random.Range(0, materialList.Count);
        var AppliedTimes = new int[materialList.Count];
        for(int i = 0; i < materialList.Count; i++)
        {
            AppliedTimes[i] = 0;
        }
        foreach(var o in PictureList)
        {
            var randPrevious = rndMaterialIndex;
            var counter = 0;
            var forceMat = false;
            while (AppliedTimes[rndMaterialIndex] >= 2 || ((randPrevious == rndMaterialIndex) && !forceMat))
            {
                rndMaterialIndex = Random.Range(0, materialList.Count);
                counter++;
                if (counter > 100)
                {
                    for(var j = 0; j < materialList.Count; j++)
                    {
                        if (AppliedTimes[j] < 2)
                        {
                            rndMaterialIndex = j;
                            forceMat = true;
                        }
                    }
                    if (forceMat == false)
                    {
                        return;
                    }
                }
            }
            print(firstMaterial + "  " + firstTexturePath);
            o.SetFirstMaterial(firstMaterial, firstTexturePath);
            o.ApplyFirstMaterial();
            o.SetSecondMaterial(materialList[rndMaterialIndex], texturePathList[rndMaterialIndex]);
            AppliedTimes[rndMaterialIndex] += 1;
            forceMat = false;
        }
        print("EndApplyTextures");
    }

    void MovePic(int row,int colum, Vector2 Pos, Vector2 Offset)
    {
        var index = 0;
        for (var i = 0; i < colum; i++)
        {
            for (var j = 0; j < row; j++)
            {
                var targetPos = new Vector3((Pos.x + (Offset.x * j)), (Pos.y - (Offset.y * i)), 0);
                StartCoroutine(MovePicToPos(targetPos, PictureList[index]));
                index++;
            }
        }
    }
    private IEnumerator MovePicToPos(Vector3 target,Picture pic)
    {
        var Speed = 10;
        while (pic.transform.position != target)
        {
            pic.transform.position = Vector3.MoveTowards(pic.transform.position, target, Speed * Time.deltaTime);
            yield return 0;
        }
    }
}
