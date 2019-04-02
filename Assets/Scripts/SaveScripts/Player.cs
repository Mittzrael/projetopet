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
    public int graphNumber = -1;

    /// <summary>
    /// Horário de saída da última seção de jogador em forma de string
    /// </summary>
    public string lastTimePlayed;
    /// <summary>
    /// Licais onde estão os cocôs do animal
    /// </summary>
    public PoopLocation poopLocation = new PoopLocation();
    /// <summary>
    /// Locais onde estão os xixis do animal
    /// </summary>
    public PeeLocation peeLocation = new PeeLocation();
    /// <summary>
    /// Lugar do pote de comida
    /// </summary>
    public ElementLocation foodPotLocation;
    /// <summary>
    /// Lugar do pote de água
    /// </summary>
    public ElementLocation waterPotLocation;
    /// <summary>
    /// Lugar onde o cachoro irá fazer suas necessidades fisiológicas.
    /// </summary>
    public ElementLocation wasteLocation;

    /// <summary>
    /// Status do pot de comida
    /// </summary>
    public PotStatus foodPot;
    /// <summary>
    /// Status do pot de água
    /// </summary>
    public PotStatus waterPot;

    public List<Flag> flag = new List<Flag>();
}