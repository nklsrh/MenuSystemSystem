using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Menu_Levels : SampleGameMenuGroup
{
    [Header("LevelGrid")]
    public GridLayoutGroup grid;
    public Button buttonPrototype;

    public override void Open(params object[] parameters)
    {
        base.Open(parameters);

        BuildLevelList((string[])parameters);
    }

    public void BuildLevelList(string[] levels)
    {
        int size = levels.Length;

        buttonPrototype.gameObject.SetActive(true);

        foreach (Transform t in grid.transform)
        {
            Button wt = t.GetComponent<Button>();
            if (wt != null && wt != buttonPrototype)
            {
                Destroy(wt.gameObject);
            }
        }

        for (int i = 0; i < size; i++)
        {
            Button wt = Instantiate(buttonPrototype);
            wt.transform.SetParent(grid.transform, false);
            wt.transform.localScale = Vector3.one;

            wt.GetComponentInChildren<Text>().text = levels[i];
            string level = levels[i];
            wt.onClick.AddListener(() => { MenuController.StartLevel(level); });
        }

        buttonPrototype.gameObject.SetActive(false);
    }
}
