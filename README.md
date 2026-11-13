<div align="center">
  <img src="https://capsule-render.vercel.app/api?type=waving&color=0%3A1C1917%2C50%3AB91C1C%2C100%3AFCA5A5&height=200&section=header&text=Vehicle+Basics&fontSize=60&fontColor=fff&fontAlignY=38&desc=Conducción+con+físicas+en+Unity+y+C%23&descAlignY=62&descSize=18" width="100%"/>
</div>

<div align="center">
  <a href="https://github.com/evagr11/Vehicle-basics" target="_blank"><img src="https://img.shields.io/badge/Ver_Código-B91C1C?style=for-the-badge&logo=github&logoColor=white" alt="Ver Código"></a>
  <a href="https://evagr11.github.io/Vehicle-basics/" target="_blank"><img src="https://img.shields.io/badge/Documentación_(Doxygen)-B91C1C?style=for-the-badge&logo=doxygen&logoColor=white" alt="Documentación del código"></a>
</div>

<div align="center">
  <a href="https://git.io/typing-svg"><img src="https://readme-typing-svg.demolab.com/?font=JetBrains+Mono&size=18&duration=3500&pause=800&color=DC2626&center=true&vCenter=true&width=600&lines=Movimiento+con+Rigidbody+y+fuerzas%3BFreno+de+mano+con+derrapes%3BC%C3%A1mara+que+sigue+al+jugador%3BEventos%2C+corrutinas+y+AnimationCurves" alt="Typing SVG" /></a>
</div>

# Sobre el Proyecto

**Vehicle Basics** es un pequeño juego en 3D hecho con **Unity** en el que controlas una mariquita por un circuito cerrado. El objetivo es recoger las flores que van apareciendo mientras esquivas los obstáculos que suben y bajan sin parar: cada flor suma puntos y, si chocas con un obstáculo, pierdes puntos y el nivel vuelve a empezar.

Todo el movimiento está hecho a mano con **físicas** (`Rigidbody`), sin usar `WheelCollider`: aceleración, rozamiento, freno, marcha atrás y un freno de mano que deja marcas de derrape.

**Características principales:**

*   **Conducción con físicas:** Aceleración limitada por una velocidad máxima, giro con `AddTorque`, rozamiento que frena el vehículo poco a poco al soltar el acelerador y freno que pasa a marcha atrás cuando ya estás parado.
*   **Freno de mano y derrapes:** Con la barra espaciadora el vehículo frena en seco, gira más rápido y activa varios `TrailRenderer` que dibujan las marcas en el suelo.
*   **Cámara de seguimiento:** Se coloca detrás del jugador con `SmoothDamp`, mira un poco por delante de él y nunca se acerca más de una distancia mínima.
*   **Coleccionables:** Las flores giran sobre sí mismas y aparecen en un punto aleatorio del circuito, sin repetir el anterior. Al recoger una aparece la siguiente.
*   **Obstáculos animados:** Suben y bajan entre dos puntos siguiendo una `AnimationCurve`, así que cada uno puede tener su propio ritmo.
*   **Eventos del juego:** Una clase estática `GameEvents` con `UnityEvent` comunica las flores y los obstáculos con el marcador sin que se conozcan entre sí.
*   **Marcador animado:** Los puntos aparecen y desaparecen con un fundido hecho con una corrutina y otra `AnimationCurve`.

### Controles

<div align="center">

| Tecla | Acción |
| :---: | :----- |
| `W` | Acelerar |
| `S` | Frenar / marcha atrás |
| `A` · `D` | Girar a la izquierda / derecha |
| `Espacio` | Freno de mano (derrape) |

</div>

# Contexto del Proyecto

Este proyecto se desarrolló durante el ciclo formativo de **Desarrollo de Aplicaciones Multiplataforma (DAM)** como práctica de introducción a Unity. El objetivo era entender lo básico de un videojuego en 3D: mover un objeto con físicas en `Update` / `FixedUpdate`, detectar colisiones y triggers, hacer que una cámara siga al jugador y comunicar objetos entre sí con eventos.

# Stack Tecnológico

<div align="center">
  <img src="https://img.shields.io/badge/Unity-000000?style=for-the-badge&logo=unity&logoColor=white" alt="Unity">
  <img src="https://img.shields.io/badge/C%23-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="C#">
  <img src="https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white" alt="Visual Studio">
  <img src="https://img.shields.io/badge/Status-Terminado-DC2626?style=for-the-badge" alt="Status Terminado">
</div>

<br>

<div align="center">

| Componente | Tecnología |
| :--------- | :--------- |
| **Motor** | `Unity 2023.2.20f1` |
| **Lenguaje** | `C#` |
| **Físicas** | `Rigidbody`, `AddTorque`, colisiones y triggers |
| **Efectos** | `TrailRenderer` para los derrapes |
| **Interfaz** | `TextMeshPro` en un `Canvas` |
| **Animación por código** | `AnimationCurve`, corrutinas y `Vector3.SmoothDamp` |

</div>

# Estructura del Proyecto

```bash
Vehicle-basics/
├── Assets/
│   ├── Scenes/
│   │   └── SampleScene.unity          # Circuito con paredes, rampa, obstáculos y marcador
│   ├── Scripts/
│   │   ├── Movimiento.cs              # Conducción: aceleración, freno, giro y freno de mano
│   │   ├── CamaraFollow.cs            # Cámara que sigue al jugador
│   │   ├── CollectableSpawner.cs      # Elige dónde aparece la siguiente flor
│   │   ├── CollectibleBehaviour.cs    # Rotación de la flor y recogida
│   │   ├── Obstacles.cs               # Obstáculos que suben y bajan; reinician el nivel
│   │   ├── GameEvents.cs              # Eventos CollectibleEarned y ObstacleHit
│   │   └── ScoreDisplayAnimator.cs    # Puntuación y fundido del marcador
│   ├── Prefabs/                       # Flores coleccionables
│   ├── Materiales/
│   ├── Lowpoly Flowers/               # Modelos 3D de las flores
│   └── cocinelle.fbx                  # Modelo 3D de la mariquita
├── Packages/
└── ProjectSettings/
```

# Cómo Utilizar el Proyecto

Necesitas **Unity Hub** y la versión **Unity 2023.2.20f1** (o una más nueva, aceptando la actualización del proyecto).

1.  **Clona el repositorio:**
    ```bash
    git clone https://github.com/evagr11/Vehicle-basics.git
    ```
2.  **Ábrelo desde Unity Hub:** `Add` > `Add project from disk` y selecciona la carpeta `Vehicle-basics`. La primera vez tarda un poco porque Unity genera la carpeta `Library`.
3.  **Abre la escena** `Assets/Scenes/SampleScene.unity`.
4.  **Pulsa Play** y conduce con `W` `A` `S` `D`.

<br>

<div align="center">
  <p>Creado por <strong>Eva Gallardo</strong></p>
  <a href="https://github.com/evagr11" target="_blank">
    <img src="https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white" alt="GitHub">
  </a>
  <a href="https://www.linkedin.com/in/eva-gallardo-romero-830362349/" target="_blank">
    <img src="https://img.shields.io/badge/LinkedIn-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white" alt="LinkedIn">
  </a>
</div>

<div align="center">
  <img src="https://capsule-render.vercel.app/api?type=waving&color=0%3A1C1917%2C50%3AB91C1C%2C100%3AFCA5A5&height=120&section=footer" width="100%"/>
</div>
