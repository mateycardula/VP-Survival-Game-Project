# VP-Survival-Game-Project

## Содржина
* [Општо](#општо)
* [Design Pattern](#design-pattern)
* [Inventory System](#inventory-system)
* [Destroyable Objects](#destroyable-objects)
* [Building System](#building-system)
* [Заклучок](#заклучок)

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
Како и кај сите класи, така наследувањето е возможно и кај _ScriptableObjects_. Во нашата игра од класата _ItemScriptableObject_ наследуваат каси како: _ConsumableScriptableObject_ (Објекти кои прават промена на глад и жед), _ResourceScriptableObject_ (Објекти кои се потребни за градење),  _ToolScriptableObject_ (Објекти со кои може да се гради или да се кршат објекти од околината).  

```csharp
[CreateAssetMenu(menuName = "Create Item/Consumable")]
public class ConsumableScriptableObject : ItemScriptableObject //Nasleduva od ItemScriptableObject
{
    public float hungerChange; //Promena na glad
    public float thirstChange; //Promena na zhed
}

```

```csharp
[CreateAssetMenu(menuName = "Create Item/Tool")]
public class ToolScriptableObject : ItemScriptableObject //Nasleduva od ItemScriptableObject
{
    //public float attackSpeed;
    public float damage; //Shteta koja ja nanesuvaat
    public bool isTwoHanded; //Dali se drzi so dve race
    public bool isBuildingTool; //Dali so alatkata moze da se gradi
}
```
### Поврзување на ScriptableObject со посакуван објект

За разлика од освновните _MonoBehaviour_ класи, класите кои наследуваат од _ScriptableObject_ **не можат** да бидат директна компонента на некој објект. Поради ова треба да се најде начин истите да се поврзат со посакуваниот тип или објект. Наше решение на овој проблем беше една „меѓукласа“ (MonoBehaviour) која е компонента на секој објект од даден тип и  чија единствена цел е да чува некој конкретен _ScriptableObject_.

Конкретно, секој објект за кој ни се потребни податоците од _ItemScriptableObject_ класата, има компонента класа _ISOHolder_ (ItemScriptableObjectHolder)

```csharp
public class ISOHolder : MonoBehaviour
{
    public ItemScriptableObject iso;
}
```
Ова е еден од начините на кои го направивме поврзувањето. За разлика од _ItemScriptableObject_, во нашата игра има и _DestroyableScriptableObjects_ (објекти кои се уништуваат и за тоа даваат некаква награда, пр. дрвата), поради тоа што за истите беше потребна посебна _Destroyable_ класа со поширока логика, променливата од типот _DestroyableScriptableObject_ се чува во неа:

```csharp
[CreateAssetMenu(menuName = "Destroyable Object")]
public class DestroyableScriptableObject : ScriptableObject
{
    public float health; 
    public GameObject brokenObject; //objekt koj treba da se instancira koja health<=0
    public List<ToolScriptableObject> WeaknessList; //Dokolku alatkata so koja se udira po objektot e vo weakness listata togas health-=tool.damage*2
    public List<ToolScriptableObject> StrenghtList; //Dokolku e vo strenght health-=tool.damage*2 
    public GameObject [] itemsToDrop; //Objekti koi gi frla kako nagradi (moze samo eden tip, no moze i povekje), se frlaat itemToDropCount objekti po slucaen izbor od listata 
    public int itemToDropCount;
}
```
Типот _DestroyableScriptableObject_ се чува и при инстанцирање на _Destroyable_ објект се вчитува health промелнивата за објектот кој се инстанцира.

```csharp
public class Destroyable : MonoBehaviour
{
    public float health;
    public DestroyableScriptableObject dso;
    void Start()
    {
        health = dso.health;
    }
.
.
.
}
```
На овој начин лесно може да се создаде кутија (чии податоци се чуваат во DestroyableScriptableObject) со health = 30 или дрво со health = 100, па дури и некое животно кое би читало податоци од _ScriptableObject_ класа која би наследувала од _DestroyableScriptableObject_.

### Пример инстанцирање на ConsumableScriptableObject

https://github.com/mateycardula/VP-Survival-Game-Project/assets/137711431/c13e4ab9-49f7-40a6-9c1b-e3e4e72a3ecd

## Inventory System
_Inventory_ системот главно е поделен на два дела. Првиот дел се двата слота коишто чуваат алатки (_ToolScriptableObject_) и дваесет слотови коишто ги чуваат останатите _ItemScriptableObjects_ (ова _inventory_ се отвора на _TAB_).

Нашето решение за овој систем беше полиморфизам преку кој ги добиваме двата посебни системи за чување на објекти. 

```csharp
public abstract class  Inventory : MonoBehaviour
{
    [SerializeField] public Slot[] slots; //Klasa vo koja cuvame podatoci za sekoj slot
    public int inventoryCapacity, itemCount; //maksimum golemina na inv, i broj na items vo inventory

    public abstract ItemScriptableObject CollectItem(ItemScriptableObject iso);
    public abstract ItemScriptableObject DropItem(int itemSlotID, bool shouldConsume = false);
    protected abstract void UpdateInterface(int id =-1);
}
```
Во двата типови на _inventory_ секое поле е објект од тип Slot:

```csharp
[SerializeField] public Slot[] slots; //Klasa vo koja cuvame podatoci za sekoj slot
```

```csharp
public class Slot
{
    public bool isEquipped; 
    public ItemScriptableObject tool; //ItemScriptableObject koj go cuvame vo poleto
    public int count; //Broj od objektot vo edno pole od inventory-to
    public Slot(ItemScriptableObject _tool, int _count = 1, bool _isEquipped = false)
    {
        isEquipped = _isEquipped;
        tool = _tool;
        count = _count;
    }

    public Slot() //Go koristime za instanciranje na prazno pole (pr. posle drop ili consume koga momentalniot count e 1)
    {
        isEquipped = false;
        tool = null;
        count = 0;
    }
}
```

#### Како изгледа Collect() методот кај ItemInventory?

```csharp
public override ItemScriptableObject CollectItem(ItemScriptableObject iso)
    {
       int id = FindFirstNonFullISOslot(iso); //go barame prviot slot od itemot koj sakame da se equip-ne, a da ne e negoviot count = stack
        bool collected = false;
        if (id == -1) //Dokolku ne sme nasle slot koj ne go dostignal ogranicuvanjeto, itemot se mesti na prvoto slobodno mesto
        {
            for (int i = 0; i < inventoryCapacity; i++)
            {
                if (slots[i].tool == null)
                {
                    slots[i] = new Slot(iso);
                    UpdateInterface(i);
                    collected = true;
                    break;
                }
            }
            if (!collected)
            {
                //TODO: Announce full inventory
            }
         }
        else //Dokolku sme nasle 
        {
            int count = slots[id].count;
            slots[id] = new Slot(iso,count+1); //Na mestoto od stariot slot cuvame nov objekt so count++
            UpdateInterface(id);
        }
        return iso;
    }
```

#### Од каде се повикува Collect()?

Методите на _inventory_ системите се повикуваат преку друга класа која што содржи метод кој служи за пуштање на [raycast](https://docs.unity3d.com/ScriptReference/Physics.Raycast.html) од координатниот почеток на камерата во права насока. 

Кога зракот има контакт со објект означен како _"Item"_ се активира [_Halo_](https://docs.unity3d.com/Manual/class-Halo.html) компонентата на објектот (жолтата светлина што индицира дека може да се собере објектот во кој што гледаме). Доколку се притисне _E_, се повикува CollectItem() од ToolInventory или ItemInventory соодветно. 

```csharp
private ToolInventory toolInventoryManager;
private ItemInventory itemInventoryManager;
.
.
if (hit.collider.CompareTag("Item"))
            {
                .
                .
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ItemScriptableObject hitItemISO = hitItem.GetComponent<ISOHolder>().iso; //od megjuklasata go zemame iso podatokot
                    if (hitItemISO is ToolScriptableObject)  //Dokolku e tool se povikuva collect na ToolInventory
                    {
                        toolInventoryManager.CollectItem(hitItemISO);
                        GameObject.Destroy(hitItem);
                    }
                    else //Inaku se povikuva Collect na ItemInventory
                    {
                        itemInventoryManager.CollectItem(hitItemISO);
                        GameObject.Destroy(hitItem);
                    }
                }
            }
```

## Destroyable Objects
Како што претходно кажавме, покрај _ItemScriptableObjects_, во нашата игра постојат и _DestroyableScriptableObjects_. Слично како и кај другите _ScriptableObjects_, податоците за овие ги чуваме во класата на конкретниот тип, т.е _Destroyable_.

```csharp
public class DestroyableScriptableObject : ScriptableObject
{
    public float health; 
    public GameObject brokenObject; //objekt koj treba da se instancira koja health<=0
    public List<ToolScriptableObject> WeaknessList; //Dokolku alatkata so koja se udira po objektot e vo weakness listata togas health-=tool.damage*2
    public List<ToolScriptableObject> StrenghtList; //Dokolku e vo strenght health-=tool.damage*2 
    public GameObject [] itemsToDrop; //Objekti koi gi frla kako nagradi (moze samo eden tip, no moze i povekje), se frlaat itemToDropCount objekti po slucaen izbor od listata 
    public int itemToDropCount;
}
```
Се поврзува со:

```csharp
public class Destroyable : MonoBehaviour
{
    public float health;
    public DestroyableScriptableObject dso;
    void Start()
    {
        health = dso.health; //pri instanciranje na objektot
    }
    public void SetHealth(float h, ToolScriptableObject tso = null);
    public void DropLoot();
    private void SpawnBrokenObject(Vector3 position);
}
```
Секојпат кога се нанесува штета врз објектот се повикува SetHealth(), функцијата, а во моментот кога health<=0, играчот ја добива наградата.

```csharp
public void SetHealth(float h, ToolScriptableObject tso = null)
    {
        if(tso!=null)
        {
            if (dso.WeaknessList.Contains(tso)) h *= 2; //Dokolku objektot e slab na itemot koj go udira (pr. drvo na sekira)
            if (dso.StrenghtList.Contains(tso)) h /= 3; 
        }
        health += h;
        if(health<=0) DropLoot();
    }
```

Убавината на DropLoot() функцијата е тоа што секој објект може да дава различни видови на награди по случаен избор. Тоа е така, поради тоа што самиот ScriptableObject содржи низа на објекти кои можат да се паднат како награда, а потоа за itemToDropCount итерации, избира случаен објект од листата кој се инстанцира на сцената.

```csharp
public void DropLoot()
    {
        Vector3 position = gameObject.transform.position;
        gameObject.SetActive(false); //najprvo se deaktivira objektot
        //dokolku namesto SetActive, na pocetokot napravime Destroy(gameObject) so toa bi prkeinala i funkcijata poradi toa sto e komponenta na toj objekt.
        
        GameObject [] lootItems = new GameObject[dso.itemToDropCount];

        for (int i = 0; i < dso.itemToDropCount; i++)
        {
            lootItems[i] = dso.itemsToDrop[Random.Range(0, dso.itemsToDrop.Length)]; //se izbira slucen item 
            lootItems[i].transform.position = transform.position + Vector3.up * Random.Range(2, 3); //se instancira vo pozicija nad koordinatniot pocetok od objektot koj se krsi
            Instantiate(lootItems[i]);
        }
        SpawnBrokenObject(position); //se mesti skrsen objekt
        Destroy(gameObject); //od scenata celosno se unistuva objektot za da ne zafakja memorija i resursi
    }
```
### Инстанцирање на јаболкница
Инстанцирање на објект јаболкница, со што во низата на објекти кои дрвото ги дава како награда, втор објект е јаболкото кое го инстанциравме во претходниот пример. 

https://github.com/mateycardula/VP-Survival-Game-Project/assets/137711431/b402d036-279f-4a11-84a5-0a09ccd0f820


## Building System

Системот за градење исто така се заснова на _ScriptableObjects_.

```csharp
public class BuldingScriptableObject : ScriptableObject
{
    [SerializeField] public GameObject building, canBuildIndicator; //dva objekti, edniot e onoj koj go prati kursorot, drugiot e onoj koj se postavuva na negovoto mesto
    [SerializeField] public ItemScriptableObject [] buildingMaterials;
    [SerializeField] public int[] countOfMaterials; 
    public bool isWall, isFloor; //dali objektot e horizontalno ili vertikalno orientiran   
}
```
Двете низи: 

```csharp
    [SerializeField] public ItemScriptableObject [] buildingMaterials;
    [SerializeField] public int[] countOfMaterials; 
```
Ни овозможуваат за секој објект кој се гради да се потребни повеќе видови на материјали (_ItemScriptableObjects_) во различни количини, при што индекс 0 од низата со материјали соодветствува со потребниот број од низата со целобројни променливи. 

За да се отклучи можноста за градење потребно е играчот во рака да држи _ToolScriptableObject_, чија што bool isBuildingTool; променлива е _true_. Моменталната верзија на играта како таков _building tool_ вклучува чекан. 

```csharp
[SerializeField] private BuldingScriptableObject bso;
.
.
if(bso != null){
  .
  .
  if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        if(EnoughMaterials()) //dokolku ima dovolno materijali za gradenje na objektot
                        {
                            BuildObject(); //objektot se gradi
                        }
                    }
}
```

```csharp
public void BuildObject()
    {
        for (int i = 0; i < bso.countOfMaterials.Length; i++)
        {
            int matCount = bso.countOfMaterials[i];
            ItemScriptableObject iso = bso.buildingMaterials[i];
            itemInventoryScript.ConsumeMultiple(iso, matCount);
        }
        bso.building.GetComponent<Transform>().position= indicator.transform.position;
        bso.building.GetComponent<Transform>().eulerAngles = indicator.transform.eulerAngles;
        Instantiate(bso.building);
    }
```
Овој метод, во зависност од бројот на **различни** материјали потребни за градење на еден објекt, толку пати го повикува _ConsumeMultiple()_ методот од претходно опишаната _ItemInventory_ класа. Всушност, за секој различен материјал _ConsumeMultiple()_ се повикува по еднаш (со агрументи од тип ItemScriptableObject и цел број кој го назначува бројот на инстанци од материјалот кои треба да се искористат).

```csharp
 public void ConsumeMultiple(ItemScriptableObject iso, int count)
    {
        int consumed = 0; //brojac za da ne se nadmine potrebniot broj na materijali
        
        for (int i = 0; i < inventoryCapacity; i++) //se iterira niz sekoj slot na inventory
        {
            if (slots[i].tool == iso) //koga kje najde slot koj go cuva potrebniot objekt
            {
                int limit = slots[i].count;
                for (int k = 0; k < limit; k++)
                {
                    Consume(i); //za negovoto id povikuva consume(), metodot koj se povikuva pri jadenje na hrana
                    consumed++;
                    if(consumed == count) return;
                }
            }
        }
        //Debug.Log("Consume Multiple");
    }
```
### Користење на материјали при градење
На следното видео, може да се забележи колку е лесно да се додаваат потребни материјали за изградба на еден објект. Во интерес на времето покрај четирите дрва потребни за изградба на под, додадовме и две јаболка :). Истиот систем може да се искористи и при создавање на _crafting system_. 

https://github.com/mateycardula/VP-Survival-Game-Project/assets/137711431/d8b7d0a3-132b-4435-8391-36bbe11dae27

## Заклучок
Развивајќи ја играта се водевме по TypeObject design pattern преку _ScriptableObjects_ во _Unity_. Ова ни овозможи да создадеме игра со висока скалабилност, па во моменталната верзија би можеле да додадеме уште десетици објекти кои се кршат, нови _ItemScriptableObjects_ кои би се добивале преку кршењето. Исто така можат да се додаваат и нови _ToolScriptableObjects_ чија ефикасност би варирала во зависност од објектот во којшто се удира. Од системот за градење, специфично искористувањето на повеќе видови на материјали во различен број за изградба на еден објект, лесно може да се пренамени за создавање на солиден _crafting system_. 




