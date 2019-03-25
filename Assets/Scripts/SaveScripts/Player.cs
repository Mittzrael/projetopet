using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public int slot = -1; //Slots de save
    public string nome = ""; //Nome do jogador
    public Health health = new Health();
    public int avatarSelecionado = -1;
    public int residenciaSelecionada = -1;
    public int petSelecionado = -1;

    public PetAccessScenes petAccessScenes;

    public string lastTimePlayed; //Horário de saída da última seção do jogador em forma de string
    public PoopLocation poopLocation = new PoopLocation(); //Locais onde estão os cocôs do animal
    public PeeLocation peeLocation = new PeeLocation(); //Locais onde estão os xixis do animal

    public List<Flag> flag = new List<Flag>();
}