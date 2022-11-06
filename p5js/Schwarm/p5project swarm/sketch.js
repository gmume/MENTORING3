const flock = [];
const initialmaxForce = 1;
const initialSpeed = 4;
const initialBoidsView = 50;

let maxForceSlider,
  maxSpeedSlider,
  boidsViewSlider,
  alignSlider,
  cohesionSlider,
  separationSlider;
  
let font;

function preload() {
  font = loadFont("ebrima.ttf");
}

function setup() {
  createCanvas(1000, 600);
  maxForceSlider = createSlider(0.5, 1.5, initalmaxForce, 0.01);
  maxForceSlider.position(5, 40);
  maxSpeedSlider = createSlider(1, 8, initalSpeed, 0.05);
  maxSpeedSlider.position(5, 70);
  boidsViewSlider = createSlider(10, 100, initalBoidsView, 0.05);
  boidsViewSlider.position(5, 100);
  alignSlider = createSlider(0, 2, 1, 0.1);
  alignSlider.position(5, 130);
  cohesionSlider = createSlider(0, 2, 1, 0.1);
  cohesionSlider.position(5, 160);
  separationSlider = createSlider(0, 2, 1, 0.1);
  separationSlider.position(5, 190);
  textFont(font, alignSlider.height + 5);

  for (let i = 0; i < 200; i++) {
    flock.push(new Boid());
  }
}

function draw() {
  background(51);

  for (let boid of flock) {
    boid.edges();
    boid.flock(flock);
    boid.updateBoids();
    boid.showBoids();
  }

  noStroke();
  fill("yellow");
  text("max force", 15 + alignSlider.width, 55);
  text("max speed", 15 + alignSlider.width, 85);
  text("boid's view", 15 + alignSlider.width, 115);
  text("align", 15 + alignSlider.width, 145);
  text("cohesion", 15 + alignSlider.width, 175);
  text("separation", 15 + alignSlider.width, 205);
}
