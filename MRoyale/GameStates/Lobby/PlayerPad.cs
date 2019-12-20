using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MRoyale
{
    /*
     * TODO: Move to sprite class
     */
    class PlayerPad
    {
        private enum PlayerStatus
        {
            None,
            Falling,
            Jumping,
        }

        public delegate void OnPadClickHandler(object sender, EventArgs e);

        public event OnPadClickHandler OnPadClick;

        public Vector2 Position = Vector2.Zero;
        public int Scale = 1;

        private GameBase _gameBase;

        private MouseState _previousMouseState;

        private bool _playerPadHover;

        private Texture2D _playerPad;
        private Texture2D _playerPadLighting;

        private PlayerStatus _playerStatus = PlayerStatus.None;

        private Vector2 _playerPosition = Vector2.Zero;
        private Vector2 _playerTransform = Vector2.Zero;

        private Texture2D _playerNormal;
        private Texture2D _playerJumping;

        private Texture2D _player;

        public PlayerPad(GameBase gameBase)
        {
            _gameBase = gameBase;

            _previousMouseState = Mouse.GetState();

            _playerPad = _gameBase.Content.Load<Texture2D>("player-pad");
            _playerPadLighting = _gameBase.Content.Load<Texture2D>("player-pad-lighting");

            _playerNormal = new Texture2D(_gameBase.GraphicsDevice, 1, 1);
            _playerJumping = new Texture2D(_gameBase.GraphicsDevice, 1, 1);

            _player = _playerNormal;
        }

        // TODO: Not final character implementation!
        public void ChangeCharacter(string character)
        {
            _playerStatus = PlayerStatus.Jumping;

            if (character != string.Empty)
            {
                _playerNormal = _gameBase.Content.Load<Texture2D>($"{character}-normal");
                _playerJumping = _gameBase.Content.Load<Texture2D>($"{character}-jumping");

                if ((_playerNormal.Width > 16 && _playerNormal.Height > 16 && _playerNormal.Width < 16 && _playerNormal.Height < 16) ||
                    (_playerJumping.Width > 16 && _playerJumping.Width > 16 && _playerJumping.Width < 16 && _playerJumping.Height < 16))
                    throw new Exception("Character is too large or too small!");

                _playerPosition.Y = -10 * Scale;

                _playerTransform.X = 8 * Scale;
                _playerTransform.Y = 12 * Scale;

                _player = _playerNormal;
            }
            else
            {
                _playerNormal = new Texture2D(_gameBase.GraphicsDevice, 1, 1);
                _playerJumping = new Texture2D(_gameBase.GraphicsDevice, 1, 1);

                _player = _playerNormal;
            }
        }

        public void Update(float updateTime)
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            var lobbyPadRectangle = new Rectangle((int)Position.X, (int)Position.Y, _playerPad.Width * Scale, _playerPad.Height * Scale);
            if (lobbyPadRectangle.Contains(mousePoint))
            {
                _playerPadHover = true;

                if (mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton != ButtonState.Pressed)
                    OnPadClick?.Invoke(this, EventArgs.Empty);
            }
            else _playerPadHover = false;

            if (_playerStatus == PlayerStatus.Jumping)
            {
                _player = _playerJumping;

                if (_playerPosition.Y <= -20 * Scale)
                {
                    _playerPosition.Y = -20 * Scale;
                    _playerStatus = PlayerStatus.Falling;
                }
                else _playerPosition.Y -= 4 * Scale;
            }
            else if (_playerStatus == PlayerStatus.Falling)
            {
                _player = _playerJumping;

                if (_playerPosition.Y >= 0)
                {
                    _playerPosition.Y = 0;
                    _playerStatus = PlayerStatus.None;
                }
                else _playerPosition.Y += 2 * Scale;
            }
            else _player = _playerNormal;

            _previousMouseState = mouseState;
        }

        public void Draw(GameTime gameTime)
        {
            _gameBase.SpriteBatch.Draw(_playerPad, Position, null, Color.White, 0, Vector2.Zero, new Vector2(Scale, Scale), SpriteEffects.None, 0);
            _gameBase.SpriteBatch.Draw(_playerPadLighting, Position, null, Color.White, 0, Vector2.Zero, new Vector2(Scale, Scale), SpriteEffects.None, 0);
            if (_playerPadHover) _gameBase.SpriteBatch.Draw(_playerPadLighting, Position, null, Color.White, 0, Vector2.Zero, new Vector2(Scale, Scale), SpriteEffects.None, 0);
            _gameBase.SpriteBatch.Draw(_player, Position + _playerPosition + _playerTransform, null, Color.White, 0, Vector2.Zero, new Vector2(Scale, Scale), SpriteEffects.None, 0);
        }
    }
}
