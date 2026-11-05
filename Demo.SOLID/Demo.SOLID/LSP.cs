using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.SOLID
{
    public interface IGeometry
    {
        double GetArea();
    }
    public class Rectangle :IGeometry
	{
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public double GetArea()
        {
            throw new NotImplementedException();
        }
    }

    //Kare, dikdörtgendan miras ALAMAZ!!! Çünkü isikisi yazılımsal olarak (property'lere dikkat!) birbirlerinden tamamen bağımsızdır.
    public class SquareBefore: Rectangle
    {
        public override int Width { get => base.Width; set { base.Width = value; base.Height = value; } }
        public override int Height { get => base.Height; set { base.Width = value; base.Height = value; } }

    }

    public class SquareAfter : IGeometry
    {
        public int EdgeLength { get; set; }

        public double GetArea()
        {
            throw new NotImplementedException();
        }
    }
}
