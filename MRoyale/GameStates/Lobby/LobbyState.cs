#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using CyuubiApps.Engine.StateManager;
using System;
#endregion

namespace MRoyale.GameStates.Lobby
{
    public interface ILobbyState : IGameState
    { }

    public class LobbyState : BaseGameState, ILobbyState
    {
        #region Field Region

        private KeyboardState _previousState;
        private PlayerPad _playerPad;

        private bool _characterToggle;

        #endregion

        #region Constructor Region

        public LobbyState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(ILobbyState), this);
        }

        #endregion

        #region Method Region

        public override void Initialize()
        {
            _previousState = Keyboard.GetState();
            _playerPad = new PlayerPad(gameBase); // TODO: Should be here?

            _playerPad.OnPadClick += OnPadClick;
            _playerPad.Position = new Vector2(250, 100);
            _playerPad.Scale = 8;

            base.Initialize();
        }

        private void OnPadClick(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Clicked!");
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Space) & !_previousState.IsKeyDown(Keys.Space))
            {
                if (!_characterToggle) _playerPad.ChangeCharacter("mario");
                else _playerPad.ChangeCharacter("paddler");

                _characterToggle = !_characterToggle;
            }

            _previousState = state;

            base.Update(gameTime);
        }

        public override void FixedUpdate(float updateTime)
        {
            _playerPad.Update(updateTime);

            base.FixedUpdate(updateTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _playerPad.Draw(gameTime);

            base.Draw(gameTime);
        }

        #endregion
    }
}
