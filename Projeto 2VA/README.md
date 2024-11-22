Game Design Document

Versão: 1.0

TÍTULO DO JOGO

Autores:

Christian Oliveira

Recife, 2024

1. História

- Descrição detalhada da história (lembre-se que toda história deve conter um começo,

meio e fim);
Em um mundo pós-apocalíptico mergulhado no caos dimensional, portais misteriosos abriram caminhos para labirintos instáveis que conectam realidades diferentes. O jogador assume o papel de **Eliot**, um explorador que busca desvendar os segredos desses portais enquanto enfrenta criaturas geradas pelo colapso entre dimensões. A história começa com Eliot sendo sugado para o primeiro labirinto, sem memórias de como chegou ali. Ele deve lutar contra inimigos, superar armadilhas e resolver enigmas para escapar e desvendar a verdade.

- A descrição da história deve conter uma breve descrição do ambiente onde o jogo

acontece e também dos principais personagens envolvidos na história;

O jogo se passa em uma série de labirintos flutuantes de design fractal e geometria surreal, repletos de corredores iluminados por cristais de energia.

2. Gameplay

- Descrição da mecânica do jogo;

> Puzzles são resolvidos usando os controles excêntricos do celular. Inimigos são enfrentados no estilo dungeon crawler, com visão frontal fixa. O plano de visão pode ser alterado com o giroscópio, e a câmera pode girar limitadamente no plano atual usando a marcha direita do celular. Inimigos surgem em todas as direções (cima, baixo, esquerda, direita, frente, trás), com indicadores aparecendo no cubo wireframe na HUD do canto direito. O jogador pode atacar, defender-se e fugir dos inimigos correndo para fora de seu alcance. Ao derrotar inimigos, eles dropam XP e Ouro, que podem ser coletados. Cada vitória é seguida por uma cinemática com texto mostrando o triunfo e os recursos obtidos. Nesta versão, as funcionalidades de Ouro e XP não serão implementadas além da acumulação, mantendo o jogador no nível 1.
> 
- Quais são os desafios encontrados pelo jogador e quais os métodos usados para superá-

los?

**Combate**: Defender e atacar utilizando os botões e giroscópio para alinhar os ataques.

**Fuga**: Correr ajustando o plano de visão e usando botões repetidamente para aumentar a velocidade.

**Puzzles**: Resolver quebra-cabeças alterando a orientação do celular, como alinhar símbolos girando o eixo Y.

- Como o jogador avança no jogo e como os desafios ficam mais difíceis?

O jogador avança completando cada nível do labirinto. Cada nível é um conjunto de salas conectadas, com inimigos e puzzles que bloqueiam o progresso.

- Os desafios aumentam conforme o jogador progride, incluindo:
    - **Inimigos mais fortes e numerosos**, com padrões de ataque mais complexos.
    - **Puzzles mais elaborados**, exigindo maior uso do giroscópio e mecânicas combinadas.
    - **Gestão de recursos**, como stamina, visão (AR) e tempo limitado para escapar de armadilhas.
- Como o gameplay está relacionado com a história? O jogador deve resolver quebra-

cabeças para avançar na história? Ou deve vencer chefões para progredir?

O progresso na história é desbloqueado ao resolver quebra-cabeças e vencer inimigos importantes, chamados **Guardas Dimensionais**.

- **Quebra-cabeças**: Revelam informações sobre a trama e abrem passagens para novos labirintos.
- **Chefões**: Cada "Guarda Dimensional" derrotado libera fragmentos de memória para Eliot, ajudando-o a entender sua missão e quem está controlando os portais.
- Qual é a condição de vitória? Salvar o universo? Matar todos os inimigos? Coletar 100

estrelas? Todas as alternativas acima?

Completar todos os níveis do labirinto e derrotar o **Guardião Final**, restaurando o equilíbrio dimensional.

Opcionalmente, coletar todos os cristais dimensionais para desbloquear um final alternativo.

- Qual é a condição de derrota? Perder 3 vidas? Ficar sem energia?

3. Personagens

**Perder 3 vidas** (representadas pelos corações no HUD). Cada ataque inimigo ou falha em armadilhas reduz um coração.

**Ficar sem energia (stamina)** em momentos cruciais pode impedir o jogador de atacar ou defender-se.

Descrição das características dos personagens principais (nome, idade, tipo...);

**Eliot**: O protagonista, hamster gordinho (PSX ou pixel art) equipado com espada e escudo que anda pulando.

**Íris**: Uma inteligência artificial que orienta Eliot com mensagens enigmáticas.

**O Guardião**: Um vovô misterioso e meio desleixado que aparece em cutscenes, aparentemente observando Eliot, e que na verdade está atrapalhando.

- História do passado dos personagens;

**Eliot**: Um hamster de laboratório modificado geneticamente durante experimentos dimensionais. Ele foi projetado para explorar ambientes instáveis, mas acabou ganhando consciência e escapou, apenas para ser sugado pelos portais.

**Íris**: Originalmente, um sistema de IA criado para guiar exploradores nos labirintos. Ela foi corrompida por energias dimensionais, adquirindo uma personalidade sarcástica e tendenciosa.

**O Guardião**: Era um cientista brilhante que liderava os experimentos dimensionais. Após o colapso, ele foi preso entre dimensões e se tornou parcialmente insano.

- Personalidade dos personagens;

**Eliot**: Determinado, curioso e cheio de energia, com traços de ingenuidade devido à sua origem artificial.

**Íris**: Sarcástica, enigmática e frequentemente irritante, mas no fundo tem um objetivo oculto que pode ou não beneficiar Eliot.

**O Guardião**: Misterioso, excêntrico e muitas vezes incoerente, com momentos de clareza onde demonstra uma ligação emocional com Eliot.

- Ações que os personagens podem executar (andar, correr, pular, pulo duplo, escalar, voar,

nadar...);

Elliot: anda, corre, sobe, desce, vira para trás e para os lados, ataca, defende e interage com puzzles.

4. Controles

- Como o jogador controla o personagem principal?

Usa o celular na orientação paisagem fixada como controle/joystick conectado pela mesma rede localhost e o computador como tela.

Left Stick: mover o personagem no eixo X e Z - no plano do chão.

Right Stick: quando em 3º pessoa gira a câmera fixamente apenas em relação as costas do personagem, e gira completamente  em torno do objeto de enigma quando em 1º pessoa.       

Pitch: ao rotacionar o X para trás (anti-horário) muda o personagem para trás (180º) já rotacionar o X para frente não faz nada.

Yaw: ao rotacionar o Y para trás (sentido lado esquerdo) move o personagem para o lado esquerdo do personagem em 90º e para a frente (seria como mover para trás a partir do lado direito do celular) move o personagem para o lado direito do personagem em 90º.

Roll: Ao rotacionar o Z par esquerda (sentido de volante) desce para o local abaixo do personagem, ao rotacionar para direita sobe em relação ao personagem.

Camera: Ao apertar no quadrado preto no centro do celular ativa a camera, ao apertar na camera desativa ela, retornando o quadrado preto.

Xis: Interage com objetos e com o mapa (ex: pula onde necessário). 

Triangle: termina interações e fecha janelas, ao pressionar repetidamente faz o personagem correr com velocidade baseada na repetição e intensidade ao apertar. 

Square - Ataca com espada e gasta stamina. 

Circle - Defende com o escudo - segurar mantém defesa mais gasta stamina.

disponíveis;

Start - Abre o menu do jogo (pausa)

Select -  Alterna modos de HUD - Aumenta tamanho do mini mapa na tela do PC, exibe mapa no lugar do quadrado da camera  no celular, desativa toda hud do PC.

5. Câmera

- Como é a câmera do jogo? Como o jogador visualiza o jogo?

A câmera do PC é fixa em 3ª pessoa - em 1ª pessoa durante resolução de enigmas.

A câmera do celular é fixa em 1ª pessoa

A câmera do celular possui AR/XR durante a resolução de enigmas

Ao ativar camera do celular a camera em 1ª pessoa do PC é ativada, essa última recebendo efeitos visuais, além disso a camera pode ser controlada com o touchscreen e gestos - a movimentação fica  impossibilitada.

6. Universo do Jogo

- Qual a estrutura do mundo?

**Labirinto de Cristais**: Geometria simétrica e música calma.

**Labirinto da Sombra**: Corredores escuros com geometria não euclidiana e música tensa.

**Labirinto do Colapso**: Estruturas em queda e música acelerada.

- Qual a emoção presente em cada ambiente?

**Cristais**: Um senso de curiosidade e descoberta.

**Sombra**: Medo e mistério.

**Colapso**: Caos e desespero.

- Que tipo de música deve ser usada em cada fase?

**Cristais**: Instrumentos leves e eletrônicos, como harpas sintetizadas.

**Sombra**: Tons graves e ecos, com batidas intermitentes.

**Colapso**: Ritmo frenético, com percussões e cordas intensas.

7. Inimigos

- Em qual ambiente/fase cada inimigo vai aparecer?

**Cristais**: Criaturas luminescentes de movimento lento.

**Sombra**: Inimigos invisíveis que só aparecem em reflexos de cristais.

**Colapso**: Golems de rocha que desmoronam ao atacar.

- O que o jogador ganha ao derrotar cada inimigo?

**XP**: Para medir progresso (versões futuras).

**Cristais de energia**: Usados para resolver puzzles ou desbloquear passagens.

- Qual o comportamento e habilidades de cada inimigo?

**Cristais**: Andam em padrões previsíveis, atacam ao contato direto.

**Sombra**: Aparecem de repente para emboscar o jogador.

**Colapso**: Lançam fragmentos de pedra à distância e se aproximam lentamente.

8. Interface

- Design e ilustração do HUD (head-up display);

Barra de vida

Barra de estamina

Cubo wireframe com eixos de direções globais, o plano da visão do jogador fica piscando

Mini Mapa 3D ilustrativo mostrando blocos de todos os níveis de altura e de distancia dentro de um raio pré-determinado em relação ao personagem - incluindo portas, esconderijos e safe rooms por onde o jogador já passou.

Left Stick, Right Stick, triangle, square, circle, xis, select, start, camera.

- Posicionamento dos elementos do HUD;

Barra de vida no canto inferior esquerdo

Barra de estamina no canto inferior esquerdo em seguida a barra de vida

Cubo no canto inferior direito.

Mini Mapa 3D no canto superior direito e também no celular (canto superior direito ou no centro no lugar da camera).

Left Stick canto esquerdo inferior

Right Stick: canto direito inferior (mais para esquerda e inferior)

Circle, triangle, xis, square: formato playstation

camera: centro - modo desativada (espaço preto) e ativada (camera XR)

select, start: embaixo da camera.