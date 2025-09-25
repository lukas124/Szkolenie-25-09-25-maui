namespace MauiStart.Components;

public partial class Rectangle : Grid
{  
    public Rectangle()
    {
        AddStyles();

        var baseRectangle = new StackLayout()
        {
            HeightRequest = 40,
            WidthRequest = 40,
            BackgroundColor = Colors.Aqua
        };

        var myRectangle = new StackLayout()
        {
            Style = Resources["rectangleStyle"] as Style
        };
        
        Children.Add(baseRectangle);
        Children.Add(myRectangle);
    }

    void AddStyles()
    {
        var rectangleStyle = new Style(typeof(StackLayout))
        {
            Setters =
            {
                new Setter { Property = BackgroundColorProperty, Value = Colors.DarkGrey },
                new Setter { Property = HeightRequestProperty, Value = 20 },
                new Setter { Property = WidthRequestProperty, Value = 20 }
            }
        };

        Resources = new ResourceDictionary();
        Resources.Add("rectangleStyle", rectangleStyle);
    }
}