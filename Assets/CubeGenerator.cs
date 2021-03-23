using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeGenerator : MonoBehaviour
{
    public GameObject block;

    public GameObject blade;
    // x
    private int width;
    // y
    private int height;
    // z
    private int length;
    private float delay;

    private int inputx;
    private int inputz;
    private int inputdx;
    private int inputdy;
    private int inputdz;
    
    private IEnumerator automode;
    private List<GameObject> cubes = new List<GameObject>();
    private float timer = 0;
   
    void Start()
    {
        
        GameObject.Find("nextstep").GetComponent<Button>().interactable = false;
        GameObject.Find("stop").GetComponent<Button>().interactable = false;
        GameObject.Find("start").GetComponent<Button>().interactable = false;
    }

    public void AddDetail()
    {
        delay = float.Parse(GameObject.Find("tzad").GetComponent<InputField>().text.Replace('.',','));
        width = int.Parse(GameObject.Find("xmax").GetComponent<InputField>().text.Replace('-','+'));
        height = int.Parse(GameObject.Find("ymax").GetComponent<InputField>().text.Replace('-','+'));
        length = int.Parse(GameObject.Find("zmax").GetComponent<InputField>().text.Replace('-','+'));
   
        inputx = int.Parse(GameObject.Find("x0").GetComponent<InputField>().text.Replace('-','+'));
        inputz = int.Parse(GameObject.Find("z0").GetComponent<InputField>().text.Replace('-','+'));
        inputdx = int.Parse(GameObject.Find("dx").GetComponent<InputField>().text.Replace('-','+'));
        inputdy = int.Parse(GameObject.Find("dy").GetComponent<InputField>().text.Replace('-','+'));
        inputdz = int.Parse(GameObject.Find("dz").GetComponent<InputField>().text.Replace('-','+'));
        
        automode = destroyer(inputx,inputz,inputdx,inputdy,inputdz);
        blade = Instantiate(blade, new Vector3(inputx, inputz, height + 1), Quaternion.identity);
        blade.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        for (int y = 1; y <= height; ++y)
        {
            for (int x = 1; x <= width; ++x)
            {
                for (int l = 1; l <= length; ++l)
                {
                    GameObject obj = Instantiate(block, new Vector3(x, y, l), Quaternion.identity);
                    obj.name = ((int) (obj.transform.position.x)).ToString() +
                               ((int) (obj.transform.position.y)).ToString() +
                               ((int) (obj.transform.position.z)).ToString();
                }
            }
        }
        
        GameObject.Find("adddetail").GetComponent<Button>().interactable = false;
        GameObject.Find("nextstep").GetComponent<Button>().interactable = true;
        GameObject.Find("stop").GetComponent<Button>().interactable = false;
        GameObject.Find("start").GetComponent<Button>().interactable = true;
        
    }

    public void StartAutoMode()
    {
        StartCoroutine(automode);
    }
    
    public void StopAutoMode()
    {
        StopCoroutine(automode);
    }

    public void NextStep()
    {
        StartCoroutine(automode);
        StopCoroutine(automode);
    }

    public void ActivateNextStep()
    {
        GameObject.Find("nextstep").GetComponent<Button>().interactable = true;
        GameObject.Find("stop").GetComponent<Button>().interactable = false;
        GameObject.Find("start").GetComponent<Button>().interactable = true;
    }
    
    public void DeactivateNextStep()
    {
        GameObject.Find("nextstep").GetComponent<Button>().interactable = false;
        GameObject.Find("stop").GetComponent<Button>().interactable = true;
        GameObject.Find("start").GetComponent<Button>().interactable = false;
    }
    

    IEnumerator destroyer(int x0, int z0, int dx, int dy, int dz)
    {
        for (int y = height; y >= height - dy + 1; --y)
        {
            if (y % 2 == 0)
            {
                for (int x = x0 ; x <= dx; ++x)
                {
                    for (int z = z0; z <= dz; ++z)
                    {
                        GameObject.Find(x.ToString() + y.ToString() + z.ToString()).SetActive(false);
                        blade.transform.position = new Vector3(x, y + 1, z);
                        yield return new WaitForSeconds(delay);
                    }
                }
            }
            else
            {
                for (int x = dx ; x >= x0; --x)
                {
                    for (int z = dz; z >= z0; --z)
                    {
                        GameObject.Find(x.ToString() + y.ToString() + z.ToString()).SetActive(false);
                        blade.transform.position = new Vector3(x, y + 1, z);
                        yield return new WaitForSeconds(delay);
                    }
                }
            }
        }
    }

    
    void Update()
    {
        
    }
}
