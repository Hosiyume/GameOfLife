int height = 15;

bool[,] cells = new bool[height, height];

Random random = new();
for (int top = 0; top < height; ++top)
{
    for (int left = 0; left < height; ++left)
    {
        cells[top, left] = random.Next(3) > 1;
        Console.SetCursorPosition(left * 2, top);
        Console.WriteLine(cells[top, left] ? "XX" : "  ");
    }
}

Console.CursorVisible = false;
while (true)
{
    Thread.Sleep(500);
    for (int top = 0; top < height; ++top)
    {
        for (int left = 0; left < height; ++left)
        {
            int aliveRound = 0;
            for (int roundTop = top - (top > 0 ? 1 : 0); roundTop <= top + (top < height - 1 ? 1 : 0); ++roundTop)
            {
                for (int roundLeft = left - (left > 0 ? 1 : 0); roundLeft <= left + (left < height - 1 ? 1 : 0); ++roundLeft)
                {
                    if (roundTop != top && roundLeft != left && cells[roundTop, roundLeft])
                    {
                        ++aliveRound;
                    }
                }
            }
            if (cells[top, left])
            {
                if (aliveRound is < 2 or > 3)
                {
                    cells[top, left] = false;
                }
            }
            else if (aliveRound > 2)
            {
                cells[top, left] = true;
            }
            Console.SetCursorPosition(left * 2, top);
            Console.WriteLine(cells[top, left] ? "XX" : "  ");
        }
    }
}
