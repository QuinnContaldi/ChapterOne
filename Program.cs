﻿// See https://aka.ms/new-console-template for more information

using ChapterOne;

var kineticReload = new KeneticReload();
var plasmaReload = new PlasmaReload();
var lazerReload = new LazerReload();

// Create a sniper gun using kinetic reload
var sniper = new Sniper("Sniper Rifle", kineticReload);

sniper.GunName();       // Output: Your current gun is Sniper Rifle
sniper.Reload();        // Output: Reloading with Kinetic
sniper.FireGun();       // Output: Your current gun is firing Kinetic
// Switch to plasma reload at runtime
sniper.reload = plasmaReload;
sniper.Reload();        // Output: Reloading Plasma Rounds
sniper.FireGun();       // Output: Your current gun is firing Plasma
//Switch to lazer
sniper.reload = lazerReload;
sniper.Reload();
sniper.FireGun();
//Gun sniper()
