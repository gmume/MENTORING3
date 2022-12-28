# MENTORING3

## Creative coding with Unity

Hi, I'm a student of the HSLU and study IT and Design. On this site I will document the progress of my mentorting in the semester of fall 2022, in which I'm exploring how to use unity for creative or generative coding. In a discussion with my mentor Guillaume Massol I decided on four goals:

1. Get familiar with Unity and Github
2. Challange yourself with little projects
3. Get faster with coding
4. Document the process on Github

#### October 2022
### Getting started
For this project I wanted to do version control with GitHub and use GitHub pages with a Jekyll theme to show the documentation. I have to mention here, that I'm not familiar with both of them. When I started a month ago, I thought, that this would be simple. Guess what? It wasn't! I'm new to Unity, GitHub and Jekyll and I got the impression, that there are a thousend ways to get the three of them working together. And belive me, I wrecked them all! I tried to set it up with Git Bash, cmd, Git Desktop and GitHub remote. I used many diffrent versions of gemfiles, .gitignores and tried out a bunch of Jekyll themes. Sometimes I was angry and desperate and often deleted the whole thing just to restart again. But here I am, the page is up and working and I'm finally ready to go. Whoop, whoop!

### Research
Before taking action myselfe I wanted to dig deeper into what creative coding actually is. I found an informative page, that shows many examples of what creative software can do.  
[THE POSSIBILITIES OF CREATIVE CODING](https://eindhoven.makerfaire.com/wp-content/uploads/sites/174/2020/08/Possibilities-of-Creative-Coding.pdf)

After a good talk with Dragica Kahlina, a lecturer at the HSLU, I decided to challange myselfe with building an AI that simulates a flock of birds. Therefore I researched the rules of a flock as they were phrased by Craig Reynolds. Here is, how an early implementation (1986) from C. Rynolds worked:  
[Craig Reynolds - Original 1986 Boids simulation](https://www.youtube.com/watch?v=86iQiV3-3IA)

### First flock in p5.js
Because I don't really know how Unity works and I can't programm in C# I thought, it would be good to first write a little prototype in p5.js. To begin with it, I took the example from Daniel Shiffman from The Coding Train in his Coding challenge #124: Flocking Simulation.  
[Coding Challange #124: Flocking Simulation](https://www.youtube.com/watch?v=mhjuuHl6qHM)

My attempt:
{% raw %}
<iframe src="content\1_Flocking_p5\index.html" width="100%" height="450" frameborder="no"></iframe> {% endraw %} [Full screen](content/1_Flocking_p5)

As you can see, the balance for the flocking behavior is very fragile.

#### November
### Flocking in Unity
Now I felt, I was ready to tackle Unity. But my feeling was terribly wrong! I had issues over and over. I managed to let the boids (member of a flock) spawn and at least one had a flight course, but there I was stuck.  
![Flocking in Unity](content/FLOCK1/san-juan-mountains.jpg "San Juan Mountains")
