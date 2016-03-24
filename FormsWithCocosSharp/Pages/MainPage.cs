using System;
using CocosSharp;
using Xamarin.Forms;

namespace FormsWithCocosSharp.Pages
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            var gameView = new CocosSharpView
            {
                // Notice it has the same properties as other XamarinForms Views
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                // This gets called after CocosSharp starts up:
                ViewCreated = OnViewCreated
            };
            Content = gameView;
        }

        // LoadGame is called when CocosSharp is initialized. We can begin creating
        // our CocosSharp objects here:
        private void OnViewCreated(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            if (gameView == null)
            {
                return;
            }

            // Set world dimensions
            gameView.DesignResolution = new CCSizeI(768, 1027);

            var gameScene = new CCScene(gameView);

            gameScene.AddLayer(new GameLayer(gameScene));
            gameView.RunWithScene(gameScene);
        }
    }
}


