# Hevadea game project features list
## world
 - [ ] Villages
 - [ ] Dungeons
 - [ ] Plants
 - [ ] Minerais

## Combat system
### Weapons
  - melee
   - Axe : Slow with hight damages 
   - Swords : Fast with lower damages
  - range
   - bow
   - magic wands: same as bow but with hight recharge delay + effect

### Effect
#### Negatives
 - Poison
 - blindness
 - weakness : no critical damages
 - Slowness

#### Positives
 - Regeneration
 - Owl vision : night vision
 - strenght : more critical damages
 - Speed
 - Fish feet : move fast on and under water.

## Creatures
 - Zombie
 - Skeleton
 - Gost
 - Slime

# Multiplayer feature list

## Client -> server packets

### login|string:playerName|string:gameinfoJson
- *playerName* : nom du joueur (string)
- *gameinfo* : un string json contenant les information du client (version)
> Connection a une serveur pour la primiere fois. 
> Le server envera ensuit un token de connection pour les autres fois ;

### LoginToken|string:playerName|int:Token|string:gameinfoJson
- *playerName* : nom du joueur (string)
- *token*: jeton unique du joueur donner par le server lors de la premiere connection avec le packet **token** (int)
- *gameinfo* : un string json contenant les information du client (version)
> Connection a un server 
> avec jeton utilisateur qui identifira le joueur de maniere unique.

PlayerMove|float:vx|float:vy

chat|string:message;
Disconnect;

PlayerAttack;
PlayerTake;
playerInteract;
playerDrop;


EntityGet|int:Ueid
TileGet|int:level|int:tx|int:ty


### Server -> client packet

*privates packet*
> these packet are only send to one client

token|int:token
kick|string:raison;
ban|raison;

*broadcasted packet*
> these packet are send to every one.

EntityMove|int:Ueid|float:vx|float:vy
EntityUpdate|int:Ueid|string:entityDataJson
EntityRemove|Ueid
EntityAdded|Ueid|int:level|int:x|int:y|string:entityDataJson
EntityPosition|Ueid|float:x|float:y : Randomly sended to the client to sync entity position

tile|int:level|int:tx|int:ty : Update a tile to a other tile
tiledata|int:level|int:tx|int:ty|string:TileDataJson : A data value change for a tile 
