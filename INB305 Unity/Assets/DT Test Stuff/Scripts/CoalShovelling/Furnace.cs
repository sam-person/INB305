using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour {

	// Fueling
	public float fuelAmount = 0.0f;
//	public float fuelUpRate = 2.0f;
//	public float fuelMax = 100f;
//	public float burnRate = 1.0f;
//	float burnAmount = 0.0f;
//	float maxBurnRate = 5f;

	// Light
	public Light furnaceLight;
	[Range(0,8)]
	public float maxLight;

	// Audio
	AudioSource[] audioSources;

	// Particles
	public ParticleSystem fireParticles;
	public Vector3 originalFireParticleBounds = new Vector3();
	ParticleSystem.ShapeModule fireShapeModule;
	Vector2 fireBoundsScale = new Vector2 (0.4f, 1f); // scale factor for the min and max bounds size respectively
	ParticleSystem.MainModule fireMainModule;
	Vector2 fireStartSize = new Vector2(0.7f, 1f); // range for start size (min, original)
	ParticleSystem.EmissionModule fireEmissionModule;
	Vector2 fireEmissions = new Vector2(3f, 10f);

	public ParticleSystem smokeParticles;
	ParticleSystem.MainModule smokeMainModule;
	Vector2 smokeStartLifeTime = new Vector2(4.5f, 7f); // (min, max)
	Vector2 smokeStartSize = new Vector2(0.3f ,0.7f);
	ParticleSystem.EmissionModule smokeEmissionModule;
	Vector2 smokeEmissions = new Vector2(1f, 3f);

	public ParticleSystem embersParticles;
	ParticleSystem.EmissionModule emberEmissionModule;
	Vector2 emberEmissions = new Vector2(1f, 3f);

	// Reference
	public FakeTank_Manager tankManager;

	void Start() {
		audioSources = GetComponents<AudioSource> ();

		// Initialising variables for fireParticles
		fireShapeModule = fireParticles.shape; // Shape
		originalFireParticleBounds = fireParticles.shape.box;

		fireMainModule = fireParticles.main; // Start Size
		fireStartSize.x = fireParticles.main.startSize.constant;

		fireEmissionModule = fireParticles.emission; // Emission Rate
		fireEmissions.y = fireParticles.emission.rateOverTime.constant;
		fireEmissions.x = Mathf.Floor(fireEmissions.y * 0.3f);

		// Initialising variables for smokeParticles
		smokeMainModule = smokeParticles.main; // Start Life Time
		smokeStartLifeTime.y = smokeMainModule.startLifetime.constant;
		smokeStartSize.y = smokeMainModule.startSize.constant; // Start Size

		smokeEmissionModule = smokeParticles.emission; // Emission Rate
		smokeEmissions.y = smokeEmissionModule.rateOverTime.constant;

		// Initialising variables for emberParticles
		emberEmissionModule = embersParticles.emission; // Emission Rate
		emberEmissions.y = emberEmissionModule.rateOverTime.constant;
	}

	void Update() {
//		burnAmount = burnRate * Time.deltaTime;
//		if (fuelAmount - burnAmount >= 0) {
//			tankManager.fuel += fuelUpRate * Time.deltaTime / 100f;
//		} else {
//			if (fuelAmount > 0) {
//				tankManager.fuel += (burnAmount - fuelAmount) * Time.deltaTime / 100f;
//			}
//		}
//		tankManager.fuel = Mathf.Clamp (tankManager.fuel, 0f, 1f);
//		if (tankManager.fuel >= 1)
//			fuelAmount -= maxBurnRate * Time.deltaTime;
//
//		fuelAmount = Mathf.Clamp (fuelAmount - burnAmount, 0f, 1000f);
		UpdateLight ();
		FurnaceAmbience ();

		// Particle updates
		ScaleFireParticles ();
		ScaleSmokeParticles ();
		ScaleEmbersParticles ();
	}


	public void AcceptFuel(float amount) {
		tankManager.fuel = Mathf.Clamp (tankManager.fuel + amount / 100f, 0f, 1f);
		fuelAmount = Mathf.Clamp (fuelAmount + amount, 0f, 1000f);
	}

	void UpdateLight(){
		furnaceLight.intensity = Mathf.Lerp (0, maxLight, tankManager.fuel);
	}

	void FurnaceAmbience() {
		audioSources [0].volume = tankManager.fuel / 8f + 0.05f; // Ambience
		audioSources [1].volume = tankManager.fuel / 14f + 0.05f; // fire crackling
	}

	void ScaleFireParticles() {
		// bounds size
		float lerpValue = 0f;
		lerpValue = LerpBasedOnFuel(fireBoundsScale);
		fireShapeModule.box = new Vector3(originalFireParticleBounds.x * lerpValue, originalFireParticleBounds.y * lerpValue, originalFireParticleBounds.z);

		// start size
		fireMainModule.startSize = LerpBasedOnFuel(fireStartSize);

		// emissions
		fireEmissionModule.rateOverTime = LerpBasedOnFuel(fireEmissions);

	}

	void ScaleSmokeParticles() {
		// start lifetime
		smokeMainModule.startLifetime = LerpBasedOnFuel(smokeStartLifeTime);

		// start size
		smokeMainModule.startSize = LerpBasedOnFuel(smokeStartSize);

		// emission
		smokeEmissionModule.rateOverTime = LerpBasedOnFuel(smokeEmissions);
	}

	void ScaleEmbersParticles() {
		// emission
		emberEmissionModule.rateOverTime = LerpBasedOnFuel(emberEmissions);

	}

	float LerpBasedOnFuel(Vector2 ranges) {
		return Mathf.Lerp(ranges.x, ranges.y, tankManager.fuel);
	}
}
