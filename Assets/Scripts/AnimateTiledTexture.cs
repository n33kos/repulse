using UnityEngine;
using System.Linq;
using System.Collections;

public class AnimateTiledTexture : MonoBehaviour
{
 
	public float fps = 10;
	public int currentAnimation = 0;
	public Sprite[] sprites;
	public string[] animations;

	private SpriteRenderer SR;
	private string[] ani;
	
	void Start () {
		SR = GetComponent<SpriteRenderer>();
		setAnimation(currentAnimation);
	}

	//Update
	void FixedUpdate () {
		SetSprite(sprites, fps);
	}

	//Set Sprite
	void SetSprite(Sprite[] sprites, float fps){

		// Calculate index
		int index  = (int)(Time.time * fps);

		// Repeat when exhausting all cells
		index = (int)Mathf.Floor(index % ani.Length);
		SR.sprite = sprites[int.Parse(ani[index])];
	}

	//Set Animation
	void setAnimation(int anim){
		ani = animations[anim].Split(',');
	}
}