using SharpCifs.Smb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WebDav;


/*
 https://hdyj.synology.me:5006
 
 
 */

namespace test11
{

    

    public partial class Form1 : Form
    {
        public static string baseUrl = "https://hdyj.synology.me:5006/home/";

        public static string baseUrl1 = "http://192.168.0.51:8080/";

        WebDavClient client;

        public Form1()
        {
            InitializeComponent();

            var clientParams = new WebDavClientParams
            {
                BaseAddress = new Uri(baseUrl1),
                Credentials = new NetworkCredential("Sejin", "123123")
                //Timeout = new TimeSpan(3000)
            };

            client = new WebDavClient(clientParams);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.Copy("1.txt", "_2.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.Delete("_2.txt");
        }

        private void button3_Click(object sender, EventArgs e)
        {
           TestPropfind(client);
        }

        private static async Task TestWebDav(WebDavClient client)
        {
            await TestPropfind(client);
        }

        public static async Task TestPropfind(WebDavClient client)
        {
            try
            {
                var result = await client.Propfind(baseUrl);

                System.Console.WriteLine("--");
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            var file = new SmbFile("smb://Sejin:123123@192.168.0.51:8080/ShareName/Folder/FileName.txt");

            //Get readable stream.
            var readStream = file.GetInputStream();

            //Create reading buffer.
            var memStream = new MemoryStream();

            //Get bytes.
            ((Stream)readStream).CopyTo(memStream);

            //Dispose readable stream.
            readStream.Dispose();

            Console.WriteLine(Encoding.UTF8.GetString(memStream.ToArray()));

        }
    }
}
