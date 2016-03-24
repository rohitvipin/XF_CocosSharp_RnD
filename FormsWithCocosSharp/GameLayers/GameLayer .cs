using System.Collections.Generic;
using CocosSharp;

namespace FormsWithCocosSharp.GameLayers
{
    public class GameLayer : CCLayer
    {
        public GameLayer()
        {
            // "paddle" refers to the paddle.png image
            var paddleSprite = new CCSprite("paddle")
            {
                PositionX = 100,
                PositionY = 100
            };
            AddChild(paddleSprite);
        }
        protected override void AddedToScene()
        {
            base.AddedToScene();
            // Use the bounds to layout the positioning of our drawable assets
            CCRect bounds = VisibleBoundsWorldspace;
            // Register for touch events
            var touchListener = new CCEventListenerTouchAllAtOnce
            {
                OnTouchesEnded = OnTouchesEnded
            
            };
            AddEventListener(touchListener, this);
        }
        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                // Perform touch handling here
            }
        }
    }
}