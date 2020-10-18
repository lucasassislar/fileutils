using Nucleus.ConsoleEngine;
using Nucleus.Platform.Windows;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileUtils.Commands {
    public class PrintLayersUnity : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "Print code for layers collision ignroes Unity"; }
        }

        private string command = "printlayersunity";

        private string[] layers = new string[] {
            "Default",
            "TransparentFX",
            "Ignore Raycast",
            "Water",
            "UI",
            "IgnoreBlobShadow",
            "Game",
            "Player",
            "Weapon",
            "Bullet",
            "DontCollideBullet",
            "DontCollidePlayer",
            "Enemy",
            "DontCollideFloor",
            "MovingPlatform",
            "Nicola",
            "StudentBullet",
            "Lighting_Dynamic_GEO",
            "PlayerLooker",
            "PixelPhysics",
            "Student",
            "DontCollideWithPlyLooker",
            "StudentFiring",
            "OnlyWithDefault",
            "2DWorld",
            "Boxes",
            "Collectable",
            "LaserBullet",
            "Teacher",
        };

        public PrintLayersUnity(ConsoleManager manager)
            : base(manager) {
        }

        public override CommandFeedback Execute(string[] args) {
            // arg1=image path
            Bitmap bmpLayers = (Bitmap)Bitmap.FromFile(args[1]);
            LockBitmap lockBitmap = new LockBitmap(bmpLayers);

            lockBitmap.LockBits();

            int startX = 165;
            int startY = 117;

            // increase
            int increase = 16;
            int totalX = 29;

            for (int y = 0; y < 29; y++) {
                int actualY = startY + (increase * y);

                for (int x = 0; x < totalX; x++) {
                    int actualX = startX + (increase * x);

                    Color color = lockBitmap.GetPixel(actualX, actualY);
                    if (color.R == 154 &&
                        color.G == 181 &&
                        color.B == 216) {
                        Console.Write("[X]");
                    } else {
                        Console.Write("[ ]");
                    }
                }
                totalX--;
                Console.WriteLine();
            }

            totalX = 29;
            for (int y = 0; y < 29; y++) {
                int actualY = startY + (increase * y);
                string firstLayer = layers[y];

                for (int x = 0; x < totalX; x++) {
                    int actualX = startX + (increase * x);
                    string secondLayer = layers[totalX - x - 1];

                    Color color = lockBitmap.GetPixel(actualX, actualY);
                    Console.Write($"{firstLayer}_{secondLayer}:");
                    if (color.R == 154 &&
                        color.G == 181 &&
                        color.B == 216) {
                        Console.Write("[X]");
                    } else {
                        Console.Write("[ ]");
                    }
                    Console.WriteLine();
                }
                totalX--;
                //Console.WriteLine();
            }


            lockBitmap.UnlockBits();

            return CommandFeedback.Success;
        }
    }
}
