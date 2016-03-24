using System.Collections.Generic;
using CocosSharp;

namespace FormsWithCocosSharp
{
    public class GameLayer : CCLayerColor
    {
        private readonly CCScene _gameScene;
        private readonly CCSprite _paddleSprite;
        private readonly CCSprite _ballSprite;
        private readonly CCLabel _scoreLabel;

        private float _ballXVelocity;
        private float _ballYVelocity;

        // How much to modify the ball's y velocity per second:
        private const float Gravity = 140;

        private int _score;

        public GameLayer(CCScene gameScene) : base(CCColor4B.Gray)
        {
            _gameScene = gameScene;

            // "paddle" refers to the paddle.png image
            _paddleSprite = new CCSprite("paddle")
            {
                PositionX = 100,
                PositionY = 100
            };
            AddChild(_paddleSprite);

            _ballSprite = new CCSprite("ball")
            {
                PositionX = 320,
                PositionY = 600
            };
            AddChild(_ballSprite);

            _scoreLabel = new CCLabel("Score: 0", "Arial", 40, CCLabelFormat.SystemFont)
            {
                PositionX = 50,
                PositionY = 1000,
                AnchorPoint = CCPoint.AnchorUpperLeft,
                Color = CCColor3B.Orange
            };

            AddChild(_scoreLabel);

            Schedule(RunGameLogic);

            Schedule(t =>
            {
                if (_ballSprite.PositionY < _paddleSprite.PositionY)
                {
                    var gameOverScene = GameOverLayer.SceneWithScore(_gameScene, _score);
                    var transitionToGameOver = new CCTransitionMoveInR(0.3f, gameOverScene);
                    Director.ReplaceScene(transitionToGameOver);
                }
            });
        }

        private void RunGameLogic(float frameTimeInSeconds)
        {
            // This is a linear approximation, so not 100% accurate
            _ballYVelocity += frameTimeInSeconds * -Gravity;
            _ballSprite.PositionX += _ballXVelocity * frameTimeInSeconds;
            _ballSprite.PositionY += _ballYVelocity * frameTimeInSeconds;
            // New Code:
            // Check if the two CCSprites overlap...
            var doesBallOverlapPaddle = _ballSprite.BoundingBoxTransformedToParent.IntersectsRect(_paddleSprite.BoundingBoxTransformedToParent);
            // ... and if the ball is moving downward.
            var isMovingDownward = _ballYVelocity < 0;
            if (doesBallOverlapPaddle && isMovingDownward)
            {
                // First let's invert the velocity:
                _ballYVelocity *= -1;
                // Then let's assign a random to the ball's x velocity:
                const float minXVelocity = -300;
                const float maxXVelocity = 300;
                _ballXVelocity = CCRandom.GetRandomFloat(minXVelocity, maxXVelocity);
                // New code:
                _score++;
                _scoreLabel.Text = "Score: " + _score;
            }
            // First let’s get the ball position:   
            var ballRight = _ballSprite.BoundingBoxTransformedToParent.MaxX;
            var ballLeft = _ballSprite.BoundingBoxTransformedToParent.MinX;
            // Then let’s get the screen edges
            var screenRight = VisibleBoundsWorldspace.MaxX;
            var screenLeft = VisibleBoundsWorldspace.MinX;

            // Check if the ball is either too far to the right or left:    
            var shouldReflectXVelocity =
                (ballRight > screenRight && _ballXVelocity > 0) ||
                (ballLeft < screenLeft && _ballXVelocity < 0);

            if (shouldReflectXVelocity)
            {
                _ballXVelocity *= -1;
            }
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Register for touch events
            var touchListener = new CCEventListenerTouchAllAtOnce
            {
                OnTouchesEnded = OnTouchesEnded,
                OnTouchesMoved = HandleTouchesMoved
            };
            AddEventListener(touchListener, this);
        }

        private void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                // Perform touch handling here
                var locationOnScreen = touches[0].Location;
                var moveSprite = new CCMoveTo(0.5f, new CCPoint(locationOnScreen.X, _paddleSprite.PositionY));
                _paddleSprite.RunAction(moveSprite);
            }
        }

        private void HandleTouchesMoved(List<CCTouch> touches, CCEvent touchEvent)
        {
            // we only care about the first touch:
            var locationOnScreen = touches[0].Location;
            _paddleSprite.PositionX = locationOnScreen.X;
        }
    }
}