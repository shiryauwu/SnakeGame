using static System.Console;


namespace SnakeGame
{
    public readonly struct Pixel
    {
        private const char PixelSymbol = '♥';
        public Pixel(int x, int y, ConsoleColor color)
        {
            this.X = x;
            this.Y = y;
            Color = color;
        }
        public int X { get; }
        public int Y { get; }
        public ConsoleColor Color { get; }

        public void Draw()
        {
            ForegroundColor = Color;
            SetCursorPosition(X, Y);
            Write(PixelSymbol);
        }
        
        public void Clear()
        {
            SetCursorPosition(X, Y);

            Write(' ');
        }
      

        
    }
}
