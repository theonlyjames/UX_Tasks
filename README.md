# UX_Tasks
UX Tasks

###Instructions:

You may need to toggle the activity of the different Game Objects to see the interactions.

The rectangle translucent box is the "Seat" area that kind of acts like the toolbelt. When you've grabbed the cursor object and let go of it outside of the seat area it now repels.

**Movement:**

- Pressing the "Space" key spawns new child objects

- Pressing "H" will lock a "Screen Cursor" Object to the screen an allow you to hover over the box "Hyperlink" on the screen in the background area.

Click and hold down anywhere on the screen with the mouse and the "Cursor" (green sphere object) will have forces applied to it in the following way:

- x: force applied transform.positoin.x > mouse screen postion x;
- y: force applied transform.positoin.y > mouse screen postion y;
- z: force applied transform.position.z > scene.positio.z = 0; // z will be controlled by A and S keys

- Pressing the "A" key moved the cursor sphere positive on the Z axis.
- Pressing the "S" key moved the cursor sphere negative on the Z axis.

- Pressing the "R" key sets the "Cursor" back to its original position.

When the Cursor Sphere approaches the mouse an increase in drag is added until it comes to a stop. This is mimicking a grab interaction.

You can then use the "Cursor" sphere to pass through the translucent outer sphere (I could have just used the Sphere Collider here) to change the inner sphere from white to blue then red when it hits the threshold.

### TASK LIST

- Grab a sphere, have it snap into a "seat" (upon release) if it's within X cm

- A sphere that changes from white to blue based on palm proximity, and red when within the near threshold

- A simplified cursor-on-plane scenario, with a box that changes color when the cursor is over it

- A sphere that pushes away from your palm, and has a springy return to its original position when the palm moves away

- Make a script that places all its child objects along an evenly-spaced circle
