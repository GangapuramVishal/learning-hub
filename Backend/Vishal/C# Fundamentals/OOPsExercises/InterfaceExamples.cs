using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsExercises
{
    public class InterfaceExamples
    {
        /* The Interface in C# is a Fully Unimplemented Class used for declaring a set of operations/methods of an object.
         * So, we can define an interface as a pure abstract class, which allows us to define only abstract methods. 
         * The abstract method means a method without a body or implementation. It is used to achieve multiple inheritances,
         * which the class can’t achieve. It is used to achieve full abstraction because it cannot have a method body.
          
         * An interface specifies a set of methods, properties, events, or indexers that implementing classes must provide.
         * Every abstract method of an interface should be implemented by the child class of the interface without fail (Mandatory).
         */
    //An interface for mediaplayer
    public interface IMediaPlayer
        {
            //Methods without implementation
            void Play();
            void Stop();
            void Pause();
        }

    public class MusicPlayer : IMediaPlayer
        {
            public void Play()
            {
                Console.WriteLine("Music Playing");
            }
            public void Pause()
            {
                Console.WriteLine("Music Paused");
            }
            public void Stop()
            {
                Console.WriteLine("Music Stopped");
            }
        }
    public class VideoPlayer : IMediaPlayer
        {
            public void Play()
            {
                Console.WriteLine("Video Playing");
            }
            public void Pause()
            {
                Console.WriteLine("Video Paused");
            }
            public void Stop()
            {
                Console.WriteLine("Video Stopped");
            }
        }
    }
}
