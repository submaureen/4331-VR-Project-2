# Lettuce Cook

## Introduction

The cooking game with no lettuce. Also a filipino-centric cooking game.

Demo Link (extremely alpha build): https://drive.google.com/file/d/1ZcSvQe5G1gTzzVBdni-12O9PYiwbXAie/view

Video Link: https://youtu.be/vt0MABPl3u4

## Design

[Design Doc](https://docs.google.com/presentation/d/1rTE0VmtJqym6j5XQw8PG4IVCNN2LJIUQEFRp-l4X51M/edit?usp=sharing)


## Implementation

We have defined object types to help our game transition smoothly.

Types:
IngredientInfo, PrepStep, Preparation, Step, Recipe.


Using these object types, we are able to create a flexible prep/recipe building script.

![L](https://i.imgur.com/3jxtla7.png)

For example, here, there are 2 total Prep type objects. 
The first one is named adobo. There are 4 steps in adobo.
The Step object defines what must be done before a step in a recipe is considered finished. The first step uses the `garlic` ingredient, and must be `chopped` into `2` pieces. Then, the `garlic_clove` must be `mixed` (and there must be `2` of them).

Using this system, we can create a variety of recipes as long as the ingredient is a defined GameObject. 
The only constraints are obtaining models for the recipes and ingredients.

## Gameplay
For every recipe, there is also a prep stage incolving cutting, mixing, etc. The prep stage can be set to 0 to immediately jump to the cooking stage. For the stages, there are 3 important factors: ingredient, health, and time. The ingredient must be placed in the pot within the time limit, or health will be lost. After the recipe is finished, the score is displayed.

![L](https://i.imgur.com/PIPyfpp.gif)

![L](https://i.imgur.com/27NDYDu.gif)

![L](https://i.imgur.com/dzWq6NC.gif)


## Features 
* VR Mode
* Non-VR Mode
* Interactibility with cooking models
* User-friendly interface 
* Gelpul tip messages
* Challenging & Fun Gameplay 

## Duties
Domingo Cook: Integrating scripts with VR, recipe steps, models

Maureen Gloria: 2D debugging, Audio sfx, prep steps, models

John Valeriano: UI/UX, menu, recipe selection, 2D debugging, assets

See more on our duties by looking at our kanban board on [Notion](https://www.notion.so/978fd418aa93456782983cc21e5790e9?v=111ec87649cd49a4b0a835e79955d908)

![L](https://i.imgur.com/4QT8Ygt.png)

## Assets Used
* [Kitchen](https://opengameart.org/content/kitchen-low-poly)
* [Knife Asset](https://opengameart.org/content/knife-1)
* [Cooking Mama SFX](https://en.wikipedia.org/wiki/Cooking_Mama)
* [Hotsilog](https://sketchfab.com/3d-models/hotsilog-344f5845929c4f0e92d10c3a97249bd2#download)
* [Quaternius](http://quaternius.com/)