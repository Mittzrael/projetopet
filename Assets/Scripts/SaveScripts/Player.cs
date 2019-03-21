using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public int slot = -1; //Slots de save
    public string nome = ""; //Nome do jogador
    public Pet pet = new Pet();
    public int avatarSelecionado = -1;
    public int residenciaSelecionada = -1;
    public int petSelecionado = -1;
    public string lastTimePlayed;

    public PetAccessScenes petAccessScenes;
}