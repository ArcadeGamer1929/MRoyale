#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

using CyuubiApps.Engine.StateManager;
#endregion

namespace MRoyale.GameStates.Lobby
{
    public interface ILobbyState : IGameState
    { }

    public class LobbyState : BaseGameState, ILobbyState
    {
        #region Field Region

        private KeyboardState _previousState;
        private PlayerPad[] _playerPads;

        //private SoundEffect _playerPadChange;

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
            _playerPads = new PlayerPad[4];

            var scale = 4;
            var x = 25 * scale;
            for (var index = 0; index < _playerPads.Length; index++)
            {
                _playerPads[index] = new PlayerPad(gameBase);

                var playerPad = _playerPads[index];

                playerPad.OnPadClick += OnPadClick;
                playerPad.Position = new Vector2(x, 50 * scale);
                playerPad.Scale = scale;

                x += 40 * scale;
            }

            //_playerPadChange = content.Load<SoundEffect>("player-pad-change");

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
                //_playerPadChange.Play();

                var playerPad = _playerPads[0];

                if (!_characterToggle) playerPad.ChangeCharacter("mario");
                else playerPad.ChangeCharacter("paddler");

                _characterToggle = !_characterToggle;
            }

            if (state.IsKeyDown(Keys.X) & !_previousState.IsKeyDown(Keys.X))
            {
                //_playerPadChange.Play();

                var playerPad = _playerPads[1];

                if (!_characterToggle) playerPad.ChangeCharacter("mario");
                else playerPad.ChangeCharacter("luigi");

                _characterToggle = !_characterToggle;
            }

            if (state.IsKeyDown(Keys.C) & !_previousState.IsKeyDown(Keys.C))
            {
                //_playerPadChange.Play();

                var playerPad = _playerPads[2];

                if (!_characterToggle) playerPad.ChangeCharacter("mario");
                else playerPad.ChangeCharacter("paddler");

                _characterToggle = !_characterToggle;
            }

            if (state.IsKeyDown(Keys.V) & !_previousState.IsKeyDown(Keys.V))
            {
                //_playerPadChange.Play();

                var playerPad = _playerPads[3];

                if (!_characterToggle) playerPad.ChangeCharacter("mario");
                else playerPad.ChangeCharacter("luigi");

                _characterToggle = !_characterToggle;
            }

            _previousState = state;

            base.Update(gameTime);
        }

        public override void FixedUpdate(float updateTime)
        {
            for (var index = 0; index < _playerPads.Length; index++)
            {
                var playerPad = _playerPads[index];
                if (playerPad != null) playerPad.Update(updateTime);
            }

            base.FixedUpdate(updateTime);
        }

        public override void Draw(GameTime gameTime)
        {
            for (var index = 0; index < _playerPads.Length; index++)
            {
                var playerPad = _playerPads[index];
                if (playerPad != null) playerPad.Draw(gameTime);
            }

            base.Draw(gameTime);
        }

        #endregion
    }
}
