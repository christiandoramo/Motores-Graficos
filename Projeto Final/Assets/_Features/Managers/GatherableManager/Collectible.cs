using UnityEngine;

public enum Collectible
{
    // criar prefab de cada um, mesmo que se repita a mesma aparencia - o codigo e caracter�sticas � diferente
    Stone, // pedra -> pedra cabe nos bra�os
    Wood, // madeira -> arvore tem que vira toras nos bra�os
    Fruit, // Fruta -> arbustinho tem que virar caixa de frutas nos bra�os
    Water, // �gua -> po�a d�gua tem que virar balde com particula de �gua dentro
    Oil, // Oleo -> tonel de �leo cabe nos bra�os (talvez o prefab tenha que diminuir)
    Rag, // Pano -> tem que virar bolsa de pano
}
