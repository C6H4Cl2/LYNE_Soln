using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using LYNE_Soln.Model.Shape;
using LYNE_Soln.Exceptions;
using LYNE_Soln.Model.Factory;

namespace LYNE_Soln.Model
{
    partial class Map
    {
        public bool IsSolvedFlag { get { return isSolvedFlag; } }
        private bool isSolvedFlag;

        public List<string> Solutions { get { return solutions; } }
        private List<string> solutions = new List<string>();

        private Shape.Shape[,] shapes;
        private int col = 0, row = 0;
        private int totalCrossNum = 0;

        private Shape.Shape triStart;
        private Shape.Shape rectStart;
        private Shape.Shape diaStart;

        private Shape.Shape triEnd;
        private Shape.Shape rectEnd;
        private Shape.Shape diaEnd;

        private int triTotalNum = 0;
        private int rectTotalNum = 0;
        private int diaTotalNum = 0;

        public Map(int col, int row, List<Shape.Shape> data)
        {
            this.col = 2 * col - 1;
            this.row = 2 * row - 1;
            shapes = new Shape.Shape[this.col, this.row];

            int c = 0, r = 0;

            // parse data to a map
            foreach (Shape.Shape s in data)
            {
                shapes[c, r] = s;
                s.col = c;
                s.row = r;
                //set start points & end ponts
                totalCrossNum += s.MaxTimeToCross;
                switch (s.ShapeType)
                {
                    case ShapeEnum.Triangle:
                        triTotalNum++;
                        if (s.IsStartPoint)
                        {
                            if (triStart == null)
                                triStart = s;
                            else if (triEnd == null)
                                triEnd = s;
                            else
                                throw new InvalidMapException("More than two triangle start points.");
                        }
                        break;
                    case ShapeEnum.Rectangle:
                        rectTotalNum++;
                        if (s.IsStartPoint)
                        {
                            if (rectStart == null)
                                rectStart = s;
                            else if (rectEnd == null)
                                rectEnd = s;
                            else
                                throw new InvalidMapException("More than two rectangle start points.");
                        }
                        break;
                    case ShapeEnum.Diamond:
                        diaTotalNum++;
                        if (s.IsStartPoint)
                        {
                            if (diaStart == null)
                                diaStart = s;
                            else if (diaEnd == null)
                                diaEnd = s;
                            else
                                throw new InvalidMapException("More than two diamond start points.");
                        }
                        break;
                    default:
                        break;
                }

                //index increment
                if (c < this.col - 1)
                {
                    c += 2;
                }
                else
                {
                    c = 0;
                    r += 2;
                }
            }

            //fill the "null" by Lines, i.e. connent figures
            for (r = 0; r < this.row; r++)
            {
                for (c = 0; c < this.col; c++)
                {
                    if (shapes[c, r] == null)
                    {
                        Shape.Shape l = ShapeFactory.createBy(new LineFactory());
                        l.col = c;
                        l.row = r;
                        shapes[c, r] = l;
                    }
                }
            }
        }
    }

    partial class Map
    {

        private bool checkBeforeSolve()
        {
            if (triStart == null && rectStart == null && diaStart == null)
                throw new InvalidMapException("No start points.");
            if (triStart != null && triEnd == null)
                throw new InvalidMapException("Triangle start points do not match.");
            if (rectStart != null && rectEnd == null)
                throw new InvalidMapException("Rectangle start points do not match.");
            if (diaStart != null && diaEnd == null)
                throw new InvalidMapException("Diamond start points do not match.");
            return true;

        }
        public void solve()
        {
            checkBeforeSolve();

            isSolvedFlag = false;

            Route route = new Route();

            if (triStart != null)
            {
                triStart.Cross();
                route.Add(triStart);
                nextStep(ShapeEnum.Triangle, route);
                triStart.UndoCross();
            }
            else if (rectStart != null)
            {
                rectStart.Cross();
                route.Add(rectStart);
                nextStep(ShapeEnum.Rectangle, route);
                rectStart.UndoCross();
            }
            else if (diaStart != null)
            {
                diaStart.Cross();
                route.Add(diaStart);
                nextStep(ShapeEnum.Diamond, route);
                diaStart.UndoCross();
            }

            isSolvedFlag = true;
        }

        private void nextStep(ShapeEnum currentShape, Route route)
        {
            bool isImpasse = true;

            Shape.Shape lastFigure = route[route.Count - 1];

            //Get the end of one shape
            //First triangle, the nrectangle, then diamond
            if (lastFigure.Equals(triEnd) && route.TriNum == triTotalNum)
            {
                if (rectTotalNum != 0)
                {
                    rectStart.Cross();
                    route.Add(rectStart);
                    nextStep(ShapeEnum.Rectangle,route);
                    route.RemoveLast(); // remove the last one
                    rectStart.UndoCross();
                    return;
                }
                else if (diaTotalNum != 0)
                {
                    diaStart.Cross();
                    route.Add(diaStart);
                    nextStep(ShapeEnum.Diamond, route);
                    route.RemoveLast(); // remove the last one
                    diaStart.UndoCross();
                    return;
                }
                else
                {
                    isImpasse = true;
                }
            }
            else if (lastFigure.Equals(rectEnd) && route.RectNum == rectTotalNum)
            {
                if (diaTotalNum != 0)
                {
                    diaStart.Cross();
                    route.Add(diaStart);
                    nextStep(ShapeEnum.Diamond, route);
                    route.RemoveLast(); // remove the last one
                    diaStart.UndoCross();
                    return;
                }
                else
                {
                    isImpasse = true;
                }
            }
            else if (lastFigure.Equals(diaEnd) && route.DiaNum == diaTotalNum)
            {
                isImpasse = true;
            }
            else
            {
                //Eight Directions
                //left top
                passing(currentShape, route, -2, -2);
                //left
                passing(currentShape, route, -2, 0);
                //left bottom
                passing(currentShape, route, -2, +2);
                //bottom
                passing(currentShape, route, 0, +2);
                //right bottom
                passing(currentShape, route, +2, +2);
                //right
                passing(currentShape, route, +2, 0);
                //right top
                passing(currentShape, route, +2, -2);
                //top
                passing(currentShape, route, 0, -2);

                return;
            }
            //
            if (isImpasse && route.Count == totalCrossNum)
            {
                string result = "";
                foreach (Shape.Shape s in route)
                {
                    if (s.ShapeType != ShapeEnum.Line && s.ShapeType != ShapeEnum.Empty)
                    {
                        if (s.MaxTimeToCross == s.TimeHasBeenCrossed)
                        {
                            result += (s.col / 2 + s.row / 2 * ((this.col + 1) / 2) + 1);
                            //result += String.Format("({0},{1})", f.col / 2 + 1, f.row / 2 + 1);
                            result += " ";

                            if(s == triEnd || s == diaEnd || s == rectEnd)
                            {
                                result += "| ";
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                solutions.Add(result);
                isSolvedFlag = true;
                return;
                //Console.WriteLine(result);
            }



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="route"></param>
        /// <param name="colChange"></param>
        /// <param name="rowChange"></param>
        private void passing(ShapeEnum currentShape, Route route, int colChange, int rowChange)
        {

            if (isSolvedFlag == true)
            {
                return;
            }

            Shape.Shape lastFigure = route[route.Count - 1];

            //check index out of array
            if (lastFigure.col + colChange < 0 ||
                lastFigure.row + rowChange < 0 ||
                lastFigure.col + colChange >= this.col ||
                lastFigure.row + rowChange >= this.row)
            {
                return;
            }


            Shape.Shape targetFigure = shapes[lastFigure.col + colChange, lastFigure.row + rowChange];
            Shape.Shape passingFigure = shapes[lastFigure.col + colChange / 2, lastFigure.row + rowChange / 2];
            if (targetFigure.CanBeCrossed(currentShape) && passingFigure.CanBeCrossed(currentShape))
            {
                passingFigure.Cross();
                targetFigure.Cross();
                route.Add(targetFigure);

                nextStep(currentShape, route);

                passingFigure.UndoCross();
                targetFigure.UndoCross();
                route.RemoveLast();
            }
        }
    }
}
