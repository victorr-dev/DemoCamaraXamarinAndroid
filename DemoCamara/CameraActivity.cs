using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Widget;
using Java.IO;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

namespace DemoCamara
{
    [Activity(Label = "CameraActivity", MainLauncher = true)]
    public class CameraActivity : Activity
    {

        private File _dir;
        private File _file;
        private ImageView _imageView;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

       

            // make it available in the gallery
            //Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            //Uri contentUri = Uri.FromFile(_file);
            //mediaScanIntent.SetData(contentUri);
            //SendBroadcast(mediaScanIntent);

            // display in ImageView. We will resize the bitmap to fit the display
            // Loading the full sized image will consume to much memory 
            // and cause the application to crash.
            int height = _imageView.Height;
            int width = Resources.DisplayMetrics.WidthPixels;

            Bundle extras = data.Extras;
            Bitmap imageBitmap = (Bitmap)extras.Get("data");
            //_imageView.setImageBitmap(imageBitmap);

            System.Console.Write(_file.Path);
            //using (Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height))
            //{
                //_imageView.SetImageURI(Android.Net.Uri.Parse(_file.Path));
                _imageView.SetImageBitmap(imageBitmap);
            
                string filePath = _file.Path;
            //}
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.camera);

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                Button sendBtn = FindViewById<Button>(Resource.Id.sendCamera);
                Button button = FindViewById<Button>(Resource.Id.myButton);
                _imageView = FindViewById<ImageView>(Resource.Id.imageView1);

                sendBtn.Click += SendBtn_Click;
                button.Click += TakeAPicture;
            }
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            //var intent = new Intent();
            //Bitmap bitmap = _file.Path.LoadAndResizeBitmap(50, 50);
            //string bit = Convert.ToString(bitmap);
            //intent.PutExtra("bitmap", bit);
            //
            //Android.Net.Uri contentUri = Android.Net.Uri.FromFile(_file);
            //string uriName = Convert.ToString(contentUri);
            //
            //string filePath = _file.Path;
            //intent.PutExtra("filepath", filePath);
            //intent.PutExtra("image3", uriName);
            //SetResult(Result.Ok, intent);
            //Finish();

        }
        private void CreateDirectoryForPictures()
        {

                _dir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
                if (!_dir.Exists())
                {
                    System.Console.Write("Crea el directorio de fotografias");
                    _dir.Mkdirs();
                }

        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            //File documentsPath = new File(Environment.ExternalStorageDirectory + "/Android/Data/");

            Intent intent = new Intent(MediaStore.ActionImageCapture);

            _file = new File(_dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

            System.Console.Write("=========================================================");
            System.Console.Write(_file.Path);
            System.Console.Write("=========================================================");
            //intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.Parse(_file.Path));

            StartActivityForResult(intent,998);
        }
    }
}