using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Vafadar_GOL
{
  public class Matter
    {
        public Rectangle Rec { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

         private double worldRowsScale,  worldColsScle;
        int worldRows, worldCols;
        private Canvas MainCanvas;
      

        //public Matter(double worldRows, double worldCols)       
        public Matter(int rows, int cols, Canvas mainCanvas)
        {
            Rec = new Rectangle();
            this.worldRows = rows;
            this.worldCols = cols;
            this.MainCanvas = mainCanvas;
            //this.worldRowsScale = worldRows;//(MainCanvas.ActualHeight / rows)
            //this.worldColsScle = worldCols;//(MainCanvas.ActualWidth / cols)
        }

      

        internal void GoTo(int newRow, int newCol)
        {
            worldRowsScale = (MainCanvas.ActualHeight / worldRows);
            worldColsScle =  (MainCanvas.ActualWidth / worldCols);

            this.Rec.SetValue(Canvas.TopProperty, newRow * worldRowsScale);
            this.Rec.SetValue(Canvas.LeftProperty, newCol * worldColsScle);
            //this.Rec.Fill = Brushes.OrangeRed;
            this.Row = newRow;
            this.Col = newCol;
        }
    }

   
}
