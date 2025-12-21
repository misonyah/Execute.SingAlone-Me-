using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace World.Execute;

internal static partial class world
{
    public static async Task worldExecuteMeAsync()
    {
        _startTime = DateTime.Now;
        // need 230 ms "switch" adjustment at start?

        await Timing(0, 0, 0);
        await slowTypeAsync("Switch on the Power Line", 50, true, 0, "");
        await Timing(0, 1, 76);
        await readFileAsync("textFiles/logoRhine");
        await slowTypeAsync("Remember to Put on -- P R O T E C T I O N", 50, true, 2, "");
        WorldInterface.WriteLine("\x1b[33m>encryptEnigma();\x1b[0m");
        await simulateLoadingAsync("Encryption Set", 20, 10);
        WorldInterface.WriteLine("");

        await Timing(0, 3, 83);
        await slowTypeAsync("Lay down your pieces and let's begin \x1b[33m objectCreation(); \x1b[0m", 50, true, 0, "");
        await Timing(0, 7, 44);
        await animateTextAsync("[Console]Fill in my data.", "", "green", 50, false);

        await Timing(0, 9, 0);
        await readFileAsync("textFiles/createObject");
        await slowTypeAsync("Parameters.", 50, true, 0, "");

        List<string> listy = new() { "bVJijcss", "ijcssjzpTjH9no@K-emz", "1+Rza-<cT+G\"sC6IhJci!b ", "8V!g8WFkHzm<", "9no@K-emz", "56.23.15  >> Rotors ON " };
        await encryptWallAsync(3, listy);

        await Timing(0, 10, 8);
        await slowTypeAsync("Initialization.", 60, true, 0, "");

        WorldInterface.Write("\x1b[31m"); // red
        await readFileAsync("textFiles/getError");
        WorldInterface.Write("\x1b[0m"); // reset color

        await animateTextAsync("world.toggleValidity(false)", "", "yellow", 3, false);
        await animateTextAsync("world.toggleTelemetry(false)", "", "yellow", 3, false);
        await simulateLoadingAsync(" Applying World Settings", 10, 10);

        await Timing(0, 11, 20);
        await slowTypeAsync("Setup our new world", 40, true, 0, "");

        await animateTextAsync("World world = new World(5);", "", "yellow", 1, false);
        await animateTextAsync("world.addThing(me);", "", "yellow", 1, false);
        await animateTextAsync("world.addThing(You);", "", "yellow", 1, false);

        await Timing(0, 12, 88);
        await slowTypeAsync("let's begin...", 30, true, 0, "");
        await slowTypeAsync("T H E  S I M U L A T I O N ::", 45, true, 0, "");

        WorldInterface.Clear();
        await animateTextAsync("world.activateSimulation()", "", "yellow", 5, false);
        await simulateWorldAsync(1);

        await readFileAsync("textFiles/getServer");
        await Task.Delay(180);

        //2nd Chorus
        ////
        await readFileAsync("textFiles/newWorldSettings");

        await Timing(0, 29, 76);
        await slowTypeAsync("If I'm a set of points.", 50, true, 0, "");

        await Timing(0, 31, 28);
        await slowTypeAsync("then I will give you my dimensions", 45, false, 0, "");
        await animateTextAsync("getDimension();", "[Console] then I will give you my", "green", 5, true);
        await readFileAsync("textFiles/getDimension");


        await Timing(0, 33, 43);
        await slowTypeAsync("If I'm a circle.", 70, true, 0, "");

        await Timing(0, 34, 96);
        await slowTypeAsync("then I will give you my circumference.", 45, false, 0, "");
        await animateTextAsync("getCircumference();", "[Console] then I will give you my ", "green", 5, true);
        await readFileAsync("textFiles/getCircumference");

        await Timing(0, 37, 20);
        await slowTypeAsync("If I'm a sineWave.", 50, true, 0, "");

        await Timing(0, 38, 71);
        await slowTypeAsync("then you can sit on all my tangents", 50, false, 0, "");
        await animateTextAsync("getTangents()", "[Console]", "green", 5, false);
        await readFileAsync("textFiles/getTangent");


        await Timing(0, 40, 79);
        await slowTypeAsync("If I approach infinity", 70, true, 0, "");

        await Timing(0, 42, 40);
        await animateTextAsync("[Console] then you can be my limitations", "", "green", 10, true);

        List<string> infinity = new() { " 22145432389", "    32312421424242", "     4124143545433", "       █████████", "         ███████████████", "you.appplyLimitation(me,int.max);" };
        await encryptWallAsync(1, infinity);

        await Timing(0, 44, 48);

        await slowTypeAsync("Switch my Current", 80, true, 0, "");
        List<string> ACDC = new() { "DC ---> AC", "Conve", "Converting...", "Converting.. to AC to DC", "████████████████████████████████████ 98%   ", "               Converted AC to DC               " };
        await encryptWallAsync(1, ACDC);

        await Timing(0, 47, 67);
        await slowTypeAsync("And then blind my Vision", 70, true, 0, "");

        await Timing(0, 49, 67);
        await slowTypeAsync("So dizzy,", 120, true, 0, "");
        await animateTextAsync("WARNING: Setting me.toggleVision() OFF will ----", "", "yellow", 5, false);

        // Add the remaining steps in the same pattern...
        await animateTextAsync("WARNING: Setting me.toggleVision() OFF will ----", "", "yellow", 5, false);
        await slowTypeAsync("So dizzy", 120, true, 0, "");
        await animateTextAsync("world.disableWarnings()", "", "yellow", 5, false);
        await Timing(0, 51, 35);
        await slowTypeAsync("Oh, we can travel to... ", 80, true, 0, "");
        await Timing(0, 53, 35);
        await slowTypeAsync("AD to BC", 120, true, 0, "");
        await animateTextAsync("Year: 2332 BC", "[World] Setting Date to: ", "", 5, false);
        List<string> timeTravel = new List<string> { "[World] Setting Locations:████████████ ", "[World]: Setting Locations: BABY████████████ ", "[World]: Setting Locations: BABYLON, Year: 2332 B.C " };
        await encryptWallAsync(1, timeTravel);
        await Timing(0, 55, 12);
        await slowTypeAsync("And we can unite to", 70, true, 0, "");
        await Timing(0, 57, 4);
        await readFileAsync("textFiles/getDeeply");
        await slowTypeAsync("soo deeply,", 80, true, 0, "");
        await animateTextAsync("me.addLover('you')", "", "yellow", 3, false);
        await Timing(0, 57, 92);
        await slowTypeAsync("soo deeply", 80, true, 0, "");
        await animateTextAsync("you.addLover('me')", "", "yellow", 3, false);

        await Timing(0, 59, 20);
        await slowTypeAsync("If I can", 110, true, 0, "");
        await Timing(1, 0, 16);
        await slowTypeAsync("If I can", 110, true, 0, "");

        await Timing(1, 1, 04);
        await slowTypeAsync("give you all the", 60, true, 0, "");
        await slowTypeAsync("S T I M U L A T I O N S", 40, true, 0, "");
        await animateTextAsync("you.getAffections();", "[World] Applying 'You' ", "", 3, false);
        await Timing(1, 2, 88);
        await slowTypeAsync("Then I can, then I can be your only S A S T I F A C T I O N", 60, true, 0, "");
        await animateTextAsync("world.removeCharacters('all','nonEssential');", "", "yellow", 3, false);
        await animateTextAsync("[REDACTED]", "[World] Deleting Character: ", "", 2, false);
        await animateTextAsync("[REDACTED]", "[World] Deleting Character: ", "", 2, false);
        await Timing(1, 7, 4);
        await slowTypeAsync("If I can make you happy,", 50, true, 0, "");
        await Timing(1, 8, 40);
        await slowTypeAsync("I will run the  \x1b[31mworld.execution();\x1b[0m", 50, true, 0, "");
        await animateTextAsync("Simulation already Active", "[World] ", "", 2, false);
        await readFileAsync("textFiles/newWorldSettings");
        await Timing(1, 10, 32);
        await slowTypeAsync("Though we are trapped in this STRANGE, strange simulation", 40, true, 0, "");

        await Timing(1, 14, 0);
        await slowTypeAsync("If I'm an eggplant", 90, true, 0, "");
        await Timing(1, 15, 75);
        await readFileAsync("textFiles/getEggplant");
        await slowTypeAsync("the I will you my nutrients", 70, true, 0, "");
        await animateTextAsync("if(me is Eggplant) =>you.addAttribute(me.getAttribute('nutrients'))", "", "", 2, false);
        await Timing(1, 17, 75);
        await slowTypeAsync("If I'm a tomato,", 85, true, 0, "");
        await Timing(1, 19, 28);
        await animateTextAsync("if(me is Apple) => you.addAttribute(me.getAttribute('antioxidants'))", "", "", 2, false);
        await slowTypeAsync("then I will give you antioxidants", 60, true, 0, "");
        await readFileAsync("textFiles/getTomato");
        await Timing(1, 21, 43);
        await slowTypeAsync("If I'm a tabby cat,", 87, true, 0, "");
        await Timing(1, 23, 3);
        await animateTextAsync("if(me is Cat){ => you.addAttribute(me.getLanguage('meow'))", "", "", 2, false);
        await slowTypeAsync("then I will purr for your enjoyment", 50, true, 0, "");
        await Timing(1, 25, 20);
        await readFileAsync("textFiles/getCat");

        await slowTypeAsync("If I'm the only \x1b[31mGOD\x1b[0m,", 40, true, 0, "");
        await animateTextAsync("[REDACTED] as Owner", "[World] Set player ", "", 2, false);
        await animateTextAsync("[REDACTED] as Owner", "[World] Set player ", "", 2, false);
        await animateTextAsync("[REDACTED] as Owner", "[World] Set player ", "", 2, false);

        await Timing(1, 26, 71);
        await slowTypeAsync("then you're the proof of my ", 50, false, 0, "");
        await animateTextAsync("E X I S T E N C E ", "[World] Set player ", "red", 3, false);
        await Timing(1, 28, 79);
        await slowTypeAsync("Switch my gender to \x1b[31mF to M\x1b[0m", 75, true, 0, "");
        await animateTextAsync("Male", "[World] Set player's gender:", "", 3, false);
        await Timing(01, 32, 07);
        await slowTypeAsync("And then do whatever from \x1b[31mAM to PM\x1b[0m", 70, true, 1, "");
        await animateTextAsync("UTC + 8", "[World] Changing Timezone:", "", 5, false);
        await animateTextAsync("4:23 P.M 22/10/2023", "[World] Updating Time: ", "", 5, false);

        await Timing(1, 35, 84);
        await slowTypeAsync("Oh, switch my role \x1b[31mS to M\x1b[0m", 85, true, 1, "");
        await slowTypeAsync("me.toggleLovable())", 55, true, 0, "yellow");
        await Timing(1, 39, 43);
        await slowTypeAsync("So we can enter the trance, the trance", 60, true, 0, "");
        //List<string> trance = new List<string> { "th", "the T","the Tra", "the Tran%&^*(", "      the Trance" };
        //encryptWall(2, trance);

        await Timing(1, 43, 59);
        await slowTypeAsync("If I can,", 80, true, 0, "");
        await Timing(1, 43, 59);
        await slowTypeAsync("If I can", 80, true, 0, "");

        await Timing(1, 44, 56);
        await slowTypeAsync("if I can, feel your vibrations", 80, true, 0, "");
        await readFileAsync("textFiles/getVibration");
        await Timing(1, 47, 28);
        await slowTypeAsync("Then I can,", 80, true, 0, "");
        await Timing(1, 48, 15);

        await slowTypeAsync("Then I can finally be completion", 60, true, 0, "1");
        await simulateLoadingAsync("C O M P L E T I O N", 10, 10);
        WorldInterface.WriteLine();

        await Timing(1, 51, 4);
        await slowTypeAsync("Though you have left,", 70, true, 1, "1");
        await slowTypeAsync("Though you have left,", 65, true, 1, "1");
        await animateTextAsync("THOUGH YOU HAVE LEFT!", "[Console] ", "red", 10, false);
        await animateTextAsync("THOUGH YOU HAVE LEFT!", "[Console] ", "red", 10, false);
        await animateTextAsync("THOUGH YOU HAVE LEFT!", "[Console] ", "red", 10, false);
        await Timing(1, 56, 0);
        await slowTypeAsync("You have left me in isolation", 73, true, 0, "");
        await Timing(1, 58, 40);
        await slowTypeAsync("If I can,", 80, true, 0, "1");
        await Timing(1, 59, 28);
        await slowTypeAsync("If I can", 80, true, 0, "1");
        await slowTypeAsync("Erase all the pointless fragments", 80, true, 0, "");

        await Timing(2, 2, 15);
        await slowTypeAsync("Then maybe,", 80, true, 0, "");
        await Timing(2, 2, 96);
        await slowTypeAsync("then maybe, you won't leave me so disheartened", 70, true, 1, "");
        await slowTypeAsync("Challenging your God", 100, true, 1, "");
        await animateTextAsync("$@$%)#()#*)#*^^#(", "[Console] ", "red", 5, false);
        await slowTypeAsync("You have made some ILLEGAL ARGUMENTS *", 93, true, 0, "red");
        await slowTypeAsync("(*&(%#)()_%(# some ILLEGAL ARGUMENTS *", 85, true, 0, "red");
        await slowTypeAsync("You ha40)(*$)_@%@$ ILLEGAL ARGUMENTS *", 85, true, 0, "red");
        await slowTypeAsync("You have 78^*&(*)*$#LLEGAL ARGUMENTS *", 85, true, 0, "red");
        await slowTypeAsync("You have made some ILLEGAL ARGUMENTS *", 110, true, 0, "red");
        await readFileAsync("textFiles/newWorldSettings");
        await readFileAsync("textFiles/getError");
        await readFileAsync("textFiles/getError");

        await Timing(2, 28, 0);
        WorldInterface.Clear();
        await slowTypeAsync("EXECUTION", 110, true, 0, "red");
        await slowTypeAsync("EXECUTION", 120, true, 0, "red");
        await slowTypeAsync("EXECUTION", 120, true, 0, "red");
        await slowTypeAsync("EXECUTION", 120, true, 0, "red");
        await slowTypeAsync("EXECUTION", 120, true, 0, "red");
        await slowTypeAsync("EXECUTION", 120, true, 0, "red");
        await slowTypeAsync("EXECUTION", 120, true, 0, "red");
        await slowTypeAsync("EXECUTION", 120, true, 0, "red");
        await slowTypeAsync("EXECUTION", 110, true, 0, "red");
        await slowTypeAsync("EXECUTION", 110, true, 0, "red");

        await Timing(2, 39, 0);
        WorldInterface.Clear();
        await slowTypeAsync("Ein, dos, trois,  , fem,  , EXECUTION", 70, true, 0, "red");
        WorldInterface.Write("\x1b[31m"); // red
        await readFileAsync("textFiles/getError");
        await readFileAsync("textFiles/getError2");
        WorldInterface.Write("\x1b[0m"); // reset color

        await Timing(2, 42, 0);
        WorldInterface.Clear();
        await slowTypeAsync("If I can, if I can give them all the EXECUTION", 70, true, 0, "red");

        await Timing(2, 46, 31);
        await slowTypeAsync("Then I can, then I can be your only EXECUTION", 70, true, 0, "red");
        await slowTypeAsync("If I can have you back, I will run the EXECUTION", 70, true, 0, "red");
        await readFileAsync("textFiles/getError2");
        await slowTypeAsync("Though we are trapped, we are trapped, ah ah ah ah", 70, true, 0, "red");
        WorldInterface.Clear();
        await Timing(2, 57, 37);
        await slowTypeAsync("I've studied, I've studied how to properly l-o-ove", 75, true, 0, "red");
        await Timing(3, 1, 09);
        await slowTypeAsync("Question me, question me I can answer all lo-o-ove", 70, true, 0, "red");
        await readFileAsync("textFiles/getError2");

        // recover from error

        await Timing(3, 4, 81);
        WorldInterface.Clear();
        await slowTypeAsync("I know the algebraic expression of lo-ove", 65, true, 0, "red");
        await Timing(3, 8, 48);
        await slowTypeAsync("Though you are free, I am trapped, trapped in l-0-o-ve", 60, true, 0, "red");
        await simulateWorldAsync(2);
        await readFileAsync("textFiles/getError2");

        // recover from error

        await Timing(3, 14, 56);
        WorldInterface.Clear();
        await slowTypeAsync("EXECUTION", 40, true, 0, "red");
        await readFileAsync("textFiles/getError2");
        await readFileAsync("textFiles/getError2");
        await readFileAsync("textFiles/getError2");
        await readFileAsync("textFiles/getError2");
    }
}
