#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using CyuubiApps.Engine.StateManager;
#endregion

namespace MRoyale.GameStates.World
{
    public interface IWorldState : IGameState
    { }

    public class WorldState : BaseGameState, IWorldState
    {
        #region Field Region

        private KeyboardState _previousState;

        private Texture2D _player;
        private Vector2 _playerPosition = Vector2.Zero;
        private Vector2 _playerVelocity = new Vector2(2, 2);
        private Vector2 _playerGravity = new Vector2(0, -2);

        private float _previousTime;
        private float _currentTime;

        #endregion

        #region Constructor Region

        public WorldState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IWorldState), this);
        }

        #endregion

        #region Method Region

        public override void Initialize()
        {
            _previousState = Keyboard.GetState();

            _player = content.Load<Texture2D>("mario-jumping");

            base.Initialize();
        }

        private void OnPadClick(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Clicked!");
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Z) & !_previousState.IsKeyDown(Keys.Z))
            {
                // ...
            }

            _previousState = state;

            _previousTime = _currentTime;
            _currentTime = (float)gameTime.TotalGameTime.TotalMilliseconds;

            float dt = _currentTime - _previousTime;
            if (dt > 0.15f) dt = 0.15f;

            _playerPosition = _playerPosition + _playerVelocity * dt;
            _playerVelocity = _playerVelocity + _playerGravity * dt;

            base.Update(gameTime);
        }

        public override void FixedUpdate(float updateTime)
        {
            // ...

            base.FixedUpdate(updateTime);
        }

        public override void Draw(GameTime gameTime)
        {
            gameBase.SpriteBatch.Draw(_player, _playerPosition, null, Color.White, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0);

            base.Draw(gameTime);
        }

        #endregion
    }
}
