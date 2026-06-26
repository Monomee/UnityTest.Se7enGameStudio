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

# Phase 3
1. Wall  
- add wall around (player can not go outside football field  
- create barrier material by shader graph  
2. Reset button  
- add reset button  
- add pause/setting UI  
3. Polish  
- arrange hierarchy  
- fix camera follow ball 's view  
- add trail to ball
- add dust particle system  
- add icon for game
- fix bug:
+ problem: when player gets close to a ball (called A), so there are 2 buttons: kick and auto kick, then click auto kick; instead of kick the furthest ball, A is kicked 
+ reason (maybe): when animation "kick" plays, collider can be triggered again, then target ball is reassigned, override previous target ball (the furthest)
+ solution: create new variable "nearBall", nearBall is ball near player, targetBall is ball that is kicked; auto kick: assign furthest ball to target ball; kick: when change camera, if target ball is null -> then assign near ball to target ball
- add UI manager: manage 2 buttons (kick and auto kick), when click button, it will be turned off interactable, after shoot ball into goal, turn on again
