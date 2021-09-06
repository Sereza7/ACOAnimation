# ACOAnimation
## Parameters
- GAMMA: Minimum value for the chance to move to another tile. Compare this value to pheromone/distance to get its relevance. Higher is more chaotic. 0 means that the ants won't explore anything new after a few trips. Recommended range: 0f-1000f.
- Q: Factor to the amount of pheromones put down by ants. Higher means the pheromone quantities will rise faster, making the GAMMA part of the direction choice less useful. The relation isn't easy to see. Recommended range: 0f-100f
- tickSpeed: Minimum amount of time between two ant movement. Capped by unity update rate (should be at 50/60 FPS). 1 second is quite slow and allows to follow one specific individual. 0.1 second is good for trend observation. Recommended range: 0.05f-5f
- Ant count: Quantity of ants to render and display. Recommended range: 1-200
- Terrain Size: The actual size of the terrain in units will be egal to 2 to the power of ::value:: plus one. Recommended range: 5-7 (==>33-129)
- Random intensity: The higher this value, and the more the map will generate towards positive values. Pretty much useless, just perform a translation if needed. Recommended range: 0f-1f
- Terrain smoothing: The lower the value, the lower the spikes on the terrain. 0.8 is terribly shacky, 0.1 is almost flat. Recommended range: 0f<x<1f
- Pheromone Loss: This number multiplies the values on the pheromone grid at each ant's finished path. Recommended value: 0.001f (0<x<=1)
- Pheromone initialisation: Wether or not initialize the grid with a direction towards the food. Can unactivate on smaller terrains, but almost mandatory on larger terrains.

For more information on those parameters, see the documentation for: 
- Diamond-square algorithm (terrain generation): https://en.wikipedia.org/wiki/Diamond-square_algorithm
- Ant behavior/path finding: https://en.wikipedia.org/wiki/Ant_colony_optimization_algorithms

The distance between two points in this build is a bit special, if you want to test other distances, rebuild your own project from the assets shared here.

<pre><code>
    internal float distance(Vector3 spotA, Vector3 spotB)
	{
		if (spotA.z == spotB.z) {
			print("samePoint");
			return 0.1f;
		} else if (Math.Abs(spotA.x - spotB.x) <= 1 && Math.Abs(spotA.y - spotB.y) <= 1) {
			return (float)Math.Pow(spotA[2] * 100 - spotB[2] * 100,2) + 0.1f;
		} else {
			return 9999999999;
		}
			
 	}
</code></pre>

Unity v. 2020.3.1f1

The web build is... working? Really unstable, might be down at any point in the future: https://sereza7.github.io/ACOAnimation/ 

Video demonstration of the latest version of the project: https://youtu.be/OKg2MPJ-gUQ

06/09/21: New application with parameters & all

___________________________________________
Design of physically grounded communication system course spring 2021 at Keio University, Lucas Charpentier.

I started this project in this course, and decided to expand upon it later on. Under there you can find miscellaneous documentation and information about early stages of this project.

## Mid-term report:
https://youtu.be/4P6rDiFF1V0

Repartition of pheromones through generations/ waves of ants:

https://youtu.be/FgbOtWgEnJE

Old version of the animation:
https://youtu.be/fCJVfSehcpE



Presentation slides:

https://docs.google.com/presentation/d/1V1FmoMcheOH6OL9ascsREVAVck8XcPkgGyRMHbNSeQM/edit?usp=sharing

Read the notebook "Animation.ipynb" for more information.
"Data" folder contains the outputs of this notebook.

## End of term report
New notebook "Refined ant algorithm.ipynb" with a more proper implementation of the algorithm.
"refinedData" folder contains the outputs of this notebook.

Added the Asset folder of the Unity project. There you can find all the ressources if you want to reuse my project or look at the final code.

A web build of this Unity app is available at https://sereza7.github.io/ACOAnimation/. However it's pretty heavy so you might need some tweaking on your browser parameters to run it. To run it locally, open "index.html" after downloading it alongside the folders "Build" and "TemplateData".

Video recordings of this projects are available here:

https://youtu.be/fD3oN6eB3LU

https://youtu.be/HQBfxGQ-1YA

Some misc pictures of the unity environment are available in the folder "pictures".
