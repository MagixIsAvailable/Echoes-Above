

Echoes  Above
An immersive VR experience designed to promote calmness, focus, and emotional well-being through interactive sound and visuals.

Overview
Sky Resonance VR places the user on a floating platform above the clouds, surrounded by interactive glowing orbs.
Each orb plays a calming steel drum tone when touched or pointed at with the controllers.

The experience combines:

Sound therapy (steel drum tones)

Color transitions (dynamic gradient background)

Visual reactions (floating swarm orbs echo the sounds)

This project was developed for Meta Quest 3 using Unity and the Meta XR SDK.

Key Features
Main Platform Scene

7 large interactive orbs arranged around the player

Each orb has a unique tone and color

Pressing an orb:

Plays a calming tone

Changes the sky color gradient

Causes nearby small swarm orbs to glow and echo

Swarm Orbs

Float dynamically around the environment

React to main orb sounds with color and audio echoes

Fog and Atmosphere

Dense fog hides the ground to create a surreal, isolated environment

Menu Scene

Simple VR menu with Start and Quit buttons

Controls
Point and Trigger:
Use either controller to point at a main orb and press the trigger to play its tone.

Touch:
You can also trigger tones by moving your controller close to an orb.

Technologies Used
Unity 2023

Meta XR SDK (All-in-One)

C# for scripts

Target Platform: Meta Quest 3 (Android, ARM64)

Project Structure
css
Copy
Edit
Assets/
├── Scenes/
│   ├── Menu.unity
│   └── MainScene.unity
├── Scripts/
│   ├── OrbInstrument.cs
│   ├── FloatingSwarmOrb.cs
│   ├── BackgroundColorController.cs
│   ├── OrbPointer.cs
│   ├── MenuManager.cs
│   └── SwarmSpawner.cs
├── Audio/
│   ├── SteelDrumTones/
│   └── Background/
└── Materials/
Setup Instructions
Clone or download the repository.

Open the project in Unity 2023 LTS.

Make sure the Meta XR SDK package is installed (v77+).

Scenes:

Menu.unity: First scene (in Build Settings)

MainScene.unity: Second scene

Connect a Meta Quest 3 device.

Switch to Android platform and build.

Known Issues
UI menus require a laser pointer for selection; currently, no hand tracking support.

Echoes from distant swarm orbs may be faint due to spatial audio distance.

Credits
Project & Code: [Michal L.]

Sound Samples: Custom steel drum recordings
Bacground sound : Night in nature - quetzalcontla (https://freesound.org/people/quetzalcontla/sounds/336602/#comments)

Inspiration: Research on music and sound therapy for stress relief.

License
This project is developed for educational purposes and not intended for commercial distribution.
