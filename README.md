# Parallax · A little depth from a little movement

A Unity 2D scene where four flat layers turn a sideways walk into a sense of distance.

## The physics behind the illusion

Look out of a moving train: nearby trees rush past, while distant clouds barely move. This is **motion parallax**—a change in viewpoint makes objects at different distances shift by different amounts.

For a camera moving sideways without rotating, perspective gives `screen shift ≈ −f × camera shift / depth`, where `f` is focal length. Greater depth means less apparent motion. This project recreates that cue with an orthographic camera and a movement multiplier for each sprite layer.

## Four layers, four distances

![Exploded 3D-style view of the four assets: parallaxEffect values 1.0, 0.8, 0.6 and 0.3 produce world shifts of +10, +8, +6 and +3 for a camera shift of +10; relative screen shifts are 0, −2, −4 and −7.](docs/parallax-layers.png)

*Depth is illustrative: the actual scene uses 2D sprites. `p` means `parallaxEffect`; screen shifts are expressed in world units relative to the camera, before wrapping.*

The sky (`1.png`) uses **1.0**, clouds (`2.png`) **0.8**, distant terrain (`3.png`) **0.6**, and foreground (`4.png`) **0.3**. A higher value follows the camera more closely, so it drifts less on screen and feels farther away. At **0**, a layer stays fixed in the world; at **1**, it stays fixed relative to the camera horizontally.

## Inside the script

[`ParallaxEff.cs`](Assets/ParallaxEff.cs) is attached to each main background layer, with `cam` referencing the scene camera.

- **`Start()`** stores the layer’s initial X position (`startPos`) and its rendered width (`length`).
- **`FixedUpdate()`** computes the camera-driven offset and updates X, preserving Y and Z:

  ```csharp
  float distance = cam.transform.position.x * parallaxEffect;
  float move = cam.transform.position.x * (1 - parallaxEffect);
  transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
  ```

- **Wrapping:** when `move` crosses `startPos ± length`, the script shifts `startPos` by one sprite width. Each main layer has two child copies at local X positions **−36** and **+36**, so the repeated strip moves together and covers the scrolling view. The adjusted origin is applied on the next physics tick.

The apparent horizontal position is therefore `startPos − cameraX × (1 − parallaxEffect)`. That subtraction explains why the foreground appears to move faster even though its multiplier is smaller.

## Credits

The parallax tutorial is by [Game Code Library](https://www.youtube.com/watch?v=AoRBZh6HvIk). The background artwork comes from [Nature Landscapes Free Pixel Art](https://free-game-assets.itch.io/nature-landscapes-free-pixel-art) by Free Game Assets / CraftPix. The art remains the creator's work; see its [license](https://craftpix.net/file-licenses/) for usage terms.

## Take a walk

Open the project in **Unity 6000.6.2f1**, load [`SampleScene`](Assets/Scenes/SampleScene.unity), and press **Play**. Use **← / →** to move. [`PlayerMovementScript.cs`](Assets/Scripts/Player/PlayerMovementScript.cs) handles movement and facing direction; Cinemachine follows the player, and the background layers respond to the camera’s X position.
