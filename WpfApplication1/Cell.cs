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

        public Cell( int type) {
            this.type = type;
        }

        public int getType() {
            return type;
        }

        public void setType(int c) {
            this.type = c;
           // setColor();
        }

    }
}
