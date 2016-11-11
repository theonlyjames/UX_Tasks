# UX_Tasks
UX Tasks

Instructions:

Movement:
Click and hold down anywhere on the screen with the mouse and the "Cursor" (green sphere object) will have forces applied to it in the following way: 

x: force applied transform.positoin.x > mouse screen postion x;
y: force applied transform.positoin.y > mouse screen postion y;
z: force applied transform.position.z > scene.positio.z = 0; // z will be controlled by A and S keys

Pressing the "A" key moved the cursor sphere positive on the Z axis.
Pressing the "S" key moved the cursor sphere negative on the Z axis.

Pressing the "R" key sets the "Cursor" back to its original position.

When the Cursor Sphere approaches the mouse an increase in drag is added until it comes to a stop. 
When the Cursor Sphere passes the threshold (defined by Cursor Mouse Position delta) and you keep mousedown it will snap onto the position (mouse x,y cursor sphere's z = 0) and manipulation of the object's transform will be used instead of adding force to the "Cursor" Sphere.

You can then use the "Cursor" object to pass through the translucent outer sphere (I could have just used the Sphere Collider here) to change the inner sphere from white to blue then red when it hits the threshold.

With the mouse held down and the green sphere "snapped" to it's position, move it towards the ground plane. When it the sphere colides with the ground it should snap to that position. This functionality was made quick and dirty style. 
I still need to set up a trigger point so that the object can "seat" on the plane within a variable distance.

## First Task Completed Notes

The task I completed first has some properties of the other tasks.
Task:
A sphere that changes from white to blue based on palm proximity, and red when within the near threshold.


## Second Task
Task: Grab a sphere, have it snap into a "seat" (upon release) if it's within X cm


### TASK LIST

- Grab a sphere, have it snap into a "seat" (upon release) if it's within X cm

- A sphere that changes from white to blue based on palm proximity, and red when within the near threshold

- A simplified cursor-on-plane scenario, with a box that changes color when the cursor is over it

- A sphere that pushes away from your palm, and has a springy return to its original position when the palm moves away

- Make a script that places all its child objects along an evenly-spaced circle

### Notes

Stage 1 working `A sphere that changes from white to blue based on palm proximity, and red when within the near threshold`

Created a colider that flys with momentum to the cursor then stops and follows 1 : 1 ratio when the cursor moves.

This is for simulating the meta hands top point or palm colider.
