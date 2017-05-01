using UnityEngine;
using System.Collections;

public class OffsetCalibration : MonoBehaviour {

    public GameObject SwordObject;

    public GameObject Calibrator1;
    public GameObject Calibrator2;

    private Vector3 offset, scloffset, tmp, difference;
    private Vector3 tmpOffset;
    private Quaternion rotoffset;

    // Use this for initialization
    void Start () {
        offset = new Vector3();
        rotoffset = new Quaternion();
        scloffset = new Vector3(1, 1, 1);
        StartCoroutine(setOffset());
        Calibrator2.SetActive(false);
        tmp = new Vector3(0,0,0);
        difference = new Vector3();
        tmpOffset = new Vector3(0.75f, 0.75f, 0.75f);
    }
	
	// Update is called once per frame
	void Update () {
        difference = SwordObject.transform.position - tmp;
        SwordObject.transform.position += Vector3.Scale(difference, scloffset);
        //SwordObject.transform.rotation = SwordObject.transform.rotation; // * rotoffset;

        // Debug.Log(CubeObject.transform.position);
        //Debug.Log(scloffset);

        tmp = SwordObject.transform.position;        
	}

    IEnumerator setOffset()
    {
        Vector3 pos1, pos2;
        yield return new WaitForSeconds(5);
        // newed object
        SwordObject = GameObject.FindGameObjectWithTag("Cube");
        offset = Calibrator1.transform.position - SwordObject.transform.position;
        SwordObject.transform.position = Calibrator1.transform.position;

        //tmp = EmptySword.transform.position;
        //Debug.Log(offset.ToString());

        pos1 = SwordObject.transform.position;
        rotoffset = Quaternion.Inverse(SwordObject.transform.rotation) * Calibrator1.transform.rotation;
        Calibrator1.SetActive(false);
        Calibrator2.SetActive(true);
        yield return new WaitForSeconds(5);
        pos2 = SwordObject.transform.position;

        scloffset.x = (Calibrator2.transform.position.x - Calibrator1.transform.position.x) / (pos2.x - pos1.x);
        scloffset.y = (Calibrator2.transform.position.y - Calibrator1.transform.position.y) / (pos2.y - pos1.y);
        scloffset.z = (Calibrator2.transform.position.z - Calibrator1.transform.position.z) / (pos2.z - pos1.z);

        //offset = Calibrator2.transform.position - SwordObject.transform.position;
        //SwordObject.transform.position = SwordObject.transform.position + offset;
        CubeObject.transform.position = Calibrator2.transform.position;

        //scloffset.x *= 0.9f;
        //scloffset.z *= 0.9f;

        Debug.Log(scloffset.ToString());
        Calibrator2.SetActive(false);
    }
}