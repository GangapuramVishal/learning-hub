using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesForPublishSubscriberDesignPattern
{
    public delegate void OnVideoUploaded();
    public class Video
    {
        //reference variable
       public OnVideoUploaded onVideoUploaded;

        public string Name { get; set; }
        public Video(string name)
        {
            Name = name;
        }
        public void UploadVideo()
        {
            Console.WriteLine("Uploading Video");
            Thread.Sleep(2000);
            Console.WriteLine("Uploaded Video");

            onVideoUploaded();  //after vide is uploaded, then calling onVideoUploaded delegate, it will call the methods it is pointing to
        }
    }
}
