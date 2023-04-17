int height = Console.BufferHeight - 1;
int width = Console.BufferWidth / 2;

bool[,] cells = new bool[height, width];
Console.CursorVisible = false;

for (int top = 0; top < height; ++top)
{
    for (int left = 0; left < width; ++left)
    {
        bool value = Convert.ToBoolean(Random.Shared.Next(2));
        cells[top, left] = value;
        Console.BackgroundColor = value ? ConsoleColor.Black : ConsoleColor.White;
        Console.SetCursorPosition(left * 2, top);
        Console.WriteLine("  ");
    }
}

while (true)
{
    int change = 0;
    bool[,] cache = (bool[,])cells.Clone();
    for (int top = 0; top < height; ++top)
    {
        for (int left = 0; left < width; ++left)
        {
            int aliveAround = 0;
            for (int roundTop = top - (top > 0 ? 1 : 0); roundTop < top + (top < height - 1 ? 2 : 1); ++roundTop)
            {
                for (int roundLeft = left - (left > 0 ? 1 : 0); roundLeft < left + (left < width - 1 ? 2 : 1); roundLeft += roundTop == top ? 2 : 1)
                {
                    if (!cells[roundTop, roundLeft])
                    {
                        continue;
                    }
                    ++aliveAround;
                }
            }
            if (cells[top, left])
            {
                if (aliveAround is < 2 or > 3)
                {
                    cache[top, left] = false;
                    Console.SetCursorPosition(left * 2, top);
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine("  ");
                    ++change;
                }
            }
            else if (aliveAround is 3)
            {
                cache[top, left] = true;
                Console.SetCursorPosition(left * 2, top);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("  ");
                ++change;
            }
        }
    }
    cells = cache;
    if (change <= 0)
    {
        break;
    }
    Thread.Sleep(50);
}
