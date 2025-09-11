# 🌌 Projeto Sistema de Partículas — Emissores e Comportamentos

## 👥 Equipe
- **Brenda Lessa Almeida**  
- **Jairo Augusto de Campos Alff**  

---

## 📝 Descrição do Projeto
Este projeto foi desenvolvido para **explorar a criação e controle de partículas** dentro do Unity, utilizando diferentes **tipos de emissores**, **comportamentos de movimento** e **critérios de evolução e morte**.

Através de uma simulação interativa, partículas são geradas em **tempo real**, permitindo que o usuário alterne entre:
- **Três tipos de emissores:** Ponto, Área e Círculo.  
- **Cinco comportamentos diferentes:** Linha reta, Espiral, Movimento aleatório, Explosão radial e Onda senoidal.  
- Mudanças visuais dinâmicas, como **cor, escala e transparência (fade)**.

> O objetivo é **compreender e testar conceitos essenciais de sistemas de partículas**, que são amplamente utilizados em jogos e efeitos visuais, como fumaça, fogo, magia, explosões e muito mais.

---

## 📂 Estrutura do Projeto
Principais scripts e suas funções:

| **Arquivo**            | **Descrição**                                                                                                         |
|------------------------|---------------------------------------------------------------------------------------------------------------------|
| `ParticleBehavior.cs`  | Controla a evolução individual de cada partícula, incluindo movimento, cor, escala, rotação e critérios de morte.   |
| `SimpleEmitter.cs`     | Gerencia os diferentes tipos de emissores e como as partículas são geradas na cena.                                 |
| `ParticleController.cs`| Controla a simulação geral, permitindo alternar emissores e comportamentos em tempo real.                           |

---

## ⚙️ Informações Técnicas
- **Engine:** Unity 2022.3.0f1  
- **Linguagem:** C#  
- **Dependências:** Nenhuma dependência externa, apenas scripts nativos do Unity  
- **Plataforma-alvo:** Windows e WebGL  

---

## ✅ Checklist de Requisitos
- [x] Sistema otimizado de partículas utilizando **object pooling**.  
- [x] **3 tipos de emissores**: Ponto, Área e Círculo.  
- [x] **5 tipos de comportamentos**: Linha reta, Espiral, Aleatório, Explosão e Onda senoidal.  
- [x] Partículas com mudança de **cor**, **escala** e **fade** ao longo da vida útil.  
- [x] Critérios de morte baseados em **tempo de vida** e **distância máxima percorrida**.  
- [x] Alternância entre emissores e comportamentos durante a execução.  

---

## 🔗 Link para a Build
Teste o sistema de partículas ou faça o download aqui:  
[**Itch.io - Projeto Sistema de Partículas**][(https://jairo-augusto.itch.io/projeto-sistema-particulas)](https://jairo-augusto.itch.io/projetos-animacao-computadorizada)

---

## 🎮 Como Funciona
1. **Escolha do emissor**  
   - Use as teclas **1**, **2** e **3** para alternar entre os emissores:
     - `1` → Emissor em **Ponto**.  
     - `2` → Emissor em **Área**.  
     - `3` → Emissor em **Círculo**.

2. **Troca de comportamento**  
   - Pressione **B** para alternar entre os cinco comportamentos de movimento disponíveis.

3. **Emissão contínua ou por burst**  
   - Pressione **C** para ligar/desligar a emissão contínua.  
   - Pressione **ESPAÇO** para emitir um burst instantâneo.

4. **Evolução visual**  
   - Cada partícula muda gradualmente de **cor**, **escala** e **opacidade** durante seu tempo de vida.

5. **Finalização da partícula**  
   - Quando a partícula atinge o tempo máximo de vida ou ultrapassa a distância limite, ela é desativada automaticamente e retorna ao pool.

---

## 💭 Comentários Finais
> Este projeto demonstra de forma prática como criar **sistemas de partículas personalizados** no Unity, sem depender exclusivamente do sistema nativo de partículas.  
> É uma base sólida para efeitos visuais mais complexos e pode ser expandido com:
> - Novos tipos de emissores (cone, espiral, etc.).  
> - Interações físicas com colisores.  
> - Diferentes texturas e sprites para partículas mais detalhadas.  
> - Efeitos avançados, como partículas com luzes ou som integrados.  
