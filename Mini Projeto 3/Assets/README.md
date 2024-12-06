# Miniprojeto 3

<div  align="center">

![conceitos](image-3.png)
![especificação](image-4.png)

Motores Gráficos
2024.2
Mini projeto 3
Aluno: Christian Oliveira

</div>

<div  align="left">

-   [x] adicione uma fonte de luz pontual no sol que ilumina todo o sistema solar e a nave
-   [x] texturize o sol, todos os planetas e os satélites com mapas de texturas
-   [x] texturize a terra com algum tipo de mapa de relevo (heightmap, normalmap…)
-   [x] texturize a lua com algum tipo de mapa de relevo
-   [x] adicione um skybox à sua cena
-   [ ] desafio: faça com o seu skybox seja dinâmico
-   [ ] desafio: para ajudar na investigação das partes escuras do sistema solar, crie uma potente lanterna (spot light) na nave que controlável com o mouse ao pressionar e segurar o botão direito do mouse
-   [ ] desafio: use o scroll do mouse para aumentar/diminuir a potência da lanterna e inversamente diminuir/aumentar seu ângulo de abertura
-   [ ] desafio: crie 10 pontos de resgate espalhados no sistema solar. cada ponto de resgate deve ter uma luz pontual piscando em vermelho. opcionalmente você pode adicionar a cada ponto de resgate algum sólido ou modelo 3D. no momento em que a nave entra em contato com o ponto de resgate ele some e um contador é incrementado. ao completar 10 resgates o jogo termina.

### Faltando:

Aqui está a lista com as tarefas não marcadas detalhadas:

**Texturize a Terra com algum tipo de mapa de relevo (heightmap, normalmap...)**  
-   [ ] Use um *heightmap* para criar a sensação de topografia na superfície da Terra, como montanhas e vales. Combine com um *normalmap* para adicionar detalhes à textura, como relevos menores e mais precisos. Isso pode ser feito ajustando materiais na engine, como no Unity ou Unreal, para incorporar esses mapas ao *shader* da superfície.

**Texturize a Lua com algum tipo de mapa de relevo**  
-   [ ] Aplique um *heightmap* ou *normalmap* para destacar as crateras e formações rochosas da Lua. Recursos como mapas de *displacement* também podem ser utilizados para deformar levemente a superfície do modelo 3D, aumentando a imersão.

**Desafio: Faça com que o seu skybox seja dinâmico**  
-   [ ] Crie um *skybox* que muda ao longo do tempo para simular ciclos, como dia e noite, ou variações no espaço, como nebulosas que se deslocam. Use animações ou scripts para alterar dinamicamente as texturas ou cores do *skybox*.

**Desafio: Crie uma potente lanterna (spot light) na nave controlável com o mouse**  
-   [ ] Adicione uma *spot light* à nave e implemente controles para que ela siga a direção do mouse enquanto o botão direito estiver pressionado. Configure a luz para ter um feixe intenso e bem definido, útil para iluminar áreas escuras.

**Desafio: Use o scroll do mouse para ajustar a potência e o ângulo de abertura da lanterna**  
    Vincule o *scroll* do mouse ao controle da luz:  
-   [ ] Rolando para frente aumenta a potência e diminui o ângulo do feixe, tornando-o mais focado.  
-   [ ] Rolando para trás diminui a potência e aumenta o ângulo, espalhando a luz por uma área maior.  

- **Desafio: Crie 10 pontos de resgate no sistema solar**  
    Cada ponto de resgate deve conter:  
-   [ ] Uma luz pontual piscando em vermelho, simulando um sinal de emergência.  
-   [ ] Um modelo ou objeto 3D simples, como uma cápsula ou um marcador de resgate.  
  - Funcionalidade:  
-   [ ] Quando a nave entra em contato com um ponto, ele desaparece e um contador é incrementado.  
-   [ ] Após coletar os 10 pontos, exiba uma mensagem de conclusão ou termine o jogo.  
  - Dica: Use colisores com eventos (como *OnTriggerEnter* no Unity) para detectar a interação.

<!-- 
### Sumário

:o: [Contribuidores](#contribuidores)

:o: [Tecnologias](#tecnologias)

:o: [Especificações](#especificações)

:o: [Link](#link)

:o: [Dicas de acesso](#dicas-de-acesso-arrow_forward)

:o: [Anotações](#anotações) -->

<!-- ## Contribuidores

| [<img src="https://avatars.githubusercontent.com/u/116025325?v=4" width=115>](https://github.com/christiandoramo) |
| ----------------------------------------------------------------------------------------------------------------- |
| [Christian Oliveira](https://github.com/christiandoramo)                                                          |

<br> -->

## Tecnologias

[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=for-the-badge&logo=unity)](https://unity3d.com)

<br>

<!-- ## Especificações

### Requisitos

:pushpin: **1** - A hierarquia da cena deve corresponder a hierarquia do sistema solar

:pushpin: **2** - As mecânicas de rotação e translação de cada planeta devem funcionar assim que a simulação for iniciada

:pushpin: **3** - Cada planeta e satélite deve ter um elemento além da esfera, anexado a si, para evidenciar o movimento da rotação

:pushpin: **4** - Os satélites devem ser incluídos (3 satélites por planeta já é suficiente)

:pushpin: **5** - Os planetas e luas devem ter materiais diferentes

:pushpin: **6** - A nave deve ser controlada com WASD

### Desafio

:pushpin: **1** - Controlar a direção da nave com o mouse

:pushpin: **2** - Adicionar uma mecânica de aceleração para a nave 

:pushpin: **3** - Criar um “skybox” com as estrelas visíveis

:pushpin: **4** - Todos os componentes devem ser criados em escala

## Link

<br>

## Dicas de Acesso :arrow_forward:

<br>

### Anotações

- [x] Criar cada astro: Sol, Mercúrio, Vênus, Terra, Marte, Júpiter, Saturno, Urano, Netuno.
- [x] colocar materiais de cada astro
- [x] criar algoritmo dos astros rotação e adequar variaveis para cada
- [x] criar algoritmo dos astros translação e adequar variaveis para cada
- [x] Criar nave
- [x] Colocar efeito de luz no sol
- [x] criar script completo de movimentação com mouse da nave
- [x] Colocar skybox

<br>

### Sumário

:o: [Contribuidores](#contribuidores)

:o: [Tecnologias](#tecnologias)

:o: [Especificações](#especificações)

:o: [Link](#link)

:o: [Dicas de acesso](#dicas-de-acesso-arrow_forward)

:o: [Anotações](#anotações) -->

</div>
