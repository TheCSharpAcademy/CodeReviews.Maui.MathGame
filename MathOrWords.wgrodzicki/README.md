# **Math or Words**

This is "Math or Words", my first project made with .NET MAUI. It's a desktop application for Windows that let's you play a game to test your skills with either math or words.

![Menu sample](MathOrWords/Resources/Images/main_menu.PNG)

## **Technical side of things**

The application is written with C# and XAML, using the .NET MAUI framework and SQLite for database management.

## **Features**

#### Math mode

Math mode consists in solving randomly generated equations (addition, subtraction, multiplication, division).

![Math sample](MathOrWords/Resources/Images/math_game.PNG)

#### Math mode

Words mode consists in forming words within the given constraints (inclusion/exclusion of letters, specific letter at the beginning/at the end of the word).

![Words sample](MathOrWords/Resources/Images/words_game.PNG)

In addition I also implemented:

- score and attempts counter
- score saving and browsing
- option to delete selected scores

## **Main challenges**

When designing and implementing the project I stumbled upon several challenges:

- _MAUI framework_. It was my first encounter with the .NET MAUI framework and XAML, so it took me a while to get a grasp of the project structure and how the pages work within the code-behind pattern.

- _Database_. Previously I dealt mostly with raw SQL, so carrying out queries purely with C# and mapping a table to a class was something new to me. I also struggled a bit with finding a proper way to indicate the target folder to store the database through code.

- _Timed events_. I wanted to implement a slight "suspension" when the game was over and first thought of a simple timer. But then I found out about async methods and tasks, so I finally opted for a Delay() method without stopping the whole app, which was a good solution to the problem.

- _App deployment_. I had previous experience with WPF desktop apps, which were built and deployed straightaway through Visual Studio. In MAUI, however, it didn't work this way and I had to manually disable default packaging to be able to deploy and run the final app on my machine.

## **Credits**

- Basic code structure and techniques by [The C# Academy](https://www.youtube.com/watch?v=o81wpRuOGjE&list=PL4G0MUH8YWiAMypwveH2LlLK_o8Jto9CE)
- Fredoka font by Milena Brandão, Hafontia via [Google Fonts](https://fonts.google.com/specimen/Fredoka)
- Indie Flower font by Kimberly Geswein via [Google Fonts](https://fonts.google.com/specimen/Indie+Flower)
- Background by stux via [Pixabay](https://pixabay.com/photos/black-board-traces-of-chalk-school-1072366/)
- Trash bin icon by cybertotte via [Pixabay](https://pixabay.com/vectors/dust-bin-icon-the-trash-can-debris-4875414/)
