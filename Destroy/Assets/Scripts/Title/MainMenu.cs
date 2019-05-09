using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    class Menu
    {
        public GameObject parent    { private set; get; }
        public GameObject selecting { private set; get; }
        public GameObject selected  { private set; get; }

        public Menu(GameObject parent)
        {
            this.parent    = parent;
            this.selecting = parent.transform.Find("Selecting").gameObject;
            this.selected  = parent.transform.Find("Selected").gameObject;
        }
    }
    private Menu start;
    private Menu ranking;
    private Menu quit;

    private Vector3 enableSelect;
    private Vector3 disableSelect;

    private int selectingNum;

    [SerializeField] AudioClip selectingSE;
    [SerializeField] AudioClip selectedStartSE;
    [SerializeField] AudioClip selectedSE;

    [SerializeField] string gameScene;
    [SerializeField] string rankingScene;

    [SerializeField] float waitTime;

    public void Awaked()
    {
        (this.start   = new Menu(GameObject.Find("Canvas/Main/Menu/Start"))).selected.SetActive(false);
        (this.ranking = new Menu(GameObject.Find("Canvas/Main/Menu/Ranking"))).selected.SetActive(false);
        (this.quit    = new Menu(GameObject.Find("Canvas/Main/Menu/Quit"))).selected.SetActive(false);

        this.enableSelect  = new Vector3(1.0f, 1.0f, 1.0f);
        this.disableSelect = new Vector3(0.8f, 0.8f, 0.8f);

        this.selectingNum = 0;
        SelectUpdate();
    }

    public void Updated()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (this.selectingNum < 2)
                {
                    this.selectingNum++;
                    SoundManager.Instance.PlaySE(this.selectingSE);
                    SelectUpdate();
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (this.selectingNum > 0)
                {
                    this.selectingNum--;
                    SoundManager.Instance.PlaySE(this.selectingSE);
                    SelectUpdate();
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(SelectButton());
            }
        }
    }

    private void SelectUpdate()
    {
        switch (this.selectingNum)
        {
            case 0:
                this.start.parent.transform.localScale = this.enableSelect;
                this.start.selecting.SetActive(true);
                this.ranking.parent.transform.localScale = this.disableSelect;
                this.ranking.selecting.SetActive(false);
                this.quit.parent.transform.localScale = this.disableSelect;
                this.quit.selecting.SetActive(false);
                break;

            case 1:
                this.start.parent.transform.localScale = this.disableSelect;
                this.start.selecting.SetActive(false);
                this.ranking.parent.transform.localScale = this.enableSelect;
                this.ranking.selecting.SetActive(true);
                this.quit.parent.transform.localScale = this.disableSelect;
                this.quit.selecting.SetActive(false);
                break;

            case 2:
                this.start.parent.transform.localScale = this.disableSelect;
                this.start.selecting.SetActive(false);
                this.ranking.parent.transform.localScale = this.disableSelect;
                this.ranking.selecting.SetActive(false);
                this.quit.parent.transform.localScale = this.enableSelect;
                this.quit.selecting.SetActive(true);
                break;
        }
    }

    private IEnumerator SelectButton()
    {
        switch (this.selectingNum)
        {
            case 0:
                if (!this.gameScene.Equals(""))
                {
                    GetComponent<TitleManager>().status = TitleStatus.None;

                    this.start.selecting.SetActive(false);
                    this.start.selected.SetActive(true);
                    SoundManager.Instance.PlaySE(this.selectedStartSE);

                    yield return new WaitForSeconds(waitTime);

                    MainGameManaer.ModeChange((GetComponent<TitleManager>().tarakoEdition) ? Mode.Tarako : Mode.Nomal);
                    SceneController.Instance.Load(this.gameScene);
                }
                break;

            case 1:
                if (!this.rankingScene.Equals(""))
                {
                    GetComponent<TitleManager>().status = TitleStatus.None;

                    this.ranking.selecting.SetActive(false);
                    this.ranking.selected.SetActive(true);
                    SoundManager.Instance.PlaySE(this.selectedSE);

                    yield return new WaitForSeconds(waitTime);

                    SceneController.Instance.Load(this.rankingScene);
                }
                break;

            case 2:
                this.quit.selecting.SetActive(false);
                this.quit.selected.SetActive(true);
                SoundManager.Instance.PlaySE(this.selectedSE);

                yield return new WaitForSeconds(waitTime);

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE
                UnityEngine.Application.Quit();
#endif
                break;
        }
    }
}
