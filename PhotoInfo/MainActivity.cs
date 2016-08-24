using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Android.Database;
using Android.Media;

namespace PhotoInfo
{
    [Activity(Label = "PhotoInfo", MainLauncher = true, Icon = "@drawable/photoInfoIcon")]
    public class MainActivity : Activity
    {
        public static readonly int PickImageId = 1000;
        private ImageView _imageView;
        private TextView _dataTextView;
        private Button getImageBtn;
        private string _imagePath = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Set up selecting an image and adding it
            _imageView = FindViewById<ImageView>(Resource.Id.selectedImageView);
            getImageBtn = FindViewById<Button>(Resource.Id.getImageBtn);
            _dataTextView = FindViewById<TextView>(Resource.Id.dataTextView);
            getImageBtn.Click +=getImageBtn_Click;
        }
        private void getImageBtn_Click(object sender, EventArgs e)
        {
            Intent = new Intent();
            Intent.SetType("image/jpg");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            string photoInfo = null;
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                _imageView.SetImageURI(data.Data);

                // Get the picture path
                _imagePath = getPicturePath(data.Data);

                // Since we have a picture, show it, hide the add button
                // and show the input titles and fields.
                _imageView.Visibility = ViewStates.Visible;
                getImageBtn.Visibility = ViewStates.Gone;
                _dataTextView.Visibility = ViewStates.Visible;
                string picturePath = getPicturePath(data.Data);
                photoInfo += "Path: " + picturePath + "\n";

                ExifInterface exif = new ExifInterface(picturePath);
                // Get the picture height
                string length = exif.GetAttribute(ExifInterface.TagImageLength);
                if (string.IsNullOrWhiteSpace(length)) { length = "N/A"; }
                photoInfo += "Length: " + length + "\n";

                // Get the picture width
                string width = exif.GetAttribute(ExifInterface.TagImageWidth);
                if (string.IsNullOrWhiteSpace(width)) { width = "N/A"; }
                photoInfo += "Width: " + width + "\n";

                // Get the picture date time
                string dateTime = exif.GetAttribute(ExifInterface.TagDatetime);
                if (string.IsNullOrWhiteSpace(dateTime)) { dateTime = "N/A"; }
                photoInfo += "date time: " + dateTime + "\n";

                // Get the picture iso
                string iso = exif.GetAttribute(ExifInterface.TagIso);
                if (string.IsNullOrWhiteSpace(iso)) { iso = "N/A"; }
                photoInfo += "ISO: " + iso + "\n";

                // Get the picture date from GPS
                string date = exif.GetAttribute(ExifInterface.TagGpsDatestamp);
                if (string.IsNullOrWhiteSpace(date)) { date = "N/A"; }
                photoInfo += "GPS Date: " + date + "\n";

                // Get the picture date time from GPS timestamp
                string timeStamp = exif.GetAttribute(ExifInterface.TagGpsTimestamp);
                if (string.IsNullOrWhiteSpace(timeStamp)) { timeStamp = "N/A"; }
                photoInfo += "GPS Timestamp: " + timeStamp + "\n";

                // Get the picture longitude from GPS Longitude
                string longitude = exif.GetAttribute(ExifInterface.TagGpsLongitude);
                if (string.IsNullOrWhiteSpace(longitude)) { longitude = "N/A"; }
                photoInfo += "GPS Longitude: " + longitude + "\n";

                // Get the picture latitude from GPS Latitude
                string latitude = exif.GetAttribute(ExifInterface.TagGpsLatitude);
                if (string.IsNullOrWhiteSpace(latitude)) { latitude = "N/A"; }
                photoInfo += "GPS Latitude: " + latitude + "\n";
                
                float[] latLong = new float[2];
                if (exif.GetLatLong(latLong))
                {
                    photoInfo += "Latitude2: " + latLong[0] + "\n";
                    photoInfo += "Longitude2: " + latLong[1] + "\n";
                }
                else
                {
                    photoInfo += "Latitude2: N/A "+ "\n";
                    photoInfo += "Longitude2: N/A " + "\n";
                }
                _dataTextView.Text = photoInfo;
            }
        }

        public string getPicturePath(Android.Net.Uri uri)
        {
            // Thanks to Benoit Jadinon via StackOverFlow
            // http://stackoverflow.com/questions/23309080/android-file-path-xamarin
            string path = null;
            String[] projection = new[] { MediaStore.Images.Media.InterfaceConsts.Data };
            using (ICursor cursor = ContentResolver.Query(uri, projection, null, null, null))
            {
                if (cursor != null)
                {
                    int columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Audio.Media.InterfaceConsts.Data);
                    cursor.MoveToFirst();
                    path = cursor.GetString(columnIndex);
                }
            }
            return path;
        }
    }
}

