using MapGenerator.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public static class GraphicModule
    {
        // Обводка зоны
        public static void DrawRectangle(int sx, int sy, int width, int height, char che, ConsoleColor Back = ConsoleColor.Black, ConsoleColor Front = ConsoleColor.White)
        {
            using (new Colorife(Back, Front))
            {
                for (int x = sx - 1; x <= sx + width; x++)
                {
                    Console.SetCursorPosition(x, sy - 1);
                    Console.Write(che);
                    Console.SetCursorPosition(x, sy + height);
                    Console.Write(che);
                }

                for (int y = sy - 1; y <= sy + height; y++)
                {
                    Console.SetCursorPosition(sx - 1, y);
                    Console.Write(che);
                    Console.SetCursorPosition(sx + width, y);
                    Console.Write(che);
                }
                    
            }
        }

        // Вывод сообщения под карту
        public static void ConsoleMessage(string message)
        {
            using (new Colorife(ConsoleColor.Black, ConsoleColor.Gray))
            {
                Console.SetCursorPosition(ConsoleSettings.RIGHT_MENU_WIDTH, ConsoleSettings.HEIGHT - 2);
                Console.Write(message);
                for (int i = 0; i < ConsoleSettings.WIDTH - ConsoleSettings.RIGHT_MENU_WIDTH - message.Length; i++)
                    Console.Write(" ");
            }
        }

        // Отрисовка карты
        public static void HardDrawMap(BaseMap map)
        {
            Console.SetCursorPosition(ConsoleSettings.RIGHT_MENU_WIDTH, ConsoleSettings.UP_MENU_HEIGHT);
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    using (new Colorife(map[x, y]))
                    {
                        Console.Write(map[x, y].Char);
                    }
                }
                Console.SetCursorPosition(ConsoleSettings.RIGHT_MENU_WIDTH, ConsoleSettings.UP_MENU_HEIGHT + y + 1);
            }
        }

        // Отрисовка зафиксированных изменений
        public static void SoftDrawMap(BaseMap map)
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (map.Retouch(x, y))
                    {
                        Console.SetCursorPosition(ConsoleSettings.RIGHT_MENU_WIDTH + x, ConsoleSettings.UP_MENU_HEIGHT + y);
                        using (new Colorife(map[x, y]))
                        {
                            Console.Write(map[x, y].Char);
                        }
                    }
                }
            }
        }

        // Примеры цветов
        public static void PrintSample()
        {
            for (int l = 0; l < 4; l++)
            {
                for (int c = 0; c < 16; c++)
                {
                    using (new Colorife((ConsoleColor)c))
                    {
                        for (int w = 0; w < 5; w++)
                        {
                            Console.Write(".");
                        }
                    }
                }
                Console.WriteLine();
            }
            for (int c = 0; c < 16; c++)
            {
                var color = (ConsoleColor)c;
                Console.WriteLine(color.ToString());
            }
        }
    }
}
