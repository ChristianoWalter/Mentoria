using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string palavra = "palavra";
    private bool binario = true;
    int numeroInteiro = 1;
    float numeroQuebrado = 1.5f;
    Vector2 doisVetores;
    Transform tamanhoLocalDirecao;
    enum selecaoEnumerada { um, dois, tres, quatro};
    List<selecaoEnumerada> listas;

    GameObject objeto;


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Exemplo1();
    }

    public void Exemplo1()
    {
        Exemplo2();
    }

    public void Exemplo2()
    {
        numeroInteiro = 2 + 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
