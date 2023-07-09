using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public List<NamedSprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public NamedSprite GetSprite(string name)
    {
        return sprites.Find(sprite => sprite.name == name);
    }
}
