int edge = 40;
bool[,] cells = new bool[edge, edge];
int gen = 0;
Console.CursorVisible = false;

Random random = new();
for (int top = 0; top < edge; ++top)
{
    for (int left = 0; left < edge; ++left)
    {
        bool value = Convert.ToBoolean(random.Next(2));
        cells[top, left] = value;
        Console.BackgroundColor = value ? ConsoleColor.Black : ConsoleColor.White;
        Console.SetCursorPosition(left * 2, top);
        Console.WriteLine("  ");
    }
}

while (true)
{
    Console.Title = gen++.ToString();
    Thread.Sleep(50);
    bool[,] cache = (bool[,])cells.Clone();
    int change = 0;
    for (int top = 0; top < edge; ++top)
    {
        for (int left = 0; left < edge; ++left)
        {
            int aliveRound = 0;
            for (int roundTop = top - (top > 0 ? 1 : 0); roundTop < top + (top < edge - 1 ? 2 : 1); ++roundTop)
            {
                for (int roundLeft = left - (left > 0 ? 1 : 0); roundLeft < left + (left < edge - 1 ? 2 : 1); roundLeft += roundTop == top ? 2 : 1)
                {
                    if (!cells[roundTop, roundLeft])
                    {
                        continue;
                    }
                    ++aliveRound;
                }
            }
            if (cells[top, left])
            {
                if (aliveRound is < 2 or > 3)
                {
                    cache[top, left] = false;
                    Console.SetCursorPosition(left * 2, top);
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine("  ");
                    ++change;
                }
            }
            else if (aliveRound is 3)
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
}
