using UnityEngine;

public enum Collectible
{
    // criar prefab de cada um, mesmo que se repita a mesma aparencia - o codigo e características é diferente
    Stone, // pedra -> pedra cabe nos braços
    Wood, // madeira -> arvore tem que vira toras nos braços
    Fruit, // Fruta -> arbustinho tem que virar caixa de frutas nos braços
    Water, // Água -> poça dágua tem que virar balde com particula de água dentro
    Oil, // Oleo -> tonel de óleo cabe nos braços (talvez o prefab tenha que diminuir)
    Rag, // Pano -> tem que virar bolsa de pano
}
