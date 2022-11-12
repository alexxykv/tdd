﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public Point Center { get; }
        public List<Rectangle> Rectangles { get; }
        public IDistribution Distribution { get; }

        public CircularCloudLayouter(Point center)
        {
            Center = center;
            Rectangles = new List<Rectangle>();
            Distribution = new ArchimedeanSpiral(center);
        }
        
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width < 0 || rectangleSize.Height < 0)
                throw new ArgumentException("Height and width must be positive");

            foreach (var point in Distribution.GetPoints())
            {
                var location = new Point(new Size(point) - (rectangleSize / 2));
                var rectangle = new Rectangle(location, rectangleSize);
                
                if (Rectangles.All(rect => !rect.IntersectsWith(rectangle)))
                {
                    Rectangles.Add(rectangle);
                    return rectangle;
                }
            }

            return new Rectangle();
        }
    }
}