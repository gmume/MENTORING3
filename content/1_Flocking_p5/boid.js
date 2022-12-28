class Boid {
  constructor() {
    this.position = createVector(random(width), random(height));
    this.velocity = p5.Vector.random2D();
    this.velocity.setMag(random(1, 2));
    this.acceleration = createVector();
    this.maxForce = 1; //Controls, how fast they align.
    this.maxSpeed = 4;
    this.boidsView = 50; //perseption
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
    let steering = createVector(); //Vector (force) für Richtungskorrektur
    let total = 0;
    for (let other of boids) {
      let distance = dist(
        this.position.x,
        this.position.y,
        other.position.x,
        other.position.y,
      );

      //Just look at boids in view
      if (other != this && distance < this.boidsView) {
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

  separation(boids) {
    let steering = createVector(); //Vector (force) für Richtungskorrektur
    let total = 0;
    for (let other of boids) {
      let distance = dist(
        this.position.x,
        this.position.y,
        other.position.x,
        other.position.y,
      );

      //Just look at boids in view
      if (other != this && distance < this.boidsView) {
        let diff = p5.Vector.sub(this.position, other.position);
        diff.div(distance);
        steering.add(diff);
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
    let steering = createVector(); //Vector (force) für Richtungskorrektur
    let total = 0;
    for (let other of boids) {
      let distance = dist(
        this.position.x,
        this.position.y,
        other.position.x,
        other.position.y,
      );

      //Just look at boids in view
      if (other != this && distance < this.boidsView * 2) {
        steering.add(other.position);
        total++;
      }
    }
    if (total > 0) {
      steering.div(total);
      steering.sub(this.position);
      steering.setMag(this.maxSpeed);
      steering.sub(this.velocity);
      steering.limit(this.maxForce); //Limits magnitude
    }
    return steering;
  }

  flock(boids) {
    let alignment = this.align(boids);
    let cohesion = this.cohesion(boids);
    let separation = this.separation(boids);

    this.acceleration.add(separation);
    this.acceleration.add(alignment);
    this.acceleration.add(cohesion);

    //Update with slider's value
    this.maxForce = maxForceSlider.value();
    this.maxSpeed = maxSpeedSlider.value();
    this.boidsView = boidsViewSlider.value();
    cohesion.mult(cohesionSlider.value());
    alignment.mult(alignSlider.value());
    separation.mult(separationSlider.value());
  }

  update() {
    this.position.add(this.velocity);
    this.velocity.add(this.acceleration);
    this.velocity.limit(this.maxSpeed);
    this.acceleration.mult(0); //Reset vector
  }

  show() {
    strokeWeight(8);
    stroke(255);
    point(this.position.x, this.position.y);
  }
}
