namespace DelegatesForPublishSubscriberDesignPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //instance of classes
            Video video = new Video("Test");
            EmailService emailService = new EmailService();
            SMSService smsService = new SMSService();

            //maiking EmailService & SMSService  subscribe to video class
            video.onVideoUploaded += emailService.SendEmail; //onVideoUploaded is pointing to the SendEmail method of EmailService class
            video.onVideoUploaded += smsService.SendSMS; //onVideoUploaded is pointing to the SendSMS method of SMSService class

            //calling uploadvideo method
            video.UploadVideo();


            //once the video was uploaded emailservice and smsservice were notified and event handler method executed
        }
    }
}
