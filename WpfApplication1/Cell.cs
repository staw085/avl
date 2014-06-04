using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace WpfApplication1 {
    class Cell {
        int x, y;
        int type;

        public Cell(int type, int x, int y) {
            this.type = type;
            this.x = x;
            this.y = y;
        }

        public int getType() {
            return type;
        }

        public void setType(int c) {
            this.type = c;
        }
        public int getX() {
            return x;
        }
        public int getY() {
            return y;
        }
    }
}
