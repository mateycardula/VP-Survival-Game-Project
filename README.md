# VP-Survival-Game-Project

## Содржина
* [Општо](#општо)
* [Design pattern](#design-pattern)

## Општо
FPS Survival игра направена во [Unity Game Engine](https://unity.com/) создадена за целите на проектот од предметот Визуелно Програмирање на [ФИНКИ](https://www.finki.ukim.mk/mk). 
  
  Моменталната верзија на играта нуди можности како собирање на ресурси, нивно користење во изградба на објекти, конзумација на храна и вода и нивно чување во _inventory_.
  
### Тек на играње

https://github.com/mateycardula/VP-Survival-Game-Project/assets/137711431/9e414afd-6cb9-41d6-b5ba-884bc0bc7d57

## Design pattern

При развојот на играта генерално се водевме по [Type Object Design Pattern](https://gameprogrammingpatterns.com/type-object.html) кој ни овозможи лесно чување и модифицирање на податоци за различни типови и поврзување со логиката на истите. 

Во Unity ова е овозможено со користење на [_Scriptable objects_](https://gamedevbeginner.com/scriptable-objects-in-unity/#what_is_a_scriptable_object). Податоците на објектите се чуваат во класа која наследува од класата на Unity, _ScriptableObject_. При развој на видео игри во _Unity_, најчесто скриптите се класи кои наследуваат од класата [MonoBehaviour](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) и за истите да работат треба да се [компонента](https://www.javatpoint.com/unity-components) на некој постоечки објект во сцената. 

Еденa од класите која служи за инстанцирање на ScriptableObjects кои ги користевме при развојот на играта е **ItemScriptableObject**:

```csharp
[CreateAssetMenu(menuName = "Create Item")] //Ovozmozuva kreiranje na instanci od klasata preku interface na Unity
public class ItemScriptableObject : ScriptableObject
{
    public int stack; //Maksimalen broj na items vo edno inventory pole
    public string itemName; //Ime na item
    public string description; //Opis na item
    public GameObject model; //Gameobject: 3D model i komponenti potrebni za pravilna rabota
    public Sprite inventorySprite; //SLika za inventory
    public AnimationClip OnLeftClickAnimationClip; //Animacija koja se stratuva pri pritiskanje na lev klik
}
```
На секој тип на објект кој може да се собере и да се чува во _inventory_ му ги доделуваме овие податоци.

### Наследување кај ScriptableObjects
Како и кај секоја од класа, така наследувањето е возможно и кај _ScriptableObjects_. Во нашата игра од класата _ItemScriptableObject_ наследуваат каси како: _ConsumableScriptableObject_ (Објекти кои прават промена на глад и жед), _ResourceScriptableObject_ (Објекти кои се потребни за градење),  _ToolScriptableObject_ (Објекти со кои може да се гради или да се кршат објекти од околината).  

```csharp
[CreateAssetMenu(menuName = "Create Item/Consumable")]
public class ConsumableScriptableObject : ItemScriptableObject //Nasleduva od ItemScriptableObject
{
    public float hungerChange; //Promena na glad
    public float thirstChange; //Promena na zhed
}

```

За разлика од освновните _MonoBehaviour_ класи, класите кои наследуваат од _ScriptableObject_ **не можат** да бидат директна компонента на некој објект. Поради ова треба да се најде начин истите да се поврзат со посакуваниот тип или објект. Наше решение на овој проблем беше една „меѓукласа“ (MonoBehaviour) која е компонента на секој објект од даден тип и  чија единствена цел е да чува некој конкретен _ScriptableObject_.

