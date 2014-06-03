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
        bool animationStart = true;
        System.ComponentModel.BackgroundWorker MyWorker = new System.ComponentModel.BackgroundWorker();

        public MainWindow() {
            InitializeComponent();
            slider1.Maximum = 100;
            slider1.Value = 2;
            slider2.Maximum = 100;
            slider2.Value = 2;
            board = new Board();
            board.setNewBoard();
            addBoardToCanvas();
            board.setClass((int) slider2.Value *100, 1);
            board.setClass((int) slider1.Value *100, 2);
            MyWorker.DoWork += startAnimation;
        }


        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
        }
        private void addBoardToCanvas() {
            for( int j = 0; j < 100; j++ ) {
                for( int i = 0; i < 100; i++ ) {
                    canvas1.Children.Add(board.getCells()[j, i].getRectangle());
                }
            }
        }


        private void updateBoard() {
            for( int j = 0; j < 100; j++ ) {
                for( int i = 0; i < 100; i++ ) {
                    if( board.getCells()[j, i].getType() != 0 ) {
                        int direction = random.Next(4);
                        int tmp;
                        switch( direction ) {
                            case 0:
                                tmp = j-1;
                                if( tmp >= 0 ) {
                                    if( checkField(tmp, i) ) {
                                        board.getCells()[tmp, i].setType(board.getCells()[j, i].getType());
                                        board.getCells()[j, i].setType(0);
                                    }
                                }
                                break;
                            case 1:
                                tmp = i+1;
                                if( tmp <100 ) {
                                    if( checkField(j, tmp) ) {
                                        board.getCells()[j, tmp].setType(board.getCells()[j, i].getType());
                                        board.getCells()[j, i].setType(0);
                                    }
                                }
                                break;
                            case 2:
                                tmp = j+1;
                                if( tmp <100 ) {
                                    if( checkField(tmp, i) ) {
                                        board.getCells()[tmp, i].setType(board.getCells()[j, i].getType());
                                        board.getCells()[j, i].setType(0);
                                    }
                                }
                                break;
                            case 3:
                                tmp = i-1;
                                if( tmp >= 0 ) {
                                    if( checkField(j, tmp) ) {
                                        board.getCells()[j, tmp].setType(board.getCells()[j, i].getType());
                                        board.getCells()[j, i].setType(0);
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        private bool checkField(int x, int y) {
            if( board.getCells()[x, y].getType() == 0 ) {
                return true;
            } else {
                return false;
            }
        }

        public void startAnimation(object Sender, System.ComponentModel.DoWorkEventArgs e) {
            while( animationStart ) {
                this.InvokeIfRequired(() => updateBoard());
                Thread.Sleep(100);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            animationStart = true;
            MyWorker.RunWorkerAsync();
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
