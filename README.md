# Platformer-Shooter

Versión de subida: 1

**Explicación del Gameplay**

Se trata de un nivel de plataformas 2D de acción y aventuras. El personage tiene que cruzar el mapa hasta tocar un orbe, si muere en el camino tendrá que volver a empezar. Dispone de dos ataques diferentes: disparo y lanzamiento de objetos. Ambos ataques eliminan los enemigos de un golpe, pero el jugador también muere de un golpe. Los enemigos sólo tienen disparos horizontales, con la misma potencia que el jugador. Caer por un precipicio tambien causa la muerte.

Los controles son básicos: flechas LEFT y RIGHT para desplazarse, flecha UP para saltar (SPACE también se puede usar para saltar). Ataques: F disparo, E lanzamiento parabólico.

El juego se basa en dos escenas: 
- Main menu: menú principal (jugar o salir).
- Level-Particles: nivel jugable, desarrollo principal del juego

**Proceso del desarrollo**

Siguiendo el repositorio de la PEC2, se ha implementado las funcionalidades de disparos, inteligencias artificiales nuevas y todos los sistemas de partículas que dan un toque visual al juego.

_Main Menu_

Sin cambios en el desarrollo. Sólo se ha modificado la imagen de fondo y la música.

_Level Particles_

- Jugador: funcionalidades de disparo. Se ha añadido un nuevo estado: shoot, senecarga de ejecutar la animación de disparar, que desde la misma llama mediante un evento la función que detiene esta misma animación (motivos explicados en el script).
Ahora el jugador puede colisionar con el enemigo, sólo las balas le dañan. La muerte por caída sigue activa.
La recolección de monedas atorga puntos, pero no matar a un enemigo.

- Goblin (enemigo): Se desplaza de lado a lado en una distancia específica y mientras el juegador no entre en la zona de detección (también tiene una distancia determinada). Cuando el jugador entra en dicha zona, empieza a disparar en dirección al jugador.

- Balas: Los proyectiles tienen sus propias características. Las balas enemigas sólo impactan con el jugador y no en los otros enemigos. Las balas entre ellas no colisionan, al impactar con la pared, desaparecen. Los objetos que lanza el jugador no colisionan con las balas del jugador, pero si pueden desviar las de los enemigos y sí chocan con las paredes.
En cualquier caso de disparo, el desarrollo se trata de instanciar el prefab para cada bala en la posición de disparo.

Podéis ver el resultado en el siguiente link: 
