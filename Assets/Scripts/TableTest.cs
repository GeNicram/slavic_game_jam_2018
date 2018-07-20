using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTest : MonoBehaviour
{
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var tables = GameObject.FindGameObjectsWithTag("Table");
            var table = tables[Random.Range(0, tables.Length - 1)];

            table.GetComponent<Table>().Serve(0);
        }
	}
}
