using UnityEngine;
using System.Collections;

public class CrazyEffect : MonoBehaviour {

	public EllipsoidParticleEmitter effect;

	public void SetSize(float size){
		effect.minSize = size;
		effect.maxSize = size;
	}

	void Start () {
		Destroy(gameObject, 0.3f);
	}
}
