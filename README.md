# Phase1:  
1. Movement + animation: 
- using Input System package to binding input
- including idle, walking, running (moving + animation)
- model always rotates in the direction of travel
- has 2 bool to setting:
  + turnVisualWhenMoveBackward: True: Press S to turn the model towards the camera. False: Move backward slowly, always facing forward.
  + reverseInputWhenMoveBackward: This only works when turnVisual = true. True: Model-based orientation key. False: Screen-based orientation key.
- use state to manage/change animation 
2. Cinemachine:
- using Virtual Camera to follow player

# Phase 2:
- Add DOTween for ball movement  
- Add animation Kick for player  
- Use animation event (in Kick anim) to Shoot Ball  
- Add Kick and Auto Kick function (ball flies to nearest goal)  
- Use cinemachine to follow ball  
- Flow: Kick -> Set target ball -> Camera follow ball -> play animation Kick  -> shoot -> goal and play particle system -> camera return back to player / ball return back to spawn position  
