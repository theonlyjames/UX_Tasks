# UX_Tasks
UX Tasks

## First Task Completed Notes

The task I completed first has some properties of the other tasks.
Task:
A sphere that changes from white to blue based on palm proximity, and red when within the near threshold.

Instructions:

Click anywhere on the screen with a mouse and the "Cursor" object will fly to the cursor. When the Cursor approaches the mouse an increase in drag is added until it comes to a stop.When it passes the threshold and you keep mousedown it will snap onto the mouse and manipulation of the objects transform will be used instead of adding force to attract it.

You can then use the "Cursor" object to pass through the translucent outer sphere (I could have just used the Sphere Collider here) to change the inner sphere from white to blue then red when it hits the threshold.

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
