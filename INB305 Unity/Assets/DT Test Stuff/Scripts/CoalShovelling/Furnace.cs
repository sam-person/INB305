using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour {

	// Fueling
	public float fuelAmount = 0.0f;

	// Light
	public Light furnaceLight;
	[Range(0,8)]
	public float maxLight;

	// Audio
	AudioSource[] audioSources;

	// Particles Systems, module references & setting bounds
	public ParticleSystem fireParticles;
	Vector3 originalFireParticleBounds = new Vector3();
	ParticleSystem.ShapeModule fireShapeModule;
	Vector2 fireBoundsScale = new Vector2 (0.2f, 1f); // scale factor for the min and max bounds size respectively
	ParticleSystem.MainModule fireMainModule;
	Vector2 fireStartSize = new Vector2(0.2f, 1f); // range for start size (min, original)
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
		fireStartSize.y = fireParticles.main.startSize.constant;

		fireEmissionModule = fireParticles.emission; // Emission Rate
		fireEmissions.y = fireParticles.emission.rateOverTime.constant;
		fireEmissions.x = Mathf.Floor(fireEmissions.y * 0.3f);

		// Initialising variables for smokeParticles
		//smokeMainModule = smokeParticles.main; // Start Life Time
		//smokeStartLifeTime.y = smokeMainModule.startLifetime.constant;
		//smokeStartSize.y = smokeMainModule.startSize.constant; // Start Size

		//smokeEmissionModule = smokeParticles.emission; // Emission Rate
		//smokeEmissions.y = smokeEmissionModule.rateOverTime.constant;

		// Initialising variables for emberParticles
		emberEmissionModule = embersParticles.emission; // Emission Rate
		emberEmissions.y = emberEmissionModule.rateOverTime.constant;
	}

	void Update() {
		UpdateLight (); // Update overall furnace light
		FurnaceAmbience (); // Update ambient furnace sounds

		// Particle updates
		ScaleFireParticles ();
		ScaleSmokeParticles ();
		ScaleEmbersParticles ();
	}

	/// <summary>
	/// Add fuel to the furnace in FakeTank Manager
	/// </summary>
	/// <param name="amount">The amount of fuel to give.</param>
	public void AcceptFuel(float amount) {
		tankManager.fuel = Mathf.Clamp (tankManager.fuel + amount / 100f, 0f, 1f);
		fuelAmount = Mathf.Clamp (fuelAmount + amount, 0f, 1000f);
	}

	// Lerp the light based on fuel
	void UpdateLight(){
		furnaceLight.intensity = Mathf.Lerp (0.5f, maxLight, tankManager.fuel);
	}

	// Scale the volume of the audio sources based on fuel
	void FurnaceAmbience() {
		audioSources [0].volume = tankManager.fuel / 8f + 0.05f; // Ambience
		audioSources [1].volume = tankManager.fuel / 14f + 0.05f; // fire crackling
	}

	// Scales the box bounds, start size and emission rate of the Fire Particles based on fuel
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

	// Scales the start lifetime, start size and emission rate of the Smoke Particles based on fuel
	void ScaleSmokeParticles() {
		// start lifetime
		//smokeMainModule.startLifetime = LerpBasedOnFuel(smokeStartLifeTime);

		// start size
		//smokeMainModule.startSize = LerpBasedOnFuel(smokeStartSize);

		// emission
		//smokeEmissionModule.rateOverTime = LerpBasedOnFuel(smokeEmissions);
	}

	// Scales the emission rate of the Ember Particles based on fuel
	void ScaleEmbersParticles() {
		// emission
		emberEmissionModule.rateOverTime = LerpBasedOnFuel(emberEmissions);

	}

	// Lerps between the give bounds based on fuel | (x, y) = (lower, upper)
	float LerpBasedOnFuel(Vector2 ranges) {
		return Mathf.Lerp(ranges.x, ranges.y, tankManager.fuel);
	}
}
