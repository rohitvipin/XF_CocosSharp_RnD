using System;
using CocosSharp;

namespace FormsWithCocosSharp
{
    public class GameOverLayer : CCLayerColor
    {
        readonly string _scoreMessage;

        public GameOverLayer(int score)
        {
            _scoreMessage = $"Game Over. You scored {score}!";
        }
        
        protected override void AddedToScene()
        {
            base.AddedToScene();

            var scoreLabel = new CCLabel(_scoreMessage, "arial", 22)
            {
                Position = new CCPoint(VisibleBoundsWorldspace.Size.Center.X, VisibleBoundsWorldspace.Size.Center.Y + 50),
                Color = new CCColor3B(CCColor4B.Yellow),
                HorizontalAlignment = CCTextAlignment.Center,
                VerticalAlignment = CCVerticalTextAlignment.Center,
                AnchorPoint = CCPoint.AnchorMiddle,
                Dimensions = ContentSize
            };

            AddChild(scoreLabel);

            var playAgainLabel = new CCLabel("Tap to Play Again", "arial", 22)
            {
                Position = VisibleBoundsWorldspace.Size.Center,
                Color = new CCColor3B(CCColor4B.Green),
                HorizontalAlignment = CCTextAlignment.Center,
                VerticalAlignment = CCVerticalTextAlignment.Center,
                AnchorPoint = CCPoint.AnchorMiddle,
                Dimensions = ContentSize
            };

            AddChild(playAgainLabel);
        }

        public static CCScene SceneWithScore(CCScene gameScene, int score)
        {
            var scene = new CCScene(gameScene);
            var layer = new GameOverLayer(score);

            scene.AddChild(layer);

            return scene;
        }
    }
}