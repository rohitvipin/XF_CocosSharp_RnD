using System;
using CocosSharp;
using Xamarin.Forms;

namespace FormsWithCocosSharp.Pages
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            Content = new CocosSharpView
            {
                // Notice it has the same properties as other XamarinForms Views
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                // This gets called after CocosSharp starts up:
                ViewCreated = OnViewCreated
            };
        }

         //LoadGame is called when CocosSharp is initialized. We can begin creating
         //our CocosSharp objects here:
        private void OnViewCreated(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            if (gameView == null)
            {
                return;
            }

            // Set world dimensions
            gameView.DesignResolution = new CCSizeI(758, 1080);

            var gameScene = new CCScene(gameView);

            gameScene.AddLayer(new GameLayer(gameScene));
            gameView.RunWithScene(gameScene);
        }
    }
}


