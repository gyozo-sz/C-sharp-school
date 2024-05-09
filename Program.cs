namespace MyApp
{
    internal class Program
    {
        class Shape
        {
            private double _width;
            private double _height;

            public double Width { get => _width; }
            public double Height { get => _height; }

            public Shape(double width, double height)
            {
                _width = width;
                _height = height;
            }

            public void GetArea()
            {
                Console.WriteLine($"Shape.GetArea() was called with result: {_width * _height}\n");
            }
        }

        class Rectangle : Shape
        {
            public Rectangle(double width, double height) : base(width, height) { }

            new public void GetArea()
            {
                Console.WriteLine($"Rectangle.GetArea() was called with result: {Width * Height}\n");
            }
        }

        static void Main()
        {
            Shape shape = new(10, 20);
            Rectangle rectangle = new(5, 30);

            shape.GetArea();
            rectangle.GetArea();

            Console.WriteLine("\n");

            Console.WriteLine("if Rectangle is a Shape GetArea():");
            if (rectangle is Shape shapeConverted) {
                shapeConverted.GetArea();
            }

            Console.WriteLine("if Shape is a Rectangle GetArea() (attempt):");
            if (shape is Rectangle rectangleConverted)
            {
                rectangleConverted.GetArea();
            }
            else
            {
                Console.WriteLine("Shape is not (necessarily) a Rectangle.\n");
            }

            Console.WriteLine("Rectangle as a Shape GetArea():");
            var rectangleAsShape = rectangle as Shape;
            if (rectangleAsShape != null)
            {
                rectangleAsShape.GetArea();
            }
            

            Console.WriteLine("Shape as a Rectangle GetArea() (attempt):");
            var shapeAsRectangle = shape as Rectangle;
            if (shapeAsRectangle != null)
            {
                shapeAsRectangle.GetArea();
            }
            else
            {
                Console.WriteLine("Shape cannot be used as a Rectangle.\n");
            }


            Console.WriteLine("Object type casts:");
            Object shapeObject = new Shape(20, 30);
            Object rectangleObject = new Rectangle(30, 50);

            Console.WriteLine("shapeObject is {0}a Shape", shapeObject is Shape ? "" : "NOT ");
            Console.WriteLine("rectangleObject is {0}a Shape", rectangleObject is Shape ? "" : "NOT ");
            Console.WriteLine("shapeObject is {0}a Rectangle", shapeObject is Rectangle ? "" : "NOT ");
            Console.WriteLine("rectangleObject is {0}a Rectangle", rectangleObject is Rectangle ? "" : "NOT ");
            Console.WriteLine("\n");


            var shapeObjectAsShape = shapeObject as Shape;
            if (shapeObjectAsShape == null)
            {
                Console.WriteLine("shapeObject can not be used as a Shape.");
            } else
            {
                Console.Write("Calling Shape's GetArea() on shapeObject: ");
                shapeObjectAsShape.GetArea();
            }

            var rectangleObjectAsShape = rectangleObject as Shape;
            if (rectangleObjectAsShape == null)
            {
                Console.WriteLine("rectangleObject can not be used as a Shape.");
            }
            else
            {
                Console.Write("Calling Shape's GetArea() on rectangleObject: ");
                rectangleObjectAsShape.GetArea();
            }

            var shapeObjectAsRectangle = shapeObject as Rectangle;
            if (shapeObjectAsRectangle == null)
            {
                Console.WriteLine("shapeObject can not be used as a Shape.");
            }
            else
            {
                Console.Write("Calling Rectangle's GetArea() on shapeObject: ");
                shapeObjectAsRectangle.GetArea();
            }

            var rectangleObjectAsRectangle = rectangleObject as Rectangle;
            if (rectangleObjectAsRectangle == null)
            {
                Console.WriteLine("rectangleObject can not be used as a Shape.");
            }
            else
            {
                Console.Write("Calling Rectangle's GetArea() on rectangleObject: ");
                rectangleObjectAsRectangle.GetArea();
            }
        }
    }
}