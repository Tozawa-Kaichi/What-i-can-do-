using UnityEngine;

public class LevelupController : MonoBehaviour
{
    float m_timer = 0;
    [SerializeField] float m_1leveluptime = 160;
    [SerializeField] float m_2leveluptime = 160;
    [SerializeField] GameObject m_level1enemyPrefab;
    [SerializeField] GameObject m_level2enemyPrefab;
    [SerializeField] GameObject m_level3enemyPrefab;
    [SerializeField] Vector2 pos;
    bool levelupcount = false;
    bool levelupcount2 = false;
    GameObject m_activeMouse = default;

    // Start is called before the first frame update

    void Start()
    {
        Debug.Log(transform.position);
        levelupcount = true;
        m_activeMouse = Instantiate(m_level1enemyPrefab);
        m_level1enemyPrefab.transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        m_timer += Time.deltaTime;
        Debug.Log(transform.position);
        Levelup();
    }
    void Levelup()
    {
        if (m_timer > m_1leveluptime && levelupcount)
        {
            Destroy(m_activeMouse);
            m_activeMouse = Instantiate(m_level2enemyPrefab, m_activeMouse.transform.position, Quaternion.identity);
            Debug.Log("1Levelup");
            levelupcount = false;
            levelupcount2 = true;
        }
        if (m_timer > m_2leveluptime && levelupcount2)
        {
            Destroy(m_activeMouse);
            m_activeMouse = Instantiate(m_level3enemyPrefab, m_activeMouse.transform.position, Quaternion.identity);
            Debug.Log("2Levelup");
            levelupcount2 = false;
        }
    }

}