# Platformer-Shooter

In case you get the "Window layout error" on loading unity project, follow the instructions in this video: https://www.youtube.com/watch?v=hGukVu1DR18&ab_channel=GameTrick

Upload version: 1

**gameplay explanation**

It is a 2D platform, action and adventures game. Player has go throught the map till it gets the final orb without getting killed, if it does, it must start again. Main character has two different attacks: shooting and throwing objects. Both can kill an enemy with a single shot, but player also can get killed by one enemy shot. Enemies can only shoot horitzontal bullets, with the same power as the player's character. Falling off of cliff also cause death.

Controls are basic: LEFT and RIGHT to run, UP to jump (SPACE also allowed to jump). F to shoot and E to throw.

Game is about two scene:
- Main menu: buttons to start game or quit.
- Level scene: main gameplay is developed.

**Development process**

Following the "Platformer" previous repository (https://github.com/johhaanndev/Platformer), it has been added the shooting features, new AI and all particles system that gives a visual effect.

_Main Menu_

No changes, only image and music have been changed.

_Level Scene_

- Player: shooting features. A new state has been added to the FSM: shoot, it plays the shooting animation, which also calls, by an animation event, another method that stops this animation (reason explained in the script).
Now, player can collide with enemies, only bullets can kill it. Death by falling is still activated.
Coins collected increases the score, but killing an enemies don't.

- Goblin (enemy): it moves side to side withing limits while player does not enter the detection zone. As soon as player enters, goblin starts shooting in the player's direction.

- Bullets: each projectile has its own characteristics. Enemy bullets collide with player, but not with other enemies. Player bullets and enemy bullets don't collide with each other, when any of these bullets hits a wall, they disappear. A thrown object does not collide with player bullets, but they can deflect enemy's and can touch a wall without disappearing.
In any case, the development is about instantiating the bullet prefab in the firing point position.

Check out the result in this video: https://www.youtube.com/watch?v=L4s1zhhjtaQ&ab_channel=JohhaannDev
