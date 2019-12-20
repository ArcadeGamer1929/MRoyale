#region Using Statements
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
#endregion

namespace CyuubiApps.Engine.StateManager
{
    public interface IGameState
    {
        GameState Tag { get; }
    }

    public abstract partial class GameState : DrawableGameComponent, IGameState
    {
        #region Field Region

        protected GameState tag;

        protected readonly IStateManager manager;

        protected ContentManager content;

        protected readonly List<GameComponent> childComponents;

        private float _updateTimer = 0;

        #endregion

        #region Property Region

        public List<GameComponent> Components
        {
            get { return childComponents; }
        }

        public GameState Tag
        {
            get { return tag; }
        }

        #endregion

        #region Constructor Region

        public GameState(Game game)
            : base(game)
        {
            tag = this;

            childComponents = new List<GameComponent>();
            content = Game.Content;

            manager = (IStateManager)Game.Services.GetService(typeof(IStateManager));
        }

        #endregion

        #region Method Region

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            _updateTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            float updateTime = 1f / 60;

            while (_updateTimer >= updateTime)
            {
                FixedUpdate(updateTime);
                _updateTimer -= updateTime;
            }

            foreach (GameComponent component in childComponents)
            {
                if (component.Enabled)
                    component.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public virtual void FixedUpdate(float updateTime)
        { }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (GameComponent component in childComponents)
            {
                if (component is DrawableGameComponent && ((DrawableGameComponent)component).Visible)
                    ((DrawableGameComponent)component).Draw(gameTime);
            }
        }

        protected internal virtual void StateChanged(object sender, EventArgs e)
        {
            SetVisibility(manager.CurrentState == tag);
        }

        public virtual void SetVisibility(bool isVisible)
        {
            Enabled = isVisible;
            Visible = isVisible;

            foreach (GameComponent component in childComponents)
            {
                component.Enabled = isVisible;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = isVisible;
            }
        }

        #endregion
    }
}
