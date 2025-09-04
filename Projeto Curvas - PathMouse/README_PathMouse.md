# 🐭 Projeto Curvas e Movimento — Bezier vs Catmull-Rom

## 👥 Equipe
- **Brenda Lessa Almeida**  
- **Jairo Augusto de Campos Alff**  

---

## 📝 Descrição do Projeto
Este projeto foi desenvolvido para **testar a diferença entre duas curvas matemáticas** amplamente usadas em computação gráfica: **Bézier** e **Catmull-Rom**.  

Através de uma simulação interativa, um **ratinho 3D** se move ao longo da cena seguindo inicialmente a curva Catmull-Rom.  
Quando ele atinge certo ponto do percurso, ele muda automaticamente para a curva Bézier, permitindo observar visualmente a diferença no **comportamento do movimento e suavidade entre as duas curvas**.

> O objetivo é **comparar o comportamento das duas curvas** em tempo real e entender como elas afetam o trajeto de um objeto em movimento, algo bastante utilizado em animações, jogos e sistemas de interpolação.

---

## 📂 Estrutura do Projeto
Principais scripts e suas funções:

| **Arquivo**            | **Descrição**                                                                                                   |
|------------------------|-----------------------------------------------------------------------------------------------------------------|
| `BezierCurve.cs`       | Gera e desenha na cena uma curva Bézier a partir de 4 pontos de controle.                                       |
| `CatMullRomCurve.cs`   | Gera e desenha na cena uma curva Catmull-Rom a partir de uma lista de pontos.                                   |
| `RatFollower.cs`       | Controla o movimento do ratinho, alternando entre as curvas Bézier e Catmull-Rom ao chegar no final do trajeto. |

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
- [x] Alternância entre curvas em tempo real  

---

## 🔗 Link para a Build
Jogue online ou faça o download aqui:  
[**Itch.io - Nome do Jogo**](https://jairo-augusto.itch.io/teste-curvas-bezier-com-ratinho)

---

## 🎮 Como Funciona
1. **Curva Catmull-Rom gerada**  
   - O projeto inicia com o ratinho seguindo o trajeto gerado pela Catmull-Rom.

2. **Transição para a curva Bézier**  
   - Quando o ratinho chega ao final da primeira curva, ele automaticamente alterna para a curva Bézier.

3. **Alternância infinita**  
   - Esse ciclo se repete, permitindo comparação visual entre as curvas em movimento.

---

## 💭 Comentários Finais
> Este projeto serve como base para estudos sobre interpolação, animação e sistemas de movimento em jogos.  
> Ele pode ser expandido adicionando diferentes tipos de curvas, obstáculos, múltiplos personagens ou integração com IA de navegação (NavMesh).

