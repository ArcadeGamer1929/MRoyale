﻿#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using CyuubiApps.Engine;
using CyuubiApps.Engine.StateManager;

using MRoyale.GameStates;
#endregion

namespace MRoyale
{
    public class GameBase : Game
    {
        #region Field Region

        /*
         * SpriteBatch & GraphicsDeviceManager
         */

        public GraphicsDeviceManager GraphicsDeviceManager;
        public SpriteBatch SpriteBatch;

        /*
         * Engine
         */

        // Game components
        private PerformanceStatistics _performanceStatistics;
        public GameStateManager StateManager;

        // States
        public ILobby Lobby;

        #endregion

        #region Constructor Region

        public GameBase()
        {
            /*
             * SpriteBatch & GraphicsDeviceManager
             */

            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            /*
             * Engine
             */

            // Initialize game components
            _performanceStatistics = new PerformanceStatistics(this);
            StateManager = new GameStateManager(this);

            // Initialize states
            Lobby = new Lobby(this);

            // Add game components
            Components.Add(_performanceStatistics);
            Components.Add(StateManager);

            StateManager.ChangeState((Lobby)Lobby); // Change state to lobby

            // Enable unlimited FPS, mouse visible, allow user resizing and set title
            IsFixedTimeStep = false;
            IsMouseVisible = true;

            Window.AllowUserResizing = true; // TODO: Improve scaling for this
            Window.Title = "Mario Royale";
        }

        #endregion

        #region Method Region

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            Window.Title = $"Mario Royale - {_performanceStatistics.CurrentFramesPerSecond} FPS";

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            base.Draw(gameTime);
        }

        #endregion
    }
}
