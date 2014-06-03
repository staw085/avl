using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace WpfApplication1 {
    class Cell {
        int type;
        Rectangle rectangle;

        public Cell( int type, Rectangle rectangle) {
            this.type = type;
            this.rectangle = rectangle;
        }

        public void setColor(){
            switch(type) {
                case 0:
                    rectangle.Stroke = Brushes.White;
                    rectangle.Fill = Brushes.White;
                    break;
                case 1:
                    rectangle.Stroke = Brushes.Green;
                    rectangle.Fill = Brushes.Green;
                    break;
                case 2:
                    rectangle.Stroke = Brushes.Red;
                    rectangle.Fill = Brushes.Red;
                    break;
            }
        }

        public int getType() {
            return type;
        }

        public void setType(int c) {
            this.type = c;
            setColor();
        }

        public Rectangle getRectangle() {
            return rectangle;
        }
    }
}
