using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace WpfApplication1 {
    class Board {
        Random random = new Random();
        Cell[,] cells = new Cell[100, 100];

        public Board() {
            for( int j = 0; j < 100; j++ ) {
                for( int i = 0; i < 100; i++ ) {
                    cells[j, i] = new Cell(0);
                }
            }
        }

        public void setClass(int amount, int type){
            for( int i=0; i<amount; i++ ) {
                bool empty = true;
                while( empty ) {
                    int x = random.Next(100);
                    int y = random.Next(100);
                    if( cells[x, y].getType() == 0 ) {
                        cells[x, y].setType(type);
                        empty = false;
                    }
                }
            }
        }

        public Cell[,] getCells() {
            return cells;
        }

    }
}
