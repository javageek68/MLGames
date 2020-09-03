using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MLGames;
using static MLGames.NeuralNetwork;

namespace TestPlatform
{
    public partial class NetTester : Form
    {
        NeuralNetwork net;
        int[] layers = new int[3] { 3, 5, 1 };

        public ActivationFunctions[] activation = new ActivationFunctions[2] { ActivationFunctions.leakyrelu, ActivationFunctions.leakyrelu };
        public NetTester()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }



        void Start()
        {
            this.net = new NeuralNetwork(layers, activation);
            for (int i = 0; i < 20000; i++)
            {
                net.BackPropagate(new float[] { 0, 0, 0 }, new float[] { 0 });
                net.BackPropagate(new float[] { 1, 0, 0 }, new float[] { 1 });
                net.BackPropagate(new float[] { 0, 1, 0 }, new float[] { 1 });
                net.BackPropagate(new float[] { 0, 0, 1 }, new float[] { 1 });
                net.BackPropagate(new float[] { 1, 1, 0 }, new float[] { 1 });
                net.BackPropagate(new float[] { 0, 1, 1 }, new float[] { 1 });
                net.BackPropagate(new float[] { 1, 0, 1 }, new float[] { 1 });
                net.BackPropagate(new float[] { 1, 1, 1 }, new float[] { 1 });
            }
            this.Display("cost: " + net.cost);

            Display(net.FeedForward(new float[] { 0, 0, 0 })[0].ToString());
            Display(net.FeedForward(new float[] { 1, 0, 0 })[0].ToString());
            Display(net.FeedForward(new float[] { 0, 1, 0 })[0].ToString());
            Display(net.FeedForward(new float[] { 0, 0, 1 })[0].ToString());
            Display(net.FeedForward(new float[] { 1, 1, 0 })[0].ToString());
            Display(net.FeedForward(new float[] { 0, 1, 1 })[0].ToString());
            Display(net.FeedForward(new float[] { 1, 0, 1 })[0].ToString());
            Display(net.FeedForward(new float[] { 1, 1, 1 })[0].ToString());
            //We want the gate to simulate 3 input or gate (A or B or C)
            // 0 0 0    => 0
            // 1 0 0    => 1
            // 0 1 0    => 1
            // 0 0 1    => 1
            // 1 1 0    => 1
            // 0 1 1    => 1
            // 1 0 1    => 1
            // 1 1 1    => 1
        }

        void Display(string strMsg)
        {
            this.txtStatus.AppendText(strMsg + "\r\n");
        }
    }
}
