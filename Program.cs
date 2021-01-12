using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SmallBasic.Library;

namespace TurtleGame
{
    class Program
    {
        static void FieldSettings()
        {
            GraphicsWindow.Height = 500;
            GraphicsWindow.Width = 500;
            GraphicsWindow.BackgroundColor = GraphicsWindow.GetRandomColor();
        }

        static void MoveSettings()
        {
            GraphicsWindow.KeyDown += GraphicsWindow_KeyDown;
            Turtle.PenUp();
            Turtle.Speed = 5;
        }

        static void Main(string[] args)
        {
            //Настройка поля
            FieldSettings();

            //Настройка движения
            MoveSettings();

            //Настройка еды
            int food_height = 15;
            int food_width = 15;
            do
            {
                GraphicsWindow.BrushColor = GraphicsWindow.GetRandomColor();
            } while (GraphicsWindow.BrushColor == GraphicsWindow.BackgroundColor);
            var food = Shapes.AddRectangle(food_width, food_height);
            int food_x = GraphicsWindow.Width / 2;
            int food_y = GraphicsWindow.Height / 2;
            Shapes.Move(food, food_x, food_y);
            
            Random rand = new Random();
            int score = 0;
            int distance = (food_width + food_height) / 2;
            GraphicsWindow.DrawText(GraphicsWindow.Width - 20, GraphicsWindow.Height - 20, score);

            while (true)
            {
                Turtle.Move(distance);

                //Обработка выхода за границы поля (x)
                if (Turtle.X>=GraphicsWindow.Width-0.5)
                {
                    Turtle.X = 0;
                }
                else if (Turtle.X<=0)
                {
                    Turtle.X = GraphicsWindow.Width;
                }

                //Обработка выхода за границы поля (y)
                if (Turtle.Y>=GraphicsWindow.Height-0.5)
                {
                    Turtle.Y = 0;
                }
                else if (Turtle.Y<=0)
                {
                    Turtle.Y = GraphicsWindow.Height;
                }

                //Обработка ловли еды
                if (Turtle.X>=food_x-0.1 && Turtle.X<=food_x+food_width+0.1 && Turtle.Y>=food_y-0.1 && Turtle.Y<=food_y+food_height+0.1)
                {
                    food_x = rand.Next(GraphicsWindow.Width-food_width);
                    food_y = rand.Next(GraphicsWindow.Height-food_width);
                    Shapes.Move(food, food_x, food_y);
                    score++;
                    GraphicsWindow.DrawText(GraphicsWindow.Width - 20, GraphicsWindow.Height - 20, score);
                }
                if (score>0 && score%5==0)
                {
                    Turtle.Speed=6;
                }
                if (score==10)
                {
                    GraphicsWindow.BackgroundColor = GraphicsWindow.GetRandomColor();
                    do
                    {
                        GraphicsWindow.BrushColor = GraphicsWindow.GetRandomColor();
                    } while (GraphicsWindow.BrushColor == GraphicsWindow.BackgroundColor);
                    Turtle.Speed = 7;
                }
            }
        }

        private static void GraphicsWindow_KeyDown()
        {
            if (GraphicsWindow.LastKey=="Up")
            {
                Turtle.Angle = 0;
            }
            else if (GraphicsWindow.LastKey=="Right")
            {
                Turtle.Angle = 90;
            }
            else if (GraphicsWindow.LastKey=="Down")
            {
                Turtle.Angle = 180;
            }
            else if (GraphicsWindow.LastKey=="Left")
            {
                Turtle.Angle = 270;
            }
        }
    }
}
