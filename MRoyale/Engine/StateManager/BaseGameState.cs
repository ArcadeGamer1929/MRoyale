#region Using Statements
using Microsoft.Xna.Framework;

using MRoyale;
#endregion

namespace CyuubiApps.Engine.StateManager
{
    public class BaseGameState : GameState
    {
        #region Field Region

        protected GameBase gameBase;

        #endregion

        #region Constructor Region

        public BaseGameState(Game game)
            : base(game)
        {
            gameBase = (GameBase)game;
        }

        #endregion

        #region Method Region

        public override void Initialize()
        {
            base.Initialize();
            gameBase.StateManager.LoadComplete();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void FixedUpdate(float updateTime)
        {
            base.FixedUpdate(updateTime);
        }

        public override void Draw(GameTime gameTime)
        {
            gameBase.SpriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion
    }
}
