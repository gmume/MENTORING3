class Boid {
  constructor() {
    this.position = createVector(random(width), random(height));
    this.velocity = p5.Vector.random2D();
    this.velocity.setMag(random(2, 4));
    this.acceleration = createVector();
    this.maxForce = initialmaxForce; //Controls, how fast they align.
    this.maxSpeed = initialSpeed;
    this.boidsView = initialBoidsView; //perseption
  }

  //donut world
  edges(){
    if(this.position.x > width) {
        this.position.x = 0;
    } else if(this.position.x < 0) {
        this.position.x = width;
    }
    if(this.position.y > width) {
        this.position.y = 0;
    } else if(this.position.y < 0) {
        this.position.y = width;
    }
  }

  align(boids) {
    let steering = createVector(); //Vector (force) to correct the direction
    let total = 0;
    for (let other of boids) {
      let distance = dist(
        this.position.x,
        this.position.y,
        other.position.x,
        other.position.y,
      );

      //Just look at boids in view
      if (other != this && distance <= this.boidsView) {
        steering.add(other.velocity);
        total++;
      }
    }
    if (total > 0) {
      steering.div(total);
      steering.setMag(this.maxSpeed);
      steering.sub(this.velocity);
      steering.limit(this.maxForce); //Limits magnitude
    }
    return steering;
  }

  cohesion(boids) {
    let steering = createVector();
    let total = 0;
    for (let other of boids) {
      let distance = dist(
        this.position.x,
        this.position.y,
        other.position.x,
        other.position.y,
      );

      if (other != this && distance <= this.boidsView) {
        steering.add(other.position);
        total++;
      }
    }
    if (total > 0) {
      steering.div(total);
      steering.sub(this.position);
      steering.setMag(this.maxSpeed);
      steering.sub(this.velocity);
      steering.limit(this.maxForce);
    }
    return steering;
  }

  separation(boids) {
    let steering = createVector();
    let total = 0;
    for (let other of boids) {
      let distance = dist(
        this.position.x,
        this.position.y,
        other.position.x,
        other.position.y,
      );

      if (other != this && distance <= this.boidsView*2) {
        let diff = p5.Vector.sub(this.position, other.position);
        diff.div(distance * distance);
        steering.add(diff);
        total++;
      }
    } 
    if (total > 0) {
      steering.div(total);
      steering.setMag(this.maxSpeed);
      steering.sub(this.velocity);
      steering.limit(this.maxForce);
    }
    return steering;
  }

  //Apply rules
  flock(boids) {
    let alignment = this.align(boids);
    let cohesion = this.cohesion(boids);
    let separation = this.separation(boids);
    
    this.acceleration.add(alignment);
    this.acceleration.add(cohesion);
    this.acceleration.add(separation);

    //Update with slider's value
    this.maxForce = maxForceSlider.value();
    this.maxSpeed = maxSpeedSlider.value();
    this.boidsView = boidsViewSlider.value();
    alignment.mult(alignSlider.value());
    cohesion.mult(cohesionSlider.value());
    separation.mult(separationSlider.value());
  }

  updateBoids() {
    this.position.add(this.velocity);
    this.velocity.add(this.acceleration);
    this.velocity.limit(this.maxSpeed);
    this.acceleration.mult(0); //Reset vector
  }

  showBoids() {
    strokeWeight(8);
    stroke(255);
    point(this.position.x, this.position.y);
  }
}
