﻿#region Using Statements
using static Retyped.dom;
#endregion

namespace MRoyale.Bridge
{
    public class App
    {
        private static GameBase _gameBase;

        public static void Main()
        {
            var canvas = new HTMLCanvasElement();
            canvas.width = 800;
            canvas.height = 480;
            canvas.id = "monogamecanvas";
            document.body.appendChild(canvas);

            _gameBase = new GameBase();
            _gameBase.Run();
        }
    }
}