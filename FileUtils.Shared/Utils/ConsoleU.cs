using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System {
    public static class ConsoleS {
        private static bool WaitingForInput;

        public static string ReadLine() {
            WaitingForInput = true;
            Console.Write("> ");
            string line = Console.ReadLine();
            WaitingForInput = false;
            return line;
        }

        public static void WriteLine(string str, ConsoleColor color) {
            if (WaitingForInput) {
                Console.SetCursorPosition(0, Console.CursorTop);
            }

            ConsoleColor last = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ForegroundColor = last;
        }

        public static void WriteLine(string str, ConsoleColor color, params object[] format) {
            if (WaitingForInput) {
                Console.SetCursorPosition(0, Console.CursorTop);
            }

            ConsoleColor last = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(str, format);
            Console.ForegroundColor = last;
        }
    }
}
