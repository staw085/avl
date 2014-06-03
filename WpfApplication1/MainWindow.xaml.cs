using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;

namespace WpfApplication1 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {
        Board board;
        Random random = new Random();
        Rectangle[,] rectangles = new Rectangle[50, 50];
        bool animationStart = true;
        int preyInc = 4;
        int predInc = 80;
        int predDec = 6;
        int predAcc = 70;
        System.ComponentModel.BackgroundWorker MyWorker = new System.ComponentModel.BackgroundWorker();

        public MainWindow() {
            InitializeComponent();
            slider1.Maximum = 100;
            slider1.Value = 2;
            slider2.Maximum = 100;
            slider2.Value = 20;
            board = new Board();
            setNewBoard();
            addBoardToCanvas();
            board.setClass((int) slider2.Value *50, 1);
            board.setClass((int) slider1.Value *50, 2);
            MyWorker.DoWork += startAnimation;
        }


        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
        }
        private void addBoardToCanvas() {
            for( int j = 0; j < 50; j++ ) {
                for( int i = 0; i < 50; i++ ) {
                    canvas1.Children.Add(rectangles[j, i]);
                }
            }
        }


        private void updateBoard(Board b) {
            board = b;
            for( int j = 0; j < 50; j++ ) {
                for( int i = 0; i < 50; i++ ) {
                    setColor(board.getCells()[j, i].getType(), rectangles[j, i]);
                }
            }
        }

        private bool checkField(Board b, int x, int y) {
            if( b.getCells()[x, y].getType() == 0 ) {
                return true;
            } else {
                return false;
            }
        }

        public void setNewBoard() {
            for( int j = 0; j < 50; j++ ) {
                for( int i = 0; i < 50; i++ ) {
                    rectangles[j, i] = new Rectangle() {
                        Stroke = Brushes.White,
                        Fill = Brushes.White,
                        Width = 10,
                        Height = 10,
                        Margin = new Thickness(left : i*10, top : j*10, right : 0, bottom : 0),
                    };
                }
            }
        }

        public void setColor(int type, Rectangle rctgl) {
            switch( type ) {
                case 0:
                    rctgl.Stroke = Brushes.White;
                    rctgl.Fill = Brushes.White;
                    break;
                case 1:
                    rctgl.Stroke = Brushes.Gray;
                    rctgl.Fill = Brushes.Gray;
                    break;
                case 2:
                    rctgl.Stroke = Brushes.Red;
                    rctgl.Fill = Brushes.Red;
                    break;
            }
        }

        public void startAnimation(object Sender, System.ComponentModel.DoWorkEventArgs e) {
            Board b2 = (Board) e.Argument;
            while( animationStart ) {
                for( int j = 0; j < 50; j++ ) {
                    for( int i = 0; i < 50; i++ ) {
                        if( b2.getCells()[j, i].getType() != 0 ) {
                            int direction = random.Next(4);
                            int tmp;
                            switch( direction ) {
                                case 0:
                                    tmp = j-1;
                                    if( tmp >= 0 ) {
                                        if( checkField(b2, tmp, i) ) {
                                            b2.getCells()[tmp, i].setType(b2.getCells()[j, i].getType());
                                            b2.getCells()[j, i].setType(0);
                                        }
                                    }
                                    break;
                                case 1:
                                    tmp = i+1;
                                    if( tmp <50 ) {
                                        if( checkField(b2, j, tmp) ) {
                                            b2.getCells()[j, tmp].setType(b2.getCells()[j, i].getType());
                                            b2.getCells()[j, i].setType(0);
                                        }
                                    }
                                    break;
                                case 2:
                                    tmp = j+1;
                                    if( tmp <50 ) {
                                        if( checkField(b2, tmp, i) ) {
                                            b2.getCells()[tmp, i].setType(b2.getCells()[j, i].getType());
                                            b2.getCells()[j, i].setType(0);
                                        }
                                    }
                                    break;
                                case 3:
                                    tmp = i-1;
                                    if( tmp >= 0 ) {
                                        if( checkField(b2, j, tmp) ) {
                                            b2.getCells()[j, tmp].setType(b2.getCells()[j, i].getType());
                                            b2.getCells()[j, i].setType(0);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
                this.InvokeIfRequired(() => updateBoard(b2));
                Thread.Sleep(2000);
            }
        }

        private void checkNearness(Board b, int j, int i, int dir) {
            if( b.getCells()[j, i].getType() == 1 ) {

            } else if( b.getCells()[j, i].getType() == 2 ) {

            }
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            animationStart = true;
            MyWorker.RunWorkerAsync(board);
        }



        private void button2_Click(object sender, RoutedEventArgs e) {
            //board.setNewBoard();
            //addBoardToCanvas();
            //board.setClass((int) slider2.Value *100, 1);
            //board.setClass((int) slider1.Value *100, 2);
        }

        private void button3_Click(object sender, RoutedEventArgs e) {
            //MyWorker.CancelAsync();\
            animationStart = false;
        }
    }

    public static class ControlExtensions {
        public static void InvokeIfRequired(this Control control, Action action) {
            if( System.Threading.Thread.CurrentThread!=control.Dispatcher.Thread )
                control.Dispatcher.Invoke(action);
            else
                action();
        }
        public static void InvokeIfRequired<T>(this Control control, Action<T> action, T parameter) {
            if( System.Threading.Thread.CurrentThread!=control.Dispatcher.Thread )
                control.Dispatcher.Invoke(action, parameter);
            else
                action(parameter);
        }
    }

}