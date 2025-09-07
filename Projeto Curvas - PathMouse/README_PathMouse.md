# 🐭 Projeto Curvas e Movimento — Bezier vs Catmull-Rom

## 👥 Equipe
- **Brenda Lessa Almeida**  
- **Jairo Augusto de Campos Alff**  

---

## 📝 Descrição do Projeto
Este projeto foi desenvolvido para **testar a diferença entre duas curvas matemáticas** amplamente usadas em computação gráfica: **Bézier** e **Catmull-Rom**.  

Através de uma simulação interativa, um ratinho 3D se move ao longo da cena seguindo inicialmente a curva Bézier. 
Ao pressionar a TECLA ESPAÇO, o ratinho muda para a curva Catmull-Rom, permitindo observar visualmente a diferença na suavidade, forma e **comportamento do movimento entre as duas curvas.**.

> O objetivo é **comparar o comportamento das duas curvas** e entender como elas afetam o trajeto de um objeto em movimento, algo bastante utilizado em animações, jogos e sistemas de interpolação.

---

## 📂 Estrutura do Projeto
Principais scripts e suas funções:

| **Arquivo**            | **Descrição**                                                                                                                  |
|------------------------|--------------------------------------------------------------------------------------------------------------------------------|
| `BezierCurve.cs`       | Gera e desenha na cena uma curva Bézier, a partir de 6 pontos, onde o rato se move em loop.                                    |
| `CatMullRomCurve.cs`   | Gera e desenha na cena uma curva Catmull-Rom, a partir de 6 pontos, onde o rato se move em loop.                               |
| `RatFollower.cs`       | Controla o movimento do ratinho, iniciando pela Bézier e permitindo alternância para Catmull-Rom ao pressionar a TECLA ESPAÇO. |

---

## ⚙️ Informações Técnicas
- **Engine:** Unity 2022.3.0f1  
- **Linguagem:** C#  
- **Dependências:** Nenhuma dependência externa, apenas scripts nativos do Unity  
- **Plataforma-alvo:** Windows e WebGL  

---

## ✅ Checklist de Requisitos
- [x] Sistema de movimentação do personagem (ratinho)  
- [x] Geração dinâmica de curva Bézier  
- [x] Geração dinâmica de curva Catmull-Rom  
- [x] Alternância entre curvas ao pressionar a TECLA ESPAÇO

---

## 🔗 Link para a Build
Jogue online ou faça o download aqui:  
[**Itch.io - Projeto Curvas e Movimento — Bezier vs Catmull-Rom**](https://jairo-augusto.itch.io/teste-curvas-bezier-com-ratinho)

---

## 🎮 Como Funciona
1. **Início na curva Bézier**  
   - O projeto inicia com o ratinho seguindo a curva Bézier em um ciclo contínuo e suave, sem teleporte.

2. **Transição para a curva Bézier**  
   - Ao apertar a TECLA ESPAÇO, o ratinho troca para a curva Catmull-Rom.

3. **Alternância infinita**  
   - O usuário pode alternar quantas vezes quiser entre as curvas para observar como cada uma afeta a suavidade e forma do movimento.

---

## 💭 Comentários Finais
> Este projeto serve como base para estudos sobre interpolação, animação e sistemas de movimento em jogos.  
> Ele pode ser expandido adicionando diferentes tipos de curvas, obstáculos, múltiplos personagens ou integração com IA de navegação (NavMesh).

